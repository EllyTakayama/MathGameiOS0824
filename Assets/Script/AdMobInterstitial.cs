using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;
//GoogleMobileAds v8.70
public class AdMobInterstitial : MonoBehaviour
{
    //public GameObject AdLoadedStatus;//
#if UNITY_ANDROID
    //string _adUnitId = "ca-app-pub-3940256099942544/1033173712";//TestAndroidのインタースティシャル広告ID
    string _adUnitId = "ca-app-pub-7439888210247528/3791046630";//ここにAndroidのインタースティシャル広告IDを入力

#elif UNITY_IPHONE
    //string _adUnitId = "ca-app-pub-3940256099942544/4411468910";//TestiOSのインタースティシャル広告ID
    string _adUnitId = "ca-app-pub-7439888210247528/9652850584";//ここにiOSのインタースティシャル広告IDを入力

#else
        string _adUnitId = "unexpected_platform";
#endif

    private InterstitialAd _interstitialAd;//InterstitialAd型の変数interstitialを宣言　この中にインタースティシャル広告の情報が入る

    private void Start()
    {
        RequestInterstitial();
        Debug.Log("読み込み開始");
    }

    //インタースティシャル広告を表示する関数
    //ボタンなどに割付けして使用
    public void ShowAdMobInterstitial()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
            //Debug.Log("インタースティシャル広告表示");
        }
        else
        {
            SceneManager.LoadScene("Menu");
            Debug.Log("広告読み込み未完了");
        }
    }
    public void DestroyAd()
    {
        if (_interstitialAd != null)
        {
            Debug.Log("Destroying interstitial ad.");
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        // Inform the UI that the ad is not ready.
        //AdLoadedStatus?.SetActive(false);
    }

    /// <summary>
    /// Logs the ResponseInfo.
    /// </summary>
    public void LogResponseInfo()
    {
        if (_interstitialAd != null)
        {
            var responseInfo = _interstitialAd.GetResponseInfo();
            UnityEngine.Debug.Log(responseInfo);
        }
    }
    
    //インタースティシャル広告を読み込む関数
    private void RequestInterstitial()
    { //インタースティシャル広告初期化
        if (_interstitialAd != null)
        {
            DestroyAd();
        }
        Debug.Log("Loading interstitial ad.");
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                return;
            }
            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                return;
            }

            // The operation completed successfully.
            Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
            _interstitialAd = ad;

            // Register to ad events to extend functionality.
            RegisterEventHandlers(ad);

            // Inform the UI that the ad is ready.
            //AdLoadedStatus?.SetActive(true);
            });
    }

    
    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            _interstitialAd.Destroy();
            //インタースティシャル再読み込み開始
            RequestInterstitial();
            Debug.Log("インタースティシャル広告再読み込み");
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content with error : "
                           + error);
            RequestInterstitial();
        };
    }

}