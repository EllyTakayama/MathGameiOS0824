using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;//DoTweenを使用する記述
//メニュー画面で練習、テストどちらかを選択してからプレイする段を選ぶ0908


public class GModeMenuManager : MonoBehaviour
{
   [SerializeField] GameObject[] Buttons;

     public GameObject AdMobManager;

     [SerializeField] private GameObject gameMenuPanel;//段を選択するPanel

     [SerializeField] private GameObject gameQuesPanel;//質問に回答するUIPanel

     [SerializeField] private GameObject piyoPlayer;//ピヨちゃんのGameObject

     [SerializeField] private DoGStartPanelRotate _doGStartPanelRotate;//スタート時のImage表示

     [SerializeField] private TextMeshProUGUI coinText;
    //tagの位置で正誤判定している
    public static string tagOfButtons;
    // Start　トップメニュー以外は非表示
    void Start()
    {
        /*
        if(GameManager.singleton.SceneCount==5||GameManager.singleton.SceneCount==20||
        GameManager.singleton.SceneCount==50||GameManager.singleton.SceneCount==70||
        GameManager.singleton.SceneCount==100||GameManager.singleton.SceneCount==130){
        AdMobManager.GetComponent<StoreReviewManager>().RequestReview();
        Debug.Log("レビュー画面表示");
        }*/
    }

    //れんしゅうボタンかテストボタンかで分岐します
    //どちらのボタンでもModeMenuPanelは表示されます、案内テキストがれんしゅうとテストとわかれています
    
    public void SelectRenshuu()//れんしゅうボタンを押した場合
    {
        SoundManager.instance.PlaySEButton();//SoundManagerからPlaySE0を実行
        GameManager.singleton.isRenshu = true;//練習モードの時の分岐
        //renshuuButtonでisPressedをtrueにする

        SoundManager.instance.PlayBGM("ModeMenuPanel");
      
    }
    public void SelectTest()//テストボタンを押した場合
    {
        SoundManager.instance.PlaySEButton();//SoundManagerからPlaySE0を実行
        //testButtonだとisPressedをfalseにする
        GameManager.singleton.isRenshu = false;//テストモードの時の分岐

         SoundManager.instance.PlayBGM("ModeMenuPanel");
       
    }
    //Gameシーンを選択したい場合（）
    public void GamePlayButtonClicked()
    {
        SoundManager.instance.PlaySEButton();
        // GameManagerのmathsTypeを設定
        GameManager.singleton.currentMathsType = GameManagerMathsType.multiplicationTest;

        // ここに必要な処理を追加する

        // 例：SceneManager.LoadScene("Game");
        //     DOTween.KillAll();
    }
    //段をセレクトする
    public void SelectDan(int buttonname)//段を選ぶボタンのScriptです。OnClickでボタンの名前を取得します
    { 
       switch (buttonname)
            {
                case 1:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 1;
                    // テストボタンからの2段でcurrentMode2を選択してMathAndScript.csに
                    break;

                case 2:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 2;
                
                   break;

                case 3:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 3;
                break;

                case 4:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 4;
                
                break;

                case 5:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 5;
               
                   break;

                case 6:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 6;
                
                break;

                case 7:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 7;
               
                break;

                case 8:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 8;
                
                break;

                case 9:
                SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                GameManager.singleton.currentMode = 9;
                
                break;
                case 10:
                    SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                    GameManager.singleton.currentMode = 10;
               
                    break;

                case 11:
                    SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                    GameManager.singleton.currentMode = 11;
                
                    break;

                case 12:
                    SoundManager.instance.PlaySE11Button3();//SoundManagerからSEButtonを実行
                    GameManager.singleton.currentMode = 12;
                    break;
               }

       StartCoroutine(SetGame());
    }

    IEnumerator SetGame()
    {
        yield return new WaitForSeconds(0.4f); // 0.3秒待機
        gameMenuPanel.SetActive(false);
        gameQuesPanel.SetActive(true);
        coinText.text = GameManager.singleton.coinNum.ToString();
        yield return null; // コルーチンを終了する
        _doGStartPanelRotate.DeleyRotetePanel();//スタートパネルからの出題
    }

    }

