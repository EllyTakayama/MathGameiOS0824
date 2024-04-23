using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;//DoTweenを使用する記述
//0908　練習画面でかけざん表を見るスクリプトを追加
using UniRx;
using TMPro;

/// <summary>
/// MathAndScriptのGUIマネージャーです
/// </summary>

public class GGUIManager1 : MonoBehaviour {
    public Text markText;
    public Text gameOverMarkText;
    public Text countText;
    private int currentMode;
    //public Text messageText;
    public GameObject AdMobManager;
    [SerializeField] private AdMobBanner _adMobBanner;
    [SerializeField] private AdMobInterstitial _adMobInterstitial;
    [SerializeField] private AdMobGameReward _adMobGameReward;
    [SerializeField] private GameObject playerFlyPiyo;//ピヨのオンオフ
    
	// Use this for initialization
	void Start ()
    {
         if (GameManager.singleton != null)
        {
            currentMode = GameManager.singleton.currentMode;
        }

         switch (GameManager.singleton.currentMathsType)
         {
             case GameManagerMathsType.multiplicationRenshuu:
                 // multiplicationRenshuu の処理をここに書く
                 //quesCountText.text = TestToggle.testQuestion.ToString();
                 break;

             case GameManagerMathsType.multiplicationTest:
                 // multiplicationTest の処理をここに書く
                 //quesCountText.text = "9";
                 break;

             case GameManagerMathsType.multiplicationMusikui:
                 // multiplicationTest の処理をここに書く
                 break;
         }
         /*
         if(GameManager.singleton.currentMode>10){//ちから試し問題
            //quesCountText.text = TestToggle.testQuestion.ToString();
            tableButton.SetActive(false);

        }
        else　if(GameManager.singleton.currentMode<10) {//練習問題
            //quesCountText.text = "9" ;
            tableButton.SetActive(true);
        }
        else
        {
            
        }*/
         
           //messageText.enabled = true;
	}

        public void ResetCounts()
{
    GameManager.singleton.currentScore = 0;
    GameManager.singleton.currentCount = 1;
  
}

    //RenshuuシーンのリトライButton
    public void RetryButton()
    {
        ResetCounts();
        DOTween.KillAll();
        GameManager.singleton.SceneCount++;
        GameManager.singleton.SaveSceneCount();
        Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
        int IScount = GameManager.singleton.SceneCount;
        _adMobBanner.DestroyAd();
        _adMobGameReward.DestroyAd();
        if(GameManager.singleton.SceneCount > 0 && GameManager.singleton.SceneCount % 3 ==0){

            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }
        _adMobInterstitial.DestroyAd();
        if(GameManager.singleton.currentMathsType == GameManagerMathsType.multiplicationRenshuu)
        {
            EasySaveManager.singleton.Save();
            SceneManager.LoadScene("Renshuu");

        }else if (GameManager.singleton.currentMathsType == GameManagerMathsType.multiplicationTest)
        {
            SceneManager.LoadScene("Game");
        }

    }

    public void MenuBackButton()
    {
        ResetCounts();
        SoundManager.instance.PlaySEButton();//SoundManagerからSEButtonを実行
          Invoke("MenuBackMove",0.3f);
       
    }
    void MenuBackMove(){
        EasySaveManager.singleton.Save();
        DOTween.KillAll();
        /*
        GameManager.singleton.SceneCount++;
        GameManager.singleton.SaveSceneCount();
        Debug.Log("SceneCount"+GameManager.singleton.SceneCount);
        int IScount = GameManager.singleton.SceneCount;
        if(GameManager.singleton.SceneCount > 0 && GameManager.singleton.SceneCount % 3 ==0){

            AdMobManager.GetComponent<AdMobInterstitial>().ShowAdMobInterstitial();
            return;
        }*/
        //SoundManager.instance.PlayBGM("ModeMenuPanel");
        SceneManager.LoadScene("Menu");
    }
}



