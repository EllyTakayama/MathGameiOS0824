using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//20221024

public class SoundToggle : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle seToggle;
    public Toggle test9Toggle;
    
     public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御

    // Start is called before the first frame update
    void Start()
    {
        canAnswer = true;
        BGMLoad();
        SELoad();
        
    }

    public void OnClickBGMToggle(){

        if (canAnswer == false)
        {
            return;//canAnswerがfalseなら実行しない
        }
        canAnswer = false;//bool canAnswerがfalseの時繰り返し、DelayResetを0.01秒ごに実行
        Invoke("DelayReset", 0.01f);
        if (bgmToggle.isOn == false){
             SoundManager.instance.BGMmute();
            ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
            Debug.Log("スタートbgmToggle" +bgmToggle.isOn);
            } 
        else if(bgmToggle.isOn ==true){
        SoundManager.instance.UnmuteBGM();
        ES3.Save<bool>("BGM_OnOf", bgmToggle.isOn);
       Debug.Log("スタートbgmToggle" +bgmToggle.isOn);
        }
       
    }

    public void OnClickSEToggle(){
        
        if (seToggle.isOn ==false){
           SoundManager.instance.SEmute(); 
            ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
           Debug.Log("スタートseToggle" +seToggle.isOn);
            } 
        else{seToggle.isOn =true;
        SoundManager.instance.UnmuteSE();
        ES3.Save<bool>("SE_OnOf", seToggle.isOn); 
        
        Debug.Log("スタートseToggle" +seToggle.isOn);
        }
     
    }
      void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }



    public void BGMLoad(){
     bgmToggle.isOn = ES3.Load<bool>("BGM_OnOf",true);
      if (bgmToggle.isOn ==true){
             SoundManager.instance.UnmuteBGM();
             }
        else{
            SoundManager.instance.BGMmute();
        }


    }

    public void SELoad(){
      seToggle.isOn  = ES3.Load<bool>("SE_OnOf",true);
      if (seToggle.isOn ==true){
             SoundManager.instance.UnmuteSE();
             }
        else{
            SoundManager.instance.SEmute();
        }
    }
    
}

