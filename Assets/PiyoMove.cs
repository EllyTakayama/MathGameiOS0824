using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PiyoMove : MonoBehaviour
{
    private Sequence piyoSequence; // トゥイーンのシーケンスを保持する変数
    // Start is called before the first frame update
    void Start()
    {
        PiyoMoveAnim();//piyoを上下にアニメーションさせる
        /*
         DOTween.Sequence()
        .Append(transform.DOLocalMoveY(100f, 2f))
        .Append(transform.DOLocalMoveY(-100f, 2f))
        .SetRelative()
        .SetLoops(-1, LoopType.Restart)
        ;
        */
    }
    public void PiyoMoveAnim()
    {
        // 既存のシーケンスがあれば停止
        if (piyoSequence != null)
        {
            piyoSequence.Kill();
        }

        // 新しいシーケンスを作成してアニメーションを設定
        piyoSequence = DOTween.Sequence()
            .Append(transform.DOLocalMoveY(100f, 2f))
            .Append(transform.DOLocalMoveY(-100f, 2f))
            .SetRelative()
            .SetLink(gameObject)
            .SetLoops(-1, LoopType.Restart);
    }
    // ピヨのアニメーションを停止するメソッド
    public void StopPiyoAnimation()
    {
        if (piyoSequence != null)
        {
            piyoSequence.Kill();
        }
    }
}
