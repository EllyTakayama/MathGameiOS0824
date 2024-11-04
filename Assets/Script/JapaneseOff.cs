using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JapaneseOff : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject; // 非表示にしたいGameObjectを指定

    void Start()
    {
        // システム言語が日本語でない場合に非表示にする
        if (Application.systemLanguage != SystemLanguage.Japanese)
        {
            targetGameObject.SetActive(false);
        }
    }
}
