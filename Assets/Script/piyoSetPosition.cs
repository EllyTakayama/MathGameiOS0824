using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PiyoSetPosition : MonoBehaviour
{
    private Vector2 defaultPosition; // 初期位置を保存する変数
    [SerializeField] private RectTransform preParentRect;//Gameポジション
    [SerializeField] private RectTransform startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を保存
        defaultPosition = startPosition.position;
        // スタート時にstartPositionの位置に移動
        if (startPosition != null)
        {
            transform.position = startPosition.position;
        }
    }

    // piyoの位置を指定した位置に設定するメソッド
    public void SetGamePosition()
    {
        if (preParentRect != null)
        {
            transform.position = preParentRect.position;
        }
        else
        {
            Debug.LogWarning("preParentRect is not assigned!");
        }
    }

    // piyoの位置を初期位置にリセットするメソッド
    [Button("ReSet Position")]
    public void ResetPosition()
    {
        transform.position = defaultPosition;
    }

    // InspectorからSetGamePositionメソッドを実行するためのボタン
    [Button("SetGame Position")]
    private void SetGamePositionButton()
    {
        // ここで任意の位置を指定してSetGamePositionメソッドを呼び出す
        SetGamePosition();
    }
}
