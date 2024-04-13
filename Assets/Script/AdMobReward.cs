using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine.UI;//1020
//Renshuuシーンで実行されるAdMobリワードのスクリプト
public class AdMobReward : MonoBehaviour
{
    //public GameObject AdLoadedStatus;//広告の準備状態を示すUI要素
    public GameObject RewardButton;
    public GameObject endImage;
     public Text gameOverMarkText;
    private bool rewardeFlag=false;//リワード広告の報酬付与用　初期値はfalse
    private bool NoShowFlag = false;//リワード広告が読み込めていなかった場合　初期値はfals
    private RewardedAd _rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る
    
    [SerializeField] private TextMeshProUGUI TextValue;//coin枚数を表示させる
    private bool SpinnerFlag = false;//Spinnerパネル表示用　初期値はfalse
    private bool OpenRewardFlag = false;//リワード広告全面表示　初期値はfalse
    //private string adUnitId;
#if UNITY_ANDROID
    //string _adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
    string _adUnitId = "ca-app-pub-7439888210247528/4069893017";//ここにAndroidのリワード広告IDを入力
        
#elif UNITY_IPHONE
    //string _adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
    string _adUnitId = "ca-app-pub-7439888210247528/7409830625";//ここにiOSのリワード広告IDを入力
        
#else
        string _adUnitId = "unexpected_platform";
#endif
    //リワード広告を表示する関数
    //ボタンに割付けして使用
    public void ShowAdMobReward()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            Debug.Log("Showing rewarded ad.");
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                    reward.Amount,
                    reward.Type));
            });
        }
        else
        {
            Debug.LogError("Rewarded ad is not ready yet.");
        }

    }

    private void Update()
    {
        //広告を見た後にrewardeFlagをtrueにしている
        //広告を見たらこの中の処理が実行される
        if (rewardeFlag == true)
        {
            rewardeFlag = false;//報酬付与用のフラグをfalseへ戻す
            //ここに報酬の処理を書く
            GameManager.singleton.coinNum += 100;
            //Debug.Log("リワードcoinGet" + GameManager.instance.totalCoin + "枚");
            GameManager.singleton.CoinSave();
            TextValue.text = GameManager.singleton.coinNum.ToString();
            endImage.SetActive(true);
            endImage.GetComponent<DOTextBounceAnim>();
            gameOverMarkText.gameObject.SetActive(true);
            gameOverMarkText.text = "コインはガチャでつかえるよ";
            
        }
    }
    private void Start()
    {      
        CreateAndLoadRewardedAd();//リワード広告読み込み
        MobileAds.SetiOSAppPauseOnBackground(true);
    }

    //リワード広告読み込む関数
    public void CreateAndLoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            DestroyAd();
        }
        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                return;
            }
            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                return;
            }

            // The operation completed successfully.
            Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
            _rewardedAd = ad;

            // Register to ad events to extend functionality.
            RegisterEventHandlers(ad);

            // Inform the UI that the ad is ready.
            //AdLoadedStatus?.SetActive(true);
            });
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            rewardeFlag = true;
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            rewardeFlag = true;
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when the ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            SpinnerFlag = true;
            OpenRewardFlag = true;
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            rewardeFlag = true;
            _rewardedAd.Destroy();
            CreateAndLoadRewardedAd();
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content with error : "
                           + error);
            CreateAndLoadRewardedAd();
        };
    }
    /// <summary>
    /// Destroys the ad.
    /// </summary>
    public void DestroyAd()
    {
        if (_rewardedAd != null)
        {
            Debug.Log("Destroying rewarded ad.");
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        // Inform the UI that the ad is not ready.
        //AdLoadedStatus?.SetActive(false);
    }

}
