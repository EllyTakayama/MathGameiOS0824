using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;//DoTweenを使用する記述
//メニュー画面で練習、テストどちらかを選択してからプレイする段を選ぶ0908


public class ModeMenuManager : MonoBehaviour
{
   public  Button renshuu;
   public  Button test;
   [SerializeField] private GameObject ModeMenuPanel;
   [SerializeField] private GameObject TopMenuPanel;
   [SerializeField] GameObject[] Buttons;
   [SerializeField] private GameObject studyPanel;
   [SerializeField] private Text studyRecord;
   [SerializeField] private GameObject ReAnnounceText;
   [SerializeField] private GameObject TestAnnounceText;
   [SerializeField] private GameObject MulToggle;
   [SerializeField] private GameObject settingPanel;
   [SerializeField] private GameObject playExPanel;
     public GameObject cloud1Image;
     public GameObject cloud1Image2;
     public GameObject AdMobManager;


    //tagの位置で正誤判定している
    public static string tagOfButtons;
    public bool isPressed; 
    
    //public bool isPressedでれんしゅうボタン、テストボタンを押した場合の分岐を行う
    //renshuuButtonを押した場合をtrue testButtonを押した場合はfalseになる
    //currentMode<10が練習、>10が力だめし問題
    // Start　トップメニュー以外は非表示
    void Start()
    {
        ModeMenuPanel.SetActive(false);
        ReAnnounceText.SetActive(false);
        TestAnnounceText.SetActive(false);
        studyPanel.SetActive(false);
        settingPanel.SetActive(false);
        playExPanel.SetActive(false);
        SoundManager.instance.PlayBGM("TopMenuPanel");
        if(GameManager.singleton.SceneCount==5||GameManager.singleton.SceneCount==20||
        GameManager.singleton.SceneCount==50||GameManager.singleton.SceneCount==70||
        GameManager.singleton.SceneCount==100||GameManager.singleton.SceneCount==130){
        AdMobManager.GetComponent<StoreReviewManager>().RequestReview();
        Debug.Log("レビュー画面表示");
        }
        
    }

    //れんしゅうボタンかテストボタンかで分岐します
    //どちらのボタンでもModeMenuPanelは表示されます、案内テキストがれんしゅうとテストとわかれています
    
    public void SelectRenshuu()//れんしゅうボタンを押した場合
    {
        SoundManager.instance.PlaySEButton();//SoundManagerからPlaySE0を実行
        GameManager.singleton.isRenshu = true;//練習モードの時の分岐
        //renshuuButtonでisPressedをtrueにする
        ModeMenuPanel.SetActive(true);
        TopMenuPanel.SetActive(false);
        ReAnnounceText.SetActive(true);
        isPressed =true;
        SoundManager.instance.PlayBGM("ModeMenuPanel");

      
    }
    public void SelectTest()//テストボタンを押した場合
    {
        SoundManager.instance.PlaySEButton();//SoundManagerからPlaySE0を実行
        //testButtonだとisPressedをfalseにする
        GameManager.singleton.isRenshu = false;//テストモードの時の分岐
        ModeMenuPanel.SetActive(true);
        TopMenuPanel.SetActive(false);
        MulToggle.SetActive(false);
        TestAnnounceText.SetActive(true);
         SoundManager.instance.PlayBGM("ModeMenuPanel");
        isPressed =false;
     
    }
    public void SelectRecord()//成績パネル表示
    {
      
        SoundManager.instance.PlaySEButton();//SoundManagerからPlaySE0を実行
        studyPanel.SetActive(true);
         EasySaveManager.singleton.Load();
         studyRecord.text = EasySaveManager.singleton.str;
        
    }
    
    public void SelectTable()//九九パネル表示
    {
        SoundManager.instance.PlaySEButton();
        DOTween.KillAll();
        SceneManager.LoadScene("Kuku");
        
    }

    public void SelectSetting()
    {
      
      Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
    SoundManager.instance.PlaySEButton();
        settingPanel.SetActive(true);
         
    }

