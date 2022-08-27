using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//0804
using TMPro;

public class EndImage : MonoBehaviour
{
    public GameObject endImage;
    public Text endText;
    public GameObject foodGeneratorCount;
    public GameObject RewardButton;
    public GameObject pickSkullEffect; 
    public GameObject gameOverPanel;
    public GameObject AdMobManager;
    public GameObject rainbowImage;
    


    // Start is called before the first frame update
    void Start()
    {
        endImage.SetActive(false);
        RewardButton.SetActive(false);
        rainbowImage.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
         if(endImage.activeSelf){
            return;
        }  
        /*if(AdMobManager.GetComponent<AdMobReward>().oyatsuReward == true){
            return;
        }*/
        
        if(foodGeneratorCount.GetComponent<FoodGenerator>().endCount < 0){
            endImage.SetActive(true);
            DoEndImage();
            
            if(AdMobManager.GetComponent<AdMobReward>().oyatsuReward == true){
            return;
        }
            Invoke("RewardCall",0.8f);
        }
        
    }

    public void DoEndImage(){
        
        if(AdMobManager.GetComponent<AdMobReward>().oyatsuReward == true){
        
        endText.text = " おなかいっぱい！";
        rainbowImage.SetActive(true);
        rainbowImage.GetComponent<RectTransform>()   
        .DOAnchorPos(new Vector2(0,260f), 1.5f)
        //.SetRelative()
        .SetEase(Ease.OutBack)
        ; 
        SoundManager.instance.PlaySE5End1();//SoundManagerからPlaySE0を実行
            return;
        }
        endImage.GetComponent<RectTransform>()   
        .DOAnchorPos(new Vector2(0,0), 1.5f)
        .SetEase(Ease.OutBack)
        ; 
    
     if(GameManager.singleton.currentScore==9){
            endText.text = " おなかいっぱい！";
            SoundManager.instance.PlaySE5End1();//SoundManagerからPlaySE0を実行
        }
        else if((GameManager.singleton.currentScore<9)&&(GameManager.singleton.currentScore>6)){
             endText.text = "おやつおいしいよ！";
             SoundManager.instance.PlaySE6End2();//SoundManagerからPlaySE0を実行
        }
         else if((GameManager.singleton.currentScore<7)&&(GameManager.singleton.currentScore>3)){
             endText.text = "おやつすき！";
              SoundManager.instance.PlaySE7End3();//SoundManagerからPlaySE0を実行
        }
        else if((GameManager.singleton.currentScore<4)&&(GameManager.singleton.currentScore>=1)){
            endText.text = "まだまだ\nたべたいな";
              SoundManager.instance.PlaySE8End4();//SoundManagerからPlaySE0を実行
        }
         else if(GameManager.singleton.currentScore==0){
            endText.text = "おなかすいた・・";
             SoundManager.instance.PlaySE9End5();//SoundManagerからPlaySE0を実行
             Instantiate(pickSkullEffect,gameOverPanel.transform,false);
             
        }

        
    //SoundManager.instance.StopBGM();
    //piyoボード表示時にBGMオフではなく、SEを追加しようかなと。

    }
    void RewardCall(){
            RewardButton.SetActive(true);
            RewardButton.GetComponent<RewardButton>().Reward();//DOTweenのアニメーション
        }

    
}
