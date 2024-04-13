using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;//1020
public class AdmobGReward : MonoBehaviour
{
//やること
    //1.リワード広告IDの入力
    //2.Update関数のif文内に報酬内容を入力
    //3.リワード起動設定　ShowAdMobReward()を使う
    private bool rewardeFlag = false;//リワード広告の報酬付与用　初期値はfalse
    private bool rewardeFlag1 = false;//リワード広告の報酬付与用　初期値はfalse
    private bool SpinnerFlag = false;//Spinnerパネル表示用　初期値はfalse
    private bool OpenRewardFlag = false;//リワード広告全面表示　初期値はfalse
    private bool NoShowFlag = false;//リワード広告が読み込めていなかった場合　初期値はfals
    public Text coinAddText;//coinを表示するtext
    private RewardedAd _rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る
    public GameObject afterAdPanel;
    public GameObject SpinnerPanel;
    public Text rewardText;//広告読み込めなかった時にテキスト差し替え
#if UNITY_ANDROID
    string _adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
    //string _adUnitId = "ca-app-pub-7439888210247528/4069893017";//ここにAndroidのリワード広告IDを入力
        
#elif UNITY_IPHONE
    string _adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
    //string _adUnitId = "ca-app-pub-7439888210247528/7409830625";//ここにiOSのリワード広告IDを入力
        
#else
        _adUnitId = "unexpected_platform";
#endif

    private void Start()
    {      
        //CreateAndLoadRewardedAd();//リワード広告読み込み
#if UNITY_IPHONE
        MobileAds.SetiOSAppPauseOnBackground(true);
#endif   
    }
    //リワード広告を表示する関数
    //ボタンに割付けして使用
    public void ShowAdMobReward()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        else{
            //Debug.Log("リワード広告読み込み未完了");
            NoShowFlag = true;
        }
    }


    private void Update()
    {
//広告がダウンロード失敗して表示されない場合
        if (NoShowFlag == true)
        {
            NoShowFlag = false;
            CreateAndLoadRewardedAd();
            //rewardText.text = "広告がダウンロード\nできませんでした";
        }
        //広告を見た後にrewardeFlagをtrueにしている
        //広告を見たらこの中の処理が実行される
        //報酬をゲットしてリワードをクローズする場合
        if (rewardeFlag1 == true && rewardeFlag == true)
        {
            rewardeFlag1 = false;
            rewardeFlag = false;
            afterAdPanel.SetActive(true);
            GameManager.singleton.LoadCoin();
            GameManager.singleton.beforeCoin = GameManager.singleton.coinNum;
            GameManager.singleton.coinNum += 100;
            //Debug.Log("リワードcoinGet" + GameManager.instance.totalCoin + "枚");
            GameManager.singleton.CoinSave();
            //SpinnerPanel.SetActive(false);
            afterAdPanel.GetComponent<DOafterRewardPanel>().coinAddText.text = GameManager.singleton.coinNum.ToString();
            afterAdPanel.GetComponent<DOafterRewardPanel>().AfterReward();
            SpinnerPanel.SetActive(false);
            //Debug.Log("リワード報酬後SpinPanel," + SpinnerPanel.activeSelf);
            //報酬を得ないでクローズする場合
        }
        else if (rewardeFlag1 == true && rewardeFlag == false)
        {
            rewardeFlag1 = false;
            afterAdPanel.SetActive(false);
            //Debug.Log("報酬なしクローズafterAdPanel," + afterAdPanel.activeSelf);
        }

        if (OpenRewardFlag == true)
        {
            OpenRewardFlag = false;
            //Debug.Log("リワードOpenRewardFlag" + OpenRewardFlag);
        }
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
            rewardeFlag1 = true;
            rewardeFlag = true;
            DestroyAd();
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
