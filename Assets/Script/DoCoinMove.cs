using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoCoinMove : MonoBehaviour
{
    public Transform coinFrame;

    private void Start()
    {
        // ゴールであるcoinFrameのx座標をランダムに設定
        float randomX = Random.Range(coinFrame.localPosition.x - 100f, coinFrame.localPosition.x);
        Vector3 randomPosition = new Vector3(randomX, coinFrame.localPosition.y, coinFrame.localPosition.z);
        // Coinの現在位置からcoinFrameの位置までのローカルパスを設定
        Vector3[] path = { transform.localPosition, randomPosition };
        // DOTweenを使用してローカルパスの移動を実行
        transform.DOLocalPath(path, 0.5f, PathType.Linear).SetEase(Ease.Linear)
            .SetLink(gameObject)
            .OnComplete(() => Destroy(gameObject)); // 移動完了時にGameObjectを破棄する
    }
}
