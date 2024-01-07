using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween
using Sirenix.OdinInspector; //Odin

public class DOStartPanelRotate : MonoBehaviour
{
    Vector3 initialPosition;  // 追加: 初期位置を保存する変数
    private Vector3 finalRotation;
    private Sequence resetRotation;
    void Start()
    {
        initialPosition = transform.localPosition;
        
        Debug.Log($"初期時の取得{initialPosition}");
        //スタートから0.2秒後にRotatePanelを呼び出す
        DeleyRotetePanel();
    }

    public void DeleyRotetePanel()
    {
        Invoke("RotatePanel",0.2f);
    }
    [Button("StartPanelIn実行")]　//←[Button("ラベル名")]
    void RotatePanel()
    {
        // 初期位置に戻す
        transform.localPosition = initialPosition;
        // 初期のRotation
        Vector3 initialRotation = new Vector3(90, 0, 0);

        // 終了時のRotation
        Vector3 finalRotation = new Vector3(0, 0, 0);

        // アニメーションの設定
        transform.rotation = Quaternion.Euler(initialRotation);
        transform.DORotate(finalRotation, 0.8f)
            .SetLink(gameObject)
            .OnComplete(() => MovePanelDelayed());
    }

    // 0.2秒後にMovePanelを実行するメソッド
    void MovePanelDelayed()
    {
        StartCoroutine(DelayedMovePanel(0.2f));
    }

    IEnumerator DelayedMovePanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        MovePanel();
    }

    [Button("StartPanelOut実行")]　//←[Button("ラベル名")]
    void MovePanel()
    {
        // 初期のRotation
        Vector3 initialRotation = new Vector3(90, 0, 0);
        // アニメーションの設定
        transform.DOLocalMoveX(-1500f, 0.4f).SetLink(gameObject).OnComplete(() => RotatePanelComplete(initialRotation));
    }
    private void RotatePanelComplete(Vector3 finalRotation)
    {
        MathAndAnswer.instance.MathsProblem();
        // アニメーション完了後の処理
        transform.DORotate(finalRotation,0);
        
    }
}
