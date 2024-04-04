using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//Button　DOScaleバージョン
using Sirenix.OdinInspector; // Odin Inspectorを追加

public class DOTextBounceAnim : MonoBehaviour
{
    //[SerializeField] private Text textComponent;
    [SerializeField] private RectTransform uiElement; // アニメーションを適用するUI要素
    [SerializeField] private float bounceDistance = 20f; // 上下にバウンドする距離
    [SerializeField] private float duration = 0.5f; // アニメーションの時間
    private Vector2 defaultPosition;
    void Start()
    {
        // デフォルトの位置を取得
        //defaultPosition = textComponent.rectTransform.anchoredPosition;
        defaultPosition = uiElement.anchoredPosition;
    }
    [Button("TextBounce")]
    public void TextBounce()
    {
        // 上下にバウンドするTweenを作成
        uiElement.DOJumpAnchorPos(uiElement.anchoredPosition + new Vector2(0, bounceDistance), bounceDistance, 1, duration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo); // ループするように設定
        // 上下にバウンドするTweenを作成
        /*
        Sequence sequence = DOTween.Sequence();
        sequence.Append(textComponent.rectTransform.DOAnchorPosY(bounceDistance, duration / 2).SetEase(Ease.InOutQuad)); // 上方向への移動
        sequence.Append(textComponent.rectTransform.DOAnchorPosY(-bounceDistance, duration).SetEase(Ease.InOutQuad)); // 下方向への移動
        sequence.SetLoops(-1, LoopType.Restart); // ループするように設定
        */
    }
    
    [Button("ResetTextBounce")]
    public void ResetTextBounce()
    {
        // 初期位置に戻す
        uiElement.anchoredPosition = defaultPosition;
    }
}
