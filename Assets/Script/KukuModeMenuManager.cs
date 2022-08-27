using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;//DoTweenを使用する記述

public class KukuModeMenuManager : MonoBehaviour
{
    public Button thisButton;
     public GameObject AdMobManager;
    // Start is called before the first frame update
    void Start(){
        if(
        GameManager.singleton.SceneCount==30||GameManager.singleton.SceneCount==80||
        GameManager.singleton.SceneCount==100||GameManager.singleton.SceneCount==130){
        AdMobManager.GetComponent<StoreReviewManager>().RequestReview();
        Debug.Log("レビュー画面表示");
        }
    }
    
    public void KukuHomeback(){
        GameManager.singleton.SceneCount++;
        GameManager.singleton.SaveSceneCount();
        Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
        int IScount = GameManager.singleton.SceneCount;
        if(IScount>0 && IScount%3 ==0){
            DOTween.KillAll();
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        SceneManager.LoadScene("Menu");
    }
}
