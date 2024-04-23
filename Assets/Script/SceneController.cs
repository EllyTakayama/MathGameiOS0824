using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;//DoTweenを使用する記述

public class SceneController : MonoBehaviour
{
    public GameObject AdMobManager;
    [SerializeField] private AdMobBanner _adMobBanner;
    [SerializeField] private AdMobInterstitial _adMobInterstitial;
    [SerializeField] private AdmobGReward _admobGReward;
    public void ResetCounts()
    {
        GameManager.singleton.currentScore = 0;
        GameManager.singleton.currentCount = 1;
  
    }
    public void MenuBackMove(){
        //EasySaveManager.singleton.Save();
        ResetCounts();
        DOTween.KillAll();
        GameManager.singleton.SceneCount++;
        GameManager.singleton.SaveSceneCount();
        Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
        int IScount = GameManager.singleton.SceneCount;
        _adMobBanner.DestroyAd();
        _admobGReward.DestroyAd();
        if(GameManager.singleton.SceneCount > 0 && GameManager.singleton.SceneCount % 3 ==0){

            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        _adMobInterstitial.DestroyAd();
        SceneManager.LoadScene("Menu");
    }
}
