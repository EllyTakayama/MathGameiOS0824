using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdMobBanner : MonoBehaviour
{
    //public GameObject AdLoadedStatus;広告の準備状態を示すUI要素
#if UNITY_ANDROID
    //string _adUnitId = "ca-app-pub-3940256099942544/6300978111";//テストAndroidのバナーID
    string _adUnitId = "ca-app-pub-7439888210247528/5761220963";//ここにAndroidのバナーIDを入力
        

#elif UNITY_IPHONE
        //string _adUnitId = "ca-app-pub-3940256099942544/2934735716";//テストiOSのバナーID
        string _adUnitId = "ca-app-pub-7439888210247528/9106055676";//ここにiOSのバナーIDを入力

#else
        string _adUnitId = "unexpected_platform";
#endif//やること
    //1.バナー広告IDを入力
    //2.バナーの表示位置　(現状表示位置は下になっています。)
    //3.バナー表示のタイミング (現状 起動直後になっています。)

    private BannerView _bannerView;//BannerView型の変数bannerViewを宣言　この中にバナー広告の情報が入る


    //シーン読み込み時からバナーを表示する
    //最初からバナーを表示したくない場合はこの関数を消してください。
    private void Start()
    {
        LoadAd(); //アダプティブバナーを表示する関数 呼び出し
    }
public void CreateBannerView()
        {
            Debug.Log("Creating banner view.");

            // If we already have a banner, destroy the old one.
            if(_bannerView != null)
            {
                DestroyAd();
            }

            // Create a 320x50 banner at top of the screen.
            _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

            // Listen to events the banner may raise.
            ListenToAdEvents();

            Debug.Log("Banner view created.");
        }

        /// <summary>
        /// Creates the banner view and loads a banner ad.
        /// </summary>
        public void LoadAd()
        {
            // Create an instance of a banner view first.
            if(_bannerView == null)
            {
                CreateBannerView();
            }

            // Create our request used to load the ad.
            var adRequest = new AdRequest();

            // Send the request to load the ad.
            Debug.Log("Loading banner ad.");
            _bannerView.LoadAd(adRequest);
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public void ShowAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Showing banner view.");
                _bannerView.Show();
            }
        }

        /// <summary>
        /// Hides the ad.
        /// </summary>
        public void HideAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Hiding banner view.");
                _bannerView.Hide();
            }
        }

        /// <summary>
        /// Destroys the ad.
        /// When you are finished with a BannerView, make sure to call
        /// the Destroy() method before dropping your reference to it.
        /// </summary>
        public void DestroyAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
            // Inform the UI that the ad is not ready.
            //AdLoadedStatus?.SetActive(false);
        }

        /// <summary>
        /// Logs the ResponseInfo.
        /// </summary>
        public void LogResponseInfo()
        {
            if (_bannerView != null)
            {
                var responseInfo = _bannerView.GetResponseInfo();
                if (responseInfo != null)
                {
                    UnityEngine.Debug.Log(responseInfo);
                }
            }
        }

        /// <summary>
        /// Listen to events the banner may raise.
        /// </summary>
        private void ListenToAdEvents()
        {
            // Raised when an ad is loaded into the banner view.
            _bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                    + _bannerView.GetResponseInfo());

                // Inform the UI that the ad is ready.
                //AdLoadedStatus?.SetActive(true);
            };
            // Raised when an ad fails to load into the banner view.
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : " + error);
            };
            // Raised when the ad is estimated to have earned money.
            _bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            _bannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            _bannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
            // Raised when an ad opened full screen content.
            _bannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            _bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
            };
        }
    }

