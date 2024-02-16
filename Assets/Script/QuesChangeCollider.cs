using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesChangeCollider : MonoBehaviour
{
    [SerializeField] private GCheckButton _gCheckButton;//gCheckButton.csを取得
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 衝突したオブジェクトがTagが"Player"であるか確認
        if (collider.CompareTag("answer"))
        {
            _gCheckButton.ChangeQues();
            print("queschange");
        }
    }
}
