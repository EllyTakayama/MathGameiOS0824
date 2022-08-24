
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;//DoTweenを使用する記述
//stydyPanel内の BackButtonでメニュー画面に戻るスクリプト


public class backButton : MonoBehaviour
{
   [SerializeField] private GameObject studyPanel;
   [SerializeField] private GameObject settingPanel;
   [SerializeField] private GameObject playExPanel;
   [SerializeField] private GameObject TopMenuPanel;
   [SerializeField] private GameObject ModeMenuPanel;

    //public Button homeBackButton;
    public Button thisButton;
    
    public void BackButton()
    {
        SoundManager.instance.PlaySEButton();//SoundManagerからSEButtonを実行
        if(settingPanel == true){
            settingPanel.SetActive(false);
        }
        if(playExPanel == true){
            playExPanel.SetActive(false);
        }
        if(studyPanel == true){
            studyPanel.SetActive(false);
        }
        if(ModeMenuPanel == true){
            TopMenuPanel.SetActive(true);
            ModeMenuPanel.SetActive(false);
        }
            //Invoke("TableBackMove",0.5f);
     
    }

}