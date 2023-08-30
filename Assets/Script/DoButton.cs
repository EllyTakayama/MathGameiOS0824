using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//Button　DOScaleバージョン

public class DoButton : MonoBehaviour
{
    [SerializeField] private Vector3 defaultScale;
    void Start()
    {
        defaultScale = transform.localScale;
    }

    public void OnButtonClick()
    {
        transform.localScale = defaultScale;
        transform.DOPunchScale(Vector3.one * 1.05f, 0.3f, 1, 1.8f)
        .SetLink(gameObject);
        
        Debug.Log("ボタン！");
    }

    public void OnAnswerButtonClick()
    {
        transform.localScale = defaultScale;
        transform.DOPunchScale(Vector3.one * 1.05f, 0.3f, 1, 1f)
        .SetLink(gameObject);
        Debug.Log("回答ボタン！");
    }

}
