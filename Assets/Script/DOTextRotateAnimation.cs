using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DOTextRotateAnimation : MonoBehaviour
{
    public Text questionCountText;
    public Text correctCountText;

    /*void Start()
    {
        // アニメーションの開始
        StartCoroutine(StartTextRotationAnimation());
    }*/

    IEnumerator StartTextRotationAnimation()
    {
        // 初期の角度
        Quaternion initialRotation = Quaternion.Euler(0f, 0f, 0f);

        // 回転させる角度（縦に回転）
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 90f);

        // アニメーションの時間
        float duration = 1.0f;

        // DOTweenによるアニメーション
        questionCountText.rectTransform.DORotateQuaternion(targetRotation, duration)
            .SetEase(Ease.OutBounce) // イーズの設定（OutBounceは一例）
            .OnComplete(() => StartCoroutine(RotateCorrectCountText(targetRotation, duration)));

        yield return new WaitForSeconds(duration);

        // ここで他のアニメーションや処理を追加できます
    }

    IEnumerator RotateCorrectCountText(Quaternion targetRotation, float duration)
    {
        // DOTweenによるアニメーション
        correctCountText.rectTransform.DORotateQuaternion(targetRotation, duration)
            .SetEase(Ease.OutBounce) // イーズの設定
            .OnComplete(() => StartCoroutine(ResetRotation()));

        yield return new WaitForSeconds(duration);

        // ここで他のアニメーションや処理を追加できます
    }

    IEnumerator ResetRotation()
    {
        // 初期の角度に戻す
        Quaternion initialRotation = Quaternion.Euler(0f, 0f, 0f);

        // DOTweenによるアニメーション
        questionCountText.rectTransform.DORotateQuaternion(initialRotation, 1.0f);
        correctCountText.rectTransform.DORotateQuaternion(initialRotation, 1.0f);

        yield return null;
    }
}