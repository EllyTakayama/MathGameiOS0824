using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoScaleButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private RectTransform buttonRectTransform;
    private Vector3 defaultScale;

    [SerializeField] private float animationDuration = 0.8f;
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float delayDuration = 0.6f; // 待機時間

    private void Start()
    {
        // Button の RectTransform を取得
        buttonRectTransform = button.GetComponent<RectTransform>();

        // Button のデフォルトのスケールを保存
        defaultScale = buttonRectTransform.localScale;

        // アニメーションを開始
        StartAnimation();
    }

    public void StartAnimation()
    {
        // ループするTweenを作成
        Sequence sequence = DOTween.Sequence();

        // デフォルトのスケールから scaleFactor までのアニメーション
        sequence.Append(buttonRectTransform.DOScale(defaultScale * scaleFactor, animationDuration));

        // 待機
        sequence.AppendInterval(delayDuration);

        // scaleFactor からデフォルトのスケールまでのアニメーション
        sequence.Append(buttonRectTransform.DOScale(defaultScale, animationDuration));
        // 待機
        sequence.AppendInterval(delayDuration);

        // ループ設定
        sequence.SetLoops(-1);

        // gameObject と Tween をリンク
        sequence.SetLink(gameObject);
    }
    
}
