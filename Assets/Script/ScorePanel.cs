using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//DoTweenでGameOverPanelをスライドいんさせるスクリプトだよ0608
public class ScorePanel : MonoBehaviour
{
 
     [SerializeField] private GameObject gradePanel;

    // Start is called before the first frame update
    void Start()
    {
   gradePanel.SetActive(false);
   Debug.Log("消えた");
    }

    // Update is called once per frame
    void Update()
    {
     if(GameManager.singleton.currentMode>10)
        {
        if(GameManager.singleton.currentCount > 2){
        gradePanel.SetActive(true);
    Debug.Log("表示");
    Debug.Log("GameManager.singleton.currentMode"+GameManager.singleton.currentMode);
    Debug.Log("GameManager.singleton.currentCount"+GameManager.singleton.currentCount);
   
    }
        }

    if(GameManager.singleton.currentMode<10){
        if(GameManager.singleton.currentCount >=9){
        gradePanel.SetActive(true);
    Debug.Log("表示");
    Debug.Log("GameManager.singleton.currentMode"+GameManager.singleton.currentMode);
    
   
    }
        }
    
    }  
    
}  