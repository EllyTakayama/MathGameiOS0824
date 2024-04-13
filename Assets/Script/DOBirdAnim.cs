using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector; // Odin Inspectorを追加

public class DOBirdAnim : MonoBehaviour
{
    [SerializeField] private Transform birdTransform; // バードのTransform
    [SerializeField] private float jumpHeight = 0.5f; // ジャンプの高さ
    [SerializeField] private float jumpHeight2 = 0.2f; // ジャンプの高さ
    [SerializeField] private float rotateAmount = 30f; // 回転の量
    [SerializeField] private float shakeAmount = 0.5f; // 震える量
    //[SerializeField] private int maxJumpCount = 2; // 最大ジャンプ回数
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private float goTime = 0.5f;//Jump行きのアニメーションの時間
    [SerializeField] private float backTime = 0.4f;//Jump戻りのアニメーションの時間
    [SerializeField] private float durationTime = 0.5f;//Rotateアニメーションを実行する時間
    void Start()
    {
        // このコンポーネントがアタッチされたゲームオブジェクトの Transform を取得
        birdTransform = transform;
        // 初期の位置を保存
        initialPosition = birdTransform.position;
    }
    // BirdSliderから呼び出されるメソッド
    [Button("BirdAnims")]
    public void BirdAnims()
    {
        SoundManager.instance.PlaySE3();//ピヨピヨ
        // ランダムな値を生成してアニメーションを切り替える
        int animationIndex = UnityEngine.Random.Range(1, 5);
        switch (animationIndex)
        {
            case 1:
                JumpAnimation();
                break;
            case 2:
                RotateAnimation();
                break;
            case 3:
                DoubleJumpAnimation();
                break;
            case 4:
                ShakePositionAnim();
                break;
        }
    }

    // ジャンプアニメーション
    [Button("Jump1")]
    private void JumpAnimation(Action onComplete = null)
    {
        // ジャンプして指定した高さに移動するアニメーション
        birdTransform.DOMoveY(birdTransform.position.y + jumpHeight, goTime)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                // 元の位置に戻るアニメーション
                birdTransform.DOMoveY(initialPosition.y, backTime)
                    .SetLink(gameObject)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        onComplete?.Invoke(); // コールバックがあれば実行
                    });;
            });
    }

    // 回転アニメーション
    [Button("Rotate")]
    private void RotateAnimation()
    {
        // Sequenceを作成して回転アニメーションを順番に実行する
        Sequence rotateSequence = DOTween.Sequence();

        // 左にrotateAmount回転した後に元の角度に戻るアニメーションを追加
        rotateSequence.Append(birdTransform.DORotate(new Vector3(0, 0, rotateAmount), durationTime)
            .SetEase(Ease.InOutQuad))
            ;
            //.AppendInterval(0.1f) // 待機
            //.Append(birdTransform.DORotate(Vector3.zero, 0.5f)); // 元の角度に戻るアニメーションを追加

        // 右にrotateAmount回転した後に元の角度に戻るアニメーションを追加
        rotateSequence.Append(birdTransform.DORotate(new Vector3(0, 0, -rotateAmount), durationTime*2)
                .SetEase(Ease.InOutQuad))
            //.AppendInterval(0.1f) // 待機
            .Append(birdTransform.DORotate(Vector3.zero, durationTime)); // 元の角度に戻るアニメーションを追加

        // Sequenceを再生する
        rotateSequence.Play();
    }

    // 2回連続ジャンプアニメーション
    [Button("Jump2")]
    private void DoubleJumpAnimation()
    {
        // 1回目のジャンプ
        JumpAnimation(() =>
        {
            // 2回目のジャンプ
            JumpAnimation();
        });
    }

    [Button("ShakePosition")]
    private void ShakePositionAnim()
    {
        //時間、強さ、回数、手ぶれ値、スナップフラグ、dフェードアウト
        birdTransform.DOShakePosition(1.0f, 30f, 10, 1, false, true)
            .SetLink(gameObject);
    }
}
