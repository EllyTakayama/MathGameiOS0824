using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;//1014

public class EasySaveManager : MonoBehaviour
{

    public static EasySaveManager singleton;

    string modeMenu;//練習　テスト問題
    public int studyScore;//正解数
    DateTime TodayNow;//勉強した日付を取得
    //public Text studyRecord;//代入するようのテキスト
    public string str;
    public int table;//GameManager currentModeからテスト、練習の段を取得し代入
    

    // TODO:追加
    List<int> scoreList = new List<int>();
    List<string> modeList = new List<string>();
    List<DateTime> timeList = new List<DateTime>();

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
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        if(GameManager.singleton.currentMode>10){
            table = GameManager.singleton.currentMode-10;
            modeMenu = "力だめし "+ table.ToString();
        }
        else{
            table = GameManager.singleton.currentMode;
            modeMenu = "れんしゅう "+ table.ToString();
        }

        studyScore = GameManager.singleton.currentScore;
        TodayNow = DateTime.Now;

        ListSave();
    }

    void ListSave()
    {
        int saveNum = 20;//問題の保存数
        
        // 一度セーブデータを引っ張ってくる
        scoreList = ES3.Load("studyScoreList", new List<int>());
        modeList = ES3.Load("modeMenuList", new List<string>());
        timeList = ES3.Load("TodayNowList", new List<DateTime>());

        if(scoreList.Count > saveNum){
        
        scoreList.RemoveAt(0);
        modeList.RemoveAt(0);
        timeList.RemoveAt(0);

        // 現在のデータを追加
        scoreList.Add(studyScore);
        modeList.Add(modeMenu);
        timeList.Add(TodayNow);
        // リストを保存
        ES3.Save("studyScoreList", scoreList);
        ES3.Save("modeMenuList", modeList);
        ES3.Save("TodayNowList", timeList);
         Debug.Log("ifScoreList"+scoreList.Count);
       Debug.Log("ifsaveNum"+saveNum);
        }
        else
        {
        // 現在のデータを追加
        scoreList.Add(studyScore);
        modeList.Add(modeMenu);
        timeList.Add(TodayNow);
        // リストを保存
        ES3.Save("studyScoreList", scoreList);
        ES3.Save("modeMenuList", modeList);
        ES3.Save("TodayNowList", timeList);
         Debug.Log("elseScoreList"+scoreList.Count);
        Debug.Log("elsesaveNum"+saveNum);
        }
    }
    public void Load()
    {
        // TODO:追加
        scoreList = ES3.Load("studyScoreList", new List<int>());
        modeList = ES3.Load("modeMenuList", new List<string>());
        timeList = ES3.Load("TodayNowList", new List<DateTime>());
        
        
        if(ES3.KeyExists("studyScore"))
         studyScore = ES3.Load<int>("studyScore");

        if(ES3.KeyExists("modeMenu"))
         modeMenu = ES3.Load<string>("modeMenu");

        if(ES3.KeyExists("TodayNow"))
         TodayNow = ES3.Load<DateTime>("TodayNow"); 

        //Debug.Log("Load");
        

        TextDispley();

    }
  
    public void TextDispley(){
       str = ""; 
       for (int i= scoreList.Count-1; i >= 0; i--)
        {
            str += modeList[i].ToString()
                 + "の段　\n"
                 + " せいかい" + scoreList[i].ToString() + " 回" + "\n"
                 + "べんきょうした日 " + "\n"+timeList[i].Year.ToString() + 
                 "年 " + timeList[i].Month.ToString() + "月" + 
                 timeList[i].Day.ToString() +"日" + 
                 timeList[i].ToShortTimeString();
            str += "\n -------------------------------\n";
            //Debug.Log(scoreList.Count);
           
    
    
      }
    }
}    
