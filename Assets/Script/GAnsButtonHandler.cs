using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAnsButtonHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 衝突したオブジェクトがTagが"Player"であるか確認
        if (collider.CompareTag("Player"))
        {
            print("PlayerCollision");
            // 衝突したオブジェクトがButtonコンポーネントを持っているか確認
            Button button = GetComponent<Button>();
            if (button != null)
            {
                // ButtonのOnClickイベントをトリガーする
                button.onClick.Invoke();
            }
        }
        
    }
}