    public void ExPlayPanel()//遊び方説明パネル表示
    {
      
        SoundManager.instance.PlaySEButton();
         playExPanel.SetActive(true);
         
    }
    public void TopPanelMove()
    {
      GameManager.singleton.SceneCount++;
      GameManager.singleton.SaveSceneCount();
      Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
      int IScount = GameManager.singleton.SceneCount;

        SoundManager.instance.PlaySEButton();//SoundManagerからSEButtonを実行
        if(settingPanel == true){
            settingPanel.SetActive(false);
        }
        if(playExPanel == true){
            playExPanel.SetActive(false);
        }
        if(studyPanel == true){
            studyPanel.SetActive(false);
        }
        if(ModeMenuPanel == true){
            ModeMenuPanel.SetActive(false);
            TopMenuPanel.SetActive(true);

        }
        if(IScount>0 && IScount%3 ==0){
            DOTween.KillAll();
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }

        if(GameManager.singleton.SceneCount==15||GameManager.singleton.SceneCount==30||
        GameManager.singleton.SceneCount==60||GameManager.singleton.SceneCount==90||
        GameManager.singleton.SceneCount==110||GameManager.singleton.SceneCount==140){
        AdMobManager.GetComponent<StoreReviewManager>().RequestReview();
        Debug.Log("レビュー画面表示");
        }
     
    }

    public void Onclick(string buttonname)//段を選ぶボタンのScriptです。OnClickでボタンの名前を取得します
    { 
        if (isPressed)//れんしゅうボタンの分岐、ボタンの名前で分岐するSwitch文です
        {
            switch (buttonname)
            {
                case "Button1":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                    GameManager.singleton.currentMode = 1;
                    // テストボタンからの2段でcurrentMode2を選択してMathAndScript.csに
                   SceneManager.LoadScene("Renshuu");
                   DOTween.KillAll();
                    break;

                case "Button2":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 2;
                DOTween.KillAll();
                   SceneManager.LoadScene("Renshuu");
                   break;

                case "Button3":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                DOTween.KillAll();
                GameManager.singleton.currentMode = 3;
                    // テストボタンから3段でcurrentMode3を選択してMathAndScript.csに
                   SceneManager.LoadScene("Renshuu");
                break;

                case "Button4":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 4;
                DOTween.KillAll();
                   SceneManager.LoadScene("Renshuu");
                break;

                case "Button5":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 5;
                DOTween.KillAll();
                    // テストボタンからの5段でcurrentMode5を選択してMathAndScript.csに
                   SceneManager.LoadScene("Renshuu");
                   break;

                case "Button6":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 6;
                 DOTween.KillAll();
                   SceneManager.LoadScene("Renshuu");
                break;

                case "Button7":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 7;
                DOTween.KillAll();
                   SceneManager.LoadScene("Renshuu");
                break;

                case "Button8":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 8;
                DOTween.KillAll();
                   SceneManager.LoadScene("Renshuu");
                break;

                case "Button9":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 9;
                DOTween.KillAll();
                   SceneManager.LoadScene("Renshuu");
                break;
               }
        }

        if(isPressed == false)//テストボタンの分岐、isPressed ==false currentMode11-19
        {
            switch (buttonname)
            {
                case "Button1":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 11;
                 DOTween.KillAll();
                   SceneManager.LoadScene("Game");
                break;

                case "Button2":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                SceneManager.LoadScene("Game");
               GameManager.singleton.currentMode = 12;
                 DOTween.KillAll();
                   SceneManager.LoadScene("Game");
                break;

                case "Button3":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 13;
                 DOTween.KillAll();
                    // テストボタンからの3段でcurrentMode13を選択してMathAndScript.csに
                   SceneManager.LoadScene("Game");
                break;

                case "Button4":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 14;
                 DOTween.KillAll();
                   SceneManager.LoadScene("Game");
                break;

                case "Button5":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 15;
                DOTween.KillAll();
                    // テストボタンからの5段でcurrentMode15を選択してMathAndScript.csに
                   SceneManager.LoadScene("Game");
               break;

                case "Button6":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 16;
                DOTween.KillAll();
                    // テストボタンからの2段でcurrentMode16を選択してMathAndScript.csに
                   SceneManager.LoadScene("Game");
                 break;

                case "Button7":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
            
                GameManager.singleton.currentMode = 17;
                DOTween.KillAll();
                   SceneManager.LoadScene("Game");
                break;

                case "Button8":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 18;
                 DOTween.KillAll();
                   SceneManager.LoadScene("Game");
                break;
                case "Button9":
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 19;
                DOTween.KillAll();
                   SceneManager.LoadScene("Game");
                break;
                
            }
        }
    }

    
}
   
