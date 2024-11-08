﻿using UnityEngine;
using System.Collections;
using System.IO; // this is required for input and output of data
using System;
using System.Runtime.Serialization.Formatters.Binary;//this is required to convert data into binary
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UniRx;
using UnityEngine.Networking;
#if UNITY_ANDROID
using Google.Play.Review;
#endif
//MathAndScriptの方のゲームマネージャー

public enum GameManagerMathsType
{
    multiplicationRenshuu,
    multiplicationTest,
    multiplicationMusikui
}
public class GameManager : MonoBehaviour {

    //we make static so in games only one script is name as this
    public static GameManager singleton;
    //data not to store on device
    public GameManagerMathsType currentMathsType;//MathTypeを代入する変数 
    public int currentScore;
    public bool isGameOver;
    public int currentCount;
    public int currentMode;
    public bool test9;
    public bool test7;
    public bool test5;
    public int SceneCount;//インタースティシャル広告表示のためにScene表示をカウントしていきます
    public bool isRenshu;
    public bool isAsendingOrder;//１から順に出題されるかどうか
    public int TestMondaiCount;//Mondai数
    public int coinNum;//コインの数
    public int beforeCoin;//LoginBonusでコインが増える前の数
    public int beforeTotalCoin;//TopLoging.csで使う
    public int totalCoin;//TopLoging.csで使う
    
    //public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御
    
    //data to store on device
    public int hiScore;
    public bool isMusicOn;
    public bool isGameStartedFirstTime;
    
    //it is call only once in a scene
    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
    //currentCount = 1;
    // Initialize the Google Mobile Ads SDK.
    MobileAds.Initialize(initStatus => { });
        //print("Admob初期化");
        LoadSceneCount();
        LoadOrderJun();
        //LoadMathsType();//MathTypeをダウンロードButtonで分岐させるからやらない
        LoadCoin();//coinの枚数をロードLoginManagerの方でやる
       //RequestReview();
       //Debug.Log("Sceneカウント"+SceneCount);
    }

    public void CoinSave()
    {
        ES3.Save<int>("coinNum",coinNum,"coinNum.es3" );
        Debug.Log("セーブcoinNum"+coinNum);
    }
    public void LoadCoin()
    {
        coinNum = ES3.Load<int>("coinNum","coinNum.es3",0 );
        Debug.Log("ロードcoinNum"+coinNum);
    }
    
    // MathsTypeのセーブ
    public void SaveMathsType()
    {
        // MathsTypeはenumなのでintに変換してセーブ
        ES3.Save<int>("currentMathsType", (int)currentMathsType, "currentMathsType.es3");
        Debug.Log("セーブcurrentMathsType" + currentMathsType);
    }

    // MathsTypeのロード
    public void LoadMathsType()
    {
        // ロードしたintをenumに変換
        currentMathsType = (GameManagerMathsType)ES3.Load<int>("currentMathsType", "currentMathsType.es3", (int)GameManagerMathsType.multiplicationRenshuu);
        Debug.Log("ロードcurrentMathsType" + currentMathsType);
    }
    
    //isAsendingOrderのセーブ
    public void SaveOrderJun(){
        ES3.Save<bool>("isAsendingOrder",isAsendingOrder,"isAsendingOrder.es3" );
        Debug.Log("セーブisAsendingOrder"+isAsendingOrder);
    }
    
    //isAsendingOrderのロード　データがない時はかける数は１から始める
    public void LoadOrderJun()
    {
        isAsendingOrder = ES3.Load<bool>("isAsendingOrder", "isAsendingOrder.es3", true);
        Debug.Log("ロードisAsendingOrder"+isAsendingOrder);
    }

    public void SaveSceneCount(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("SceneCount",SceneCount,"SceneCount.es3" );
        Debug.Log("セーブSceneCount"+SceneCount);
    }
    
    public void LoadSceneCount(){
         //if(ES3.KeyExists("isfontSize"))
         SceneCount = ES3.Load<int>("SceneCount","SceneCount.es3",0);
         Debug.Log("ロードSceneCount"+SceneCount);
    }
    
    public void RequestReview()
    {
       
#if UNITY_IOS && !UNITY_EDITOR
        UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID && !UNITY_EDITOR
        RateAndReview();
#else
        Debug.LogWarning("This platform is not support RequestReview.");
#endif
        Debug.Log("レビューリクエスト呼び出し");
    }

#if UNITY_ANDROID

    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;
    private Coroutine _coroutine;
    private void RateAndReview()
    {
        StartCoroutine(LaunchReview());
    }
    private IEnumerator InitReview(bool force = false)
    {
        if (_reviewManager == null) _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            if (force) DirectlyOpen();
            yield break;
        }

        _playReviewInfo = requestFlowOperation.GetResult();
    }

    public IEnumerator LaunchReview()
    {
        if (_playReviewInfo == null)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            yield return StartCoroutine(InitReview(true));
        }

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null;
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            //DirectlyOpen();
            Debug.Log("AppReviewエラー");
            yield break;
        }
    }

    private void DirectlyOpen() { Application.OpenURL("https://play.google.com/store/apps/details?id=com.RieTakayama.MathGame"); }
#endif

}
