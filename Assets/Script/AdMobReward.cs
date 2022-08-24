using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;//1020

public class AdMobReward : MonoBehaviour
{//やること
    //1.リワード広告IDの入力
    //2.Update関数のif文内に報酬内容を入力
    //3.リワード起動設定　ShowAdMobReward()を使う
    public GameObject RewardButton;
    public bool oyatsuReward = false;
    public GameObject endImage;
     public GameObject endHaloImage;
     public Text gameOverMarkText;
    public Text messageText;

    private bool rewardeFlag=false;//リワード広告の報酬付与用　初期値はfalse

    private RewardedAd rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る

    private string adUnitId;

    //リワード広告を表示する関数
    //ボタンに割付けして使用
    public void ShowAdMobReward()
    {
        //広告の読み込みが完了していたら広告表示
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            Debug.Log("リワード広告表示");
        }
        else
        {
            Debug.Log("リワード広告読み込み未完了");
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
            oyatsuReward = true;
            endImage.SetActive(false);
            endHaloImage.SetActive(true);
            SoundManager.instance.PlaySE13rewardButton();//SoundManagerからPlaySE0を実行
            FoodGenerator.instance.foodCount = 0;
            FoodGenerator.instance.foodScore =0;
            FoodGenerator.instance.endCount = 0;
            gameOverMarkText.text = "おやつ 5こ ゲット！";
            messageText.enabled = false;
            FoodGenerator.instance.Spawn();
            Debug.Log("oyatsuReward"+oyatsuReward);
            
        }

    }


    private void Start()
    {      
        //AndroidとiOSで広告IDが違うのでプラットフォームで処理を分けます。
        // 参考
        //【Unity】AndroidとiOSで処理を分ける方法
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
    //adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
    adUnitId = "ca-app-pub-7439888210247528/4069893017";//ここにAndroidのリワード広告IDを入力
        
#elif UNITY_IPHONE
    //adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
    adUnitId = "ca-app-pub-7439888210247528/7409830625";//ここにiOSのリワード広告IDを入力
        
#else
        adUnitId = "unexpected_platform";
#endif

        CreateAndLoadRewardedAd();//リワード広告読み込み
        MobileAds.SetiOSAppPauseOnBackground(true);
    }

    //リワード広告読み込む関数
    public void CreateAndLoadRewardedAd()
    {
        //リワード広告初期化
        rewardedAd = new RewardedAd(adUnitId);

        //RewardedAd型の変数 rewardedAdの各種状態 に関数を登録
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;//rewardedAdの状態が リワード広告読み込み完了 となった時に起動する関数(関数名HandleRewardedAdLoaded)を登録
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;//rewardedAdの状態が　リワード広告読み込み失敗 　となった時に起動する関数(関数名HandleRewardedAdFailedToLoad)を登録
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;//rewardedAdの状態が  リワード広告閉じられた　となった時に起動する関数(関数名HandleRewardedAdFailedToLoad)を登録
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;//rewardedAdの状態が ユーザーの報酬処理　となった時に起動する関数(関数名HandleUserEarnedReward)を登録


        //リクエストを生成
        AdRequest request = new AdRequest.Builder().Build();
        //リクエストと共にリワード広告をロード
        rewardedAd.LoadAd(request);
    }


    //リワード読み込み完了 となった時に起動する関数
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("リワード広告読み込み完了");
    }

    //リワード読み込み失敗 となった時に起動する関数
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("リワード広告読み込み失敗" + args.LoadAdError);//args.Message:エラー内容 
    }

    //リワード広告閉じられた時に起動する関数
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("リワード広告閉じられる");

        //広告再読み込み
        CreateAndLoadRewardedAd();
    }

    //ユーザーの報酬処理 となった時に起動する関数
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("報酬受け取り");
        //この関数内ではゲームオブジェクトの操作ができない
        //そのため、ここでは報酬受け取りのフラグをtrueにするだけにする
        //具体的な処理はUpdate関数内で行う。
        rewardeFlag = true;
    }


}
