using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiyoGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // PiyoがAnsButton配列のButtonにぶつかった場合
        if (other.CompareTag("Button"))
        {
            // AnsButton配列のButtonのインデックスを取得
            int buttonIndex = GetButtonIndex(other.gameObject);

            // GUIManagerオブジェクトにアタッチされたGCheckButtonスクリプトを取得
            GCheckButton gCheckButton = FindObjectOfType<GCheckButton>();

            // GCheckButtonスクリプトが存在し、buttonIndexが有効な値であれば、CheckTheTextofButtonを呼び出す
            if (gCheckButton != null && buttonIndex != -1)
            {
                gCheckButton.CheckTheTextofButton(buttonIndex);
            }
        }
    }

    // AnsButton配列のButtonのインデックスを取得するメソッド
    private int GetButtonIndex(GameObject buttonObject)
    {
        GCheckButton gCheckButton = FindObjectOfType<GCheckButton>();
        if (gCheckButton != null)
        {
            Button[] ansButtons = gCheckButton.AnsButtons;
            for (int i = 0; i < ansButtons.Length; i++)
            {
                if (ansButtons[i].gameObject == buttonObject)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}
