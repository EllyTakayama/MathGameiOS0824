using UnityEngine;
using System.Collections;
using System.IO; // this is required for input and output of data
using System;
using System.Runtime.Serialization.Formatters.Binary;//this is required to convert data into binary
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UniRx;
//MathAndScriptの方のゲームマネージャー

public class GameManager : MonoBehaviour {

    //we make static so in games only one script is name as this
    public static GameManager singleton;

    ReactiveProperty<int> currentCount1 = new ReactiveProperty<int>(0);
    //variable of gamedata
    private GameData data;

    //data not to store on device
   
    public int currentScore;
    public bool isGameOver;
    public int currentCount;
    public int currentMode;
    public bool test9;
    public bool test7;
    public bool test5;
    public int SceneCount;//インタースティシャル広告表示のためにScene表示をカウントしていきます
    public bool isRenshu;
    
   
    //public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御


    //data to store on device
    public int hiScore;
    public bool isMusicOn;
    public bool isGameStartedFirstTime;
    
    //it is call only once in a scene
    void Awake()
    {
        MakeSingleton();
        InitializeVariables();
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
    currentCount = 1;
    // Initialize the Google Mobile Ads SDK.
    MobileAds.Initialize(initStatus => { });
        //print("Admob初期化");
        LoadSceneCount();
       //RequestReview();
       //Debug.Log("Sceneカウント"+SceneCount);
    }

    
    void InitializeVariables()
    {
        Load();

        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {

            isGameStartedFirstTime = false;
            hiScore = 0;
            isMusicOn = true;

            data = new GameData();


            data.setHiScore(hiScore);
           
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);

            Save();

            Load();

        }
        else
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            
            hiScore = data.getHiScore();
        }


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



    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/Renshuu.dat");
            if (data != null)
            {
                data.setHiScore(hiScore);
                
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);

                bf.Serialize(file, data);

            }
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }



    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/Renshuu.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception e)
        { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }


}

[Serializable]
class GameData
{
    private int hiScore;
    
    private bool isGameStartedFirstTime;



    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }


    //HiScore
    public void setHiScore(int hiScore)
    {
        this.hiScore = hiScore;
    }

    public int getHiScore()
    {
        return hiScore;
    }

    

}
