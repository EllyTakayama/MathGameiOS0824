using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesChangeCollider : MonoBehaviour
{
    [SerializeField] private GCheckButton _gCheckButton;//gCheckButton.csを取得
    private bool canChangeQues = true;
    private float cooldownTime = 1.0f; // 衝突後のクールダウン時間（秒）
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 衝突したオブジェクトがTagが"Player"かつcanChangeQuesがtrueか確認
        if (collider.CompareTag("Player") && canChangeQues)
        {
            _gCheckButton.ChangeQues();
            print("queschange");

            // 衝突後一定時間経過するまで変更を受け付けないようにする
            StartCoroutine(Cooldown());
        }
    }
    //連続で呼び出されないよう時間経過でboolを切り替えする
    private IEnumerator Cooldown()
    {
        canChangeQues = false;
        yield return new WaitForSeconds(cooldownTime);
        canChangeQues = true;
    }
}
