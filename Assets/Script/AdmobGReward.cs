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

    private RewardedAd rewardedAd;//RewardedAd型の変数 rewardedAdを宣言 この中にリワード広告の情報が入る
    public GameObject afterAdPanel;
    public GameObject SpinnerPanel;
    public Text rewardText;//広告読み込めなかった時にテキスト差し替え
#if UNITY_ANDROID
    //string adUnitId = "ca-app-pub-3940256099942544/5224354917";//TestAndroidのリワード広告ID
    string adUnitId = "ca-app-pub-7439888210247528/4069893017";//ここにAndroidのリワード広告IDを入力
        
#elif UNITY_IPHONE
    //string adUnitId = "ca-app-pub-3940256099942544/1712485313";//TestiOSのリワード広告ID
    string adUnitId = "ca-app-pub-7439888210247528/7409830625";//ここにiOSのリワード広告IDを入力
        
#else
        adUnitId = "unexpected_platform";
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
        rewardeFlag1 = true;
        rewardedAd.Destroy();
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
