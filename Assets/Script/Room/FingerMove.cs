using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class FingerMove : MonoBehaviour
{
    public RectTransform imageRectTransform;
    [SerializeField] private float goAnimTime = 0.5f;//アニメーションさせる時間の設定
    [SerializeField] private float backAnimTime = 0.3f;//アニメーションさせる時間の設定

    void Start()
    {
        FingerRotate();
    }
    [Button("FingerRotate実行")]
    public void FingerRotate()
    {
        if (imageRectTransform == null)
        {
            Debug.LogError("ImageのRectTransformが設定されていません。");
            return;
        }
        // Imageのz軸を0から25まで0.6秒かけて回転し、その後0.2秒静止してから0まで戻す
        Sequence sequence = DOTween.Sequence();
        sequence.Append(imageRectTransform.DORotate(new Vector3(0, 0, 25), goAnimTime)
                .SetEase(Ease.Linear))
            .AppendInterval(0.4f)
            .Append(imageRectTransform.DORotate(Vector3.zero, backAnimTime)
                .SetEase(Ease.Linear))
            .AppendInterval(0.2f)
            .SetLoops(-1, LoopType.Yoyo) // ヨーヨーのループ
            .SetLink(gameObject);
    }

    public void FingerOff()
    {
        gameObject.SetActive(false);
    }
}
