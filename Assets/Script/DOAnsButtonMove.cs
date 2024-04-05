using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class DOAnsButtonMove : MonoBehaviour
{
    [SerializeField] private GameObject[] AnsButtons; // 移動させるButtonの配列
    public RectTransform AnsButtonParent;
    public Transform targetObject; // 移動の目標となるオブジェクト
    public float initialMoveSpeed = 8.0f; // DOTweenによる移動スピード値が大きくなるほどゆっくり移動する
    // DOTweenによる移動スピードの増加量
    public float moveSpeedIncrease = 0.2f;

    private void Start()
    {
        // 初期スピードでButtonを移動させる
       
    }
    // Buttonを移動させる関数
    [Button("MoveButton実行")]
    public void DoMoveAnsButton()
    {
        MoveButtons(initialMoveSpeed);
    }
    
    //AnsButtonsを移動させる関数
    private void MoveButtons(float moveSpeed)
    {
        //Debug.Log("MoveTween 開始前");
// AnsButtonParent の y 座標を piyoObject の y 座標に Tween でアニメーションさせる
        AnsButtonParent.DOMoveY(targetObject.position.y, moveSpeed)
            .SetEase(Ease.Linear)
            .SetLink(gameObject)
            //.OnComplete(() => Debug.Log("MoveTween 終了"))
            ;
    }
    // AnsButtonの動きを一時停止する関数
    [Button("Button一時停止")]
    public void PauseButtonMovement()
    {
        // AnsButtonParentのTweenを一時停止する
        AnsButtonParent.DOPause();
    }
    
    // AnsButtonの動きを再開する関数
    [Button("Button再開")]
    public void ReStartAnsButton()
    {
        ResumeButtonMovement(initialMoveSpeed);
    }
    
    public void ResumeButtonMovement(float moveSpeed)
    {Debug.Log("MoveTween 再開");
        // AnsButtonParentのTweenを再開し、新しいスピードで再生する
        AnsButtonParent.DOPlayForward();
    }
}
