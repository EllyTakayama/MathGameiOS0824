using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;//DoTweenを使用する記述
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;


public class RModeMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject ModeMenuPanel;

    [SerializeField] private GameObject RenshuuPanel;//QuesPanel出題の

    [SerializeField] private DOStartPanelRotate _doStartPanelRotate;//StartImageを実行するスクリプト

    [SerializeField] private GameObject piyoRenshuuPanel;//出題パネルのpiyoのオンオフ

    [SerializeField] private BackgroundControll _backgroundControll;
    public Text markText;
    public Text countText;
    
    // Start is called before the first frame update
    void Start()
    {
        const string tableName = "RenshuuScene";
        // ローカライズされた文字列を取得
        string countText = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "countTextBefore");
        string scoreText  = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "scoreTextBefore");
    }
//RenshuuPanelのピヨはスタート時でオン
    public void PiyoSetOn()
    {
        piyoRenshuuPanel.SetActive(true);
    }
//RenshuuPanelのピヨはスタート時でオフ
    public void piyoSetOff()
    {
        piyoRenshuuPanel.SetActive(false);
    }
    public void SelectRenshuuDan(int ButtonNum)//れんしゅうボタンを押した場合
    {
        const string tableName = "RenshuuScene";
        // ローカライズされた文字列を取得
        string countText = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "countTextBefore");
        string scoreText  = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "scoreTextBefore");
        //SoundManager.instance.PlaySEButton();//SoundManagerからPlaySE0を実行
      
        switch (ButtonNum)
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
        ModeMenuPanel.SetActive(true);
        //SoundManager.instance.PlayBGM("Renshuu");
        GameManager.singleton.currentCount = 0;
        StartCoroutine(SetRenshuu());
        Debug.Log(GameManager.singleton.currentMode);
    }
    IEnumerator SetRenshuu()
    {
        yield return new WaitForSeconds(0.4f); // 0.3秒待機
        ModeMenuPanel.SetActive(false);
        RenshuuPanel.SetActive(true);
        _backgroundControll.ActiveBackground();//スクロールを表示させる
        //_piyoSetPosition.SetGamePosition();
        //coinText.text = GameManager.singleton.coinNum.ToString();
        //SoundManager.instance.PlayBGM("Renshuu");
        yield return null; // コルーチンを終了する
        _doStartPanelRotate.DeleyRotetePanel();//スタートパネルからの出題
    }
}
