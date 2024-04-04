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
    //リセットするためのメソッド
    public void ResetEndImage()
    {
        rainbowImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
        rainbowImage.SetActive(false);
    }
    
    //AfterReward後に実行するメソッド
    public void DoEndImage(){
        //endText.text = " おなかいっぱい！";
        rainbowImage.SetActive(true);
        SoundManager.instance.PlaySE5End1();//ぷーい効果音
        rainbowImage.GetComponent<RectTransform>()  
        .DOAnchorPos(new Vector2(0,260f), 1.5f)
        //.SetRelative()
        .SetEase(Ease.OutBack)
        .OnComplete(() => {
            rainbowImage.GetComponent<DOTextBounceAnim>().TextBounce();
            SoundManager.instance.PlaySE17CoinOneceGet();//コインの音をならす
        });
        
    /*
     if(GameManager.singleton.currentScore>=9){
            endText.text = " すごいね！";
            SoundManager.instance.PlaySE5End1();//SoundManagerからPlaySE0を実行
        }
        else if((GameManager.singleton.currentScore<9)&&(GameManager.singleton.currentScore>6)){
             endText.text = "がんばったね！";
             SoundManager.instance.PlaySE6End2();//SoundManagerからPlaySE0を実行
        }
         else if((GameManager.singleton.currentScore<7)&&(GameManager.singleton.currentScore>3)){
             endText.text = "えらい！";
              SoundManager.instance.PlaySE7End3();//SoundManagerからPlaySE0を実行
        }
        else if((GameManager.singleton.currentScore<4)&&(GameManager.singleton.currentScore>=1)){
            endText.text = "まだまだ\nいけるよ";
              SoundManager.instance.PlaySE8End4();//SoundManagerからPlaySE0を実行
        }
         else if(GameManager.singleton.currentScore==0){
            endText.text = "ざんねんピヨ";
             SoundManager.instance.PlaySE9End5();//SoundManagerからPlaySE0を実行
             Instantiate(pickSkullEffect,gameOverPanel.transform,false);
        }*/

    }
    public void RewardCall(){
            RewardButton.SetActive(true);
            SoundManager.instance.PlaySE18();//Result効果音
        }

    
}
