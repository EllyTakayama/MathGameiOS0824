using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
//DoTweenでgradePanelのアニメーションを実行するスクリプト0901
public class DOGTweenPanel : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel; //リザルト画面1枚目
    [SerializeField] private GameObject gameQuesPanel;//ゲーム回答画面
    [SerializeField] private GameObject gradePanel;//リザルト2枚目

    //[SerializeField] private GameObject gradePiyoSprite;
    [SerializeField] private Image medalImage;
    [SerializeField] private Sprite[] medalGrades; //Grade画像の差し替え用スプライト1,2,3,4,5の5要素
    [SerializeField] private TextMeshProUGUI gradeText;

    //[SerializeField] private DoGradeImage _doGradeImage;
    //[SerializeField] private DoGradePiyo _doGradePiyo;

    [SerializeField] private ParticleManager _particleManager; //パーティクル呼び出し
    private Vector3[] particlePositions; //パーティクルを生成する位置
    [SerializeField] private DoGResultAnswerPanel _doResultAnswerPanel; //AnswerPanelのDOTween
    [SerializeField] private DoCoinAnim _doCoinAnim;//coinプレファブを生成するスクリプト

    void Start()
    {
        gradePanel.SetActive(false);
        medalImage.enabled = false;
        particlePositions = new Vector3[]
        {
            new Vector3(200, 1120, 0),
            new Vector3(500, 1120, 0),
            new Vector3(280, 1000, 0),
            new Vector3(550, 1000, 0)
        };
        //DoGradeCall();
    }

    //DoGradeCrallを呼び出す
    public void DoGameGradeCall()
    {
        Invoke("DoGradePanel", 0.1f);
    }


    void DoGradePanel()
    {
        StartCoroutine(DoGradePanelCoroutine());
    }

    // DoGradePanel メソッド内でコルーチンを使用して0.3秒待機
    private IEnumerator DoGradePanelCoroutine()
    {
        gameQuesPanel.SetActive(false);
        gradePanel.SetActive(true);
        gradeText.text = GameManager.singleton.coinNum.ToString();
        GradeImage();
        SoundManager.instance.PlaySE12GradePanel();
        /*
        yield return new WaitForSeconds(0.3f); // 0.3秒待機
        
        if (GameManager.singleton.currentScore > 0)
        {
            _doCoinAnim.SpawnRewardCoin();
        }*/
        yield return new WaitForSeconds(0.3f); // 0.3秒待機
        medalImage.enabled = true;
        //medalImage.GetComponent<DoGradeImage>().DoImageChange();
        
        //パーティクルの紙吹雪を再生        
        for (int i = 0; i < 4; i++)
        {
            _particleManager.PlayParticle(i, particlePositions[i]);

            yield return new WaitForSeconds(0.2f); // 0.1秒待機
        }
        
        yield return new WaitForSeconds(0.5f); // 0.3秒待機
        _doResultAnswerPanel.SetAnsResultPanel();
        yield return null; // コルーチンを終了する
    }


void GradeImage()
    {
        if (GameManager.singleton.currentScore>= 9)
        {
            //9点満点
            medalImage.sprite = medalGrades[0];
            SoundManager.instance.PlaySE5End1();
        }
        if ((GameManager.singleton.currentScore < 9) && (GameManager.singleton.currentScore >= 3))
        {
            //7-8点
            medalImage.sprite = medalGrades[1];
            SoundManager.instance.PlaySE6End2();
        }
        if (GameManager.singleton.currentScore <3 )
        {
            medalImage.sprite = medalGrades[2];
            SoundManager.instance.PlaySE6End2();
        }
    }

}

