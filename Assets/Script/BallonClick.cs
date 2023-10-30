using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BallonClick : MonoBehaviour
{
    public int balloonIndex;
    [SerializeField] private Sprite[] _brokenBallons;
    [SerializeField] private TextMeshProUGUI kuku;
    private BalloonObjectPool balloonObjectPool;
    void Start()
    {
        balloonObjectPool = FindObjectOfType<BalloonObjectPool>();
    }

    /*public void OnClick()
    {
        StartCoroutine((BreakB()));
    }*/
    //クリックした時に呼び出すコルーチン
    public void Init()
    {
        SoundManager.instance.PlaySE14BreakBalloon();
        StartCoroutine(BreakB());
        
    }
    //設定をリセットしてオブジェクトプールのリストに戻す
    public void Sleep()
    {
        StopAllCoroutines();
        //GetComponent<Image>().sprite = null;
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
        
    }
    IEnumerator BreakB()
    {
        SoundManager.instance.PlaySE14BreakBalloon();
        yield return new WaitForSeconds(0.2f);
        //Imageを破裂した風船のSpriteに差し替える
        GetComponent<Image>().sprite = _brokenBallons[balloonIndex];
        
        //scaleを大きくする
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f)
            .SetLink(gameObject);
        //Destroy(gameObject, 1.5f);
        yield return new WaitForSeconds(0.8f);
        Sleep();
    }
}
