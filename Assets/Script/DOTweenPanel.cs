using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//DoTweenでgradePanelのアニメーションを実行するスクリプト0901
public class DOTweenPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] private GameObject gradePanel;
    [SerializeField] private GameObject gradePiyoSprite;
    [SerializeField] private Image gradeImage;
    [SerializeField] private Sprite[] grades;//Grade画像の差し替え用スプライト1,2,3,4,5の5要素
    [SerializeField] private Text gradeText;

    [SerializeField] private DoGradeImage _doGradeImage;
    [SerializeField] private DoGradePiyo _doGradePiyo;

    [SerializeField] private ParticleManager _particleManager;//パーティクル呼び出し
    private Vector3[] particlePositions;//パーティクルを生成する位置
    [SerializeField] private DoResultAnswerPanel _doResultAnswerPanel;//AnswerPanelのDOTween

    void Start()
    {
        gradePanel.SetActive(false);
        gradeImage.enabled = false;
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
    public void DoGradeCall()
    {
        //gradePanel.SetActive(true);
        Invoke("SetGradePanel", 0.8f);
        SoundManager.instance.PlaySE12GradePanel();
        Invoke("DoGradePanel", 1.5f);
    }

    void SetGradePanel()
    {
        gradePanel.SetActive(true);
        if (GameManager.singleton.currentMode > 10)
        {
            gradeText.text = "ちからだめし\nがんばったね";
        }
        else
        {
            gradeText.text = "れんしゅう\nがんばったね";
        }
        GradeImage();
    }
    
    void DoGradePanel()
    {
        StartCoroutine(DoGradePanelCoroutine());
    }
    // DoGradePanel メソッド内でコルーチンを使用して0.3秒待機
    private IEnumerator DoGradePanelCoroutine()
    {
        SoundManager.instance.PlaySE12GradePanel();

    _doGradePiyo.CallGradePiyo();
    yield return new WaitForSeconds(0.3f); // 0.3秒待機
    //パーティクルの紙吹雪を再生        
    for (int i = 0; i < 4; i++)
    {
        _particleManager.PlayParticle(i, particlePositions[i]);

        yield return new WaitForSeconds(0.1f); // 0.1秒待機
    } 
        yield return new WaitForSeconds(0.3f); // 0.3秒待機
        GradeImage();
        gradeImage.enabled = true;
        gradeImage.GetComponent<DoGradeImage>().DoImageChange();

        _doResultAnswerPanel.SetAnsResultPanel();

        yield return null; // コルーチンを終了する

    }

    void GradeImage()
    {
        //gradeImage.enabled = true;
        //gradeImage.GetComponent<DoGradeImage>().DoImageChange();
        if (GameManager.singleton.currentScore == 9)
        {
            //9点満点
            gradeImage.sprite = grades[0];
            SoundManager.instance.PlaySE5End1();
        }
        if ((GameManager.singleton.currentScore < 9) && (GameManager.singleton.currentScore > 6))
        {
            //7-8点
            gradeImage.sprite = grades[1];
            SoundManager.instance.PlaySE6End2();
        }
        if ((GameManager.singleton.currentScore < 7) && (GameManager.singleton.currentScore > 3))
        {
            //4-6点
            gradeImage.sprite = grades[2];
            SoundManager.instance.PlaySE7End3();
        }
        if ((GameManager.singleton.currentScore < 4) && (GameManager.singleton.currentScore > 0))
        {
            //1-3点
            gradeImage.sprite = grades[3];
            SoundManager.instance.PlaySE8End4();
        }
        if (GameManager.singleton.currentScore == 0)
        {
            //0点
            SoundManager.instance.PlaySE8End4();
            gradeImage.sprite = grades[4];
        }
    }


}
