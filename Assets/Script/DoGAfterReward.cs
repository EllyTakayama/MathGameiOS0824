using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

//5月19日更新

public class DoGAfterReward : MonoBehaviour
{
    [SerializeField] private Text rewardText;
    [SerializeField] private GameObject RewardPanel;
    [SerializeField] private GameObject RewardButton;
    [SerializeField] private GameObject RewardCoinImage;
    [SerializeField] private GameObject RewardflashImage;
    public TextMeshProUGUI coinAddText;
    //[SerializeField] private GameObject coinGenerator;//CoinPrefabを生成する場所
    [SerializeField] private GameObject SpinnerPanel;
    [SerializeField] private GameObject RegradePanel;
    [SerializeField] GameObject AdMobManager;
    //[SerializeField] private GachaManager _gachaManager;
    private string afRewardText;
    
    void Start()
    {
        const string tableName = "GachaScene";
        // ローカライズされた文字列を取得
        afRewardText = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "af_rewardText");
    }
    public void AfterReward(){
        rewardText.text = "";
        SpinnerPanel.SetActive(false);
        //SoundManager.instance.PlayPanelBGM("GradePanel");
        RewardButton.SetActive(false);
        RewardCoinImage.SetActive(false);
        RewardflashImage.SetActive(false);
        //Debug.Log("AfterReward,SpinPanel,"+SpinnerPanel.activeSelf);
        StartCoroutine(DoRewardPanel());
    }
    IEnumerator DoRewardPanel()
    { 
        yield return new WaitForSeconds(0.2f);
        DoRewardText();
        
        yield break;
    }
    public void DoRewardText(){
        rewardText.DOText(afRewardText
        , 0.5f)
        .OnComplete(Coinhoka)
        ;
        print("rewardText");
        
    }
    public void Coinhoka(){
        StartCoroutine(CoinMove());
        Debug.Log("coinHoka");
    }
    IEnumerator CoinMove()
    {   yield return new WaitForSeconds(0.1f);
        RewardCoinImage.SetActive(true);
        RewardflashImage.SetActive(true);
        RewardflashImage.GetComponent<DOflash>().Flash18();
       
       yield return new WaitForSeconds(0.2f);
       //coinGenerator.GetComponent<CoinGenerator>().SpawnRewardCoin();
       
       yield return new WaitForSeconds(2.0f);
       SoundManager.instance.StopSE();
       
       //RewardflashImage.SetActive(false);
       yield break;

    }
    //GameシーンのAdCloseボタン
    public void GCloseAdPanel()
    {
        RegradePanel.SetActive(false);
    }
    
    
}
