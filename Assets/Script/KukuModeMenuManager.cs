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
    
    public void KukuHomeback(){
        GameManager.singleton.SceneCount++;
        GameManager.singleton.SaveSceneCount();
        Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
        int IScount = GameManager.singleton.SceneCount;
        if(IScount>0 && IScount%3 ==0){
            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        SceneManager.LoadScene("Menu");
    }
}
