﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;//DoTweenを使用する記述
//0908　練習画面でかけざん表を見るスクリプトを追加
using UniRx;
using TMPro;

/// <summary>
/// MathAndScriptのGUIマネージャーです
/// </summary>

public class GUIManager1 : MonoBehaviour {
    //ref to score text in game over panel
    public Text scoreOverText;
    //ref to hiscore text in game over panel
    public Text hiScoreOverText;
    public Text wrongAnswerText;
    //ref to game over panel
    public GameObject gameOverPanel;
    //ref to game over panel animator
    public Animator gameOverAnim;
    public Text markText;
    public Text gameOverMarkText;
    public TextMeshProUGUI fruitCountText;
    public Text countText;
    private int currentMode;
    public Text quesCountText;
    public GameObject tableButton;
    public GameObject RenshuuPanel;
    public GameObject imageTable1;
    public GameObject imageTable2;
    public GameObject imageTable3;
    public GameObject imageTable4;
    public GameObject imageTable5;
    public GameObject imageTable6;
    public GameObject imageTable7;
    public GameObject imageTable8;
    public GameObject imageTable9;
    public Text messageText;
    public GameObject AdMobManager;
    public GameObject[] imageTables;


	// Use this for initialization
	void Start ()
    {
         if (GameManager.singleton != null)
        {
            currentMode = GameManager.singleton.currentMode;
        }
        if(GameManager.singleton.currentMode>10){//ちから試し問題
            quesCountText.text = TestToggle.testQuestion.ToString();
            tableButton.SetActive(false);

        }
        else{//練習問題
            quesCountText.text = "9" ;
             tableButton.SetActive(true);
        }

        InitializeImageTables();
           messageText.enabled = true;

	}
    
    void InitializeImageTables()
    {
        foreach (var imageTable in imageTables)
        {
            imageTable.SetActive(false);
        }
    }

    public void ImageTable()
    {
        int index = GameManager.singleton.currentMode;
        SoundManager.instance.PlaySE10Button2();
        imageTables[index -1].SetActive(true);
        
    }
        public void CloseTablePre(){
            int index = GameManager.singleton.currentMode;
            SoundManager.instance.PlaySE10Button2();
            imageTables[index -1].SetActive(false);
    }

    public void SetFruitText()
    {
        if (GameManager.singleton.currentMode > 10)
        {
            gameOverMarkText.text = "おやつ " + GameManager.singleton.currentScore.ToString() + "こ ゲット！";
        }//力試し問題は正解数おやつゲット
        else
        {
            gameOverMarkText.text = "おやつ 3こ ゲット！";//練習問題
        }
    }


public void ResetCounts()
{
    GameManager.singleton.currentScore = 0;
    GameManager.singleton.currentCount = 1;
  
}


    //ref method to retry button
    public void RetryButton()
    {
        ResetCounts();
        if(GameManager.singleton.currentMode>10)
        {
            EasySaveManager.singleton.Save();
            SceneManager.LoadScene("Renshuu");
            MathAndAnswer.instance.MultiplicationMethodTest();
        }
        if(GameManager.singleton.currentMode<10)
        {
            EasySaveManager.singleton.Save();
            SceneManager.LoadScene("Renshuu");
            MathAndAnswer.instance.MultiplicationMethodRenshuu();
        }

        //MathAndAnswer.instance.MathsProblem();
        //SceneManager.LoadScene("Renshuu");//use this for 5.3 version
       GameManager.singleton.isGameOver = false;
    }

    
    public void MenuBackButton()
    {
        ResetCounts();
        SoundManager.instance.PlaySEButton();//SoundManagerからSEButtonを実行
          Invoke("MenuBackMove",0.3f);
       
    }
    void MenuBackMove(){
        EasySaveManager.singleton.Save();
        DOTween.KillAll();
        GameManager.singleton.SceneCount++;
        GameManager.singleton.SaveSceneCount();
        Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
        int IScount = GameManager.singleton.SceneCount;
        if(GameManager.singleton.SceneCount > 0 && GameManager.singleton.SceneCount % 3 ==0){

            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        SceneManager.LoadScene("Menu");
    }
    
}



