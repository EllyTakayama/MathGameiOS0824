using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween
using Sirenix.OdinInspector; //Odin

public class DOStartPanelRotate : MonoBehaviour
{
    Vector3 initialPosition;  // 追加: 初期位置を保存する変数
    
    void Start()
    {
        initialPosition = transform.localPosition;
        Debug.Log($"初期時の取得{initialPosition}");
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
        transform.DORotate(finalRotation, 0.8f);
    }

    [Button("StartPanelOut実行")]　//←[Button("ラベル名")]
    void MovePanel()
    {
        // 初期のRotation
        Vector3 initialRotation = new Vector3(90, 0, 0);
        // アニメーションの設定
        transform.DOLocalMoveX(-1500f, 0.8f);
        

    }
}
