using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGameObject : MonoBehaviour
{
    // GameObjectの参照をインスペクターから設定できるようにします
    public GameObject targetObject;

    // システムの言語が英語の場合、targetObjectを非表示にします
    void Start()
    {
        if (targetObject != null && Application.systemLanguage != SystemLanguage.English)
        {
            targetObject.SetActive(false);
        }
    }
}
