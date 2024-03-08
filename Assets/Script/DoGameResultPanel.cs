using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoGameResultPanel : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel1;//最初のresultパネル
    [SerializeField] private RectTransform glowText;//
    [SerializeField] private GameObject piyo;//プレイヤーのひよこSprite
    [SerializeField] private GameObject[] starsOn;//星のオンオブジェクト
    [SerializeField] private DOGTweenPanel _dogTweenPanel;//2枚目のリザルトパネルを呼び出す
    [SerializeField] private RectTransform gradePreParent;//ピヨの移動先の位置
    public void SetResult()
    {
        Debug.Log("setResult");
        StartCoroutine(GameResult1());
    }
    
    IEnumerator GameResult1()
    {
        yield return new WaitForSeconds(0.4f);
        resultPanel1.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        // glowTextをX軸で0に移動
        glowText.DOAnchorPosX(0f, 0.8f);

        yield return new WaitForSeconds(0.4f);

        // スコアに応じて星の表示を設定
        if (GameManager.singleton.currentScore >= 9)
        {
            // 9以上の場合、全ての星をアクティブにする
            foreach (GameObject star in starsOn)
            {
                star.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                
            }
        }
        else if (GameManager.singleton.currentScore >= 4)
        {
            // 4以上9未満の場合、最初の2つの星をアクティブにする
            for (int i = 0; i < 2; i++)
            {
                starsOn[i].SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {
            // 3以下の場合、最初の星のみをアクティブにする
            starsOn[0].SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.4f);
        // piyoをY軸で1000移動
        //piyo.transform.DOMoveY(piyo.transform.position.y + 1000f, 0.6f);
        piyo.transform.DOMove(gradePreParent.position, 0.6f);
        yield return new WaitForSeconds(0.3f);
        resultPanel1.SetActive(false);
        _dogTweenPanel.DoGameGradeCall();
    }
}