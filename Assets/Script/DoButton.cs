using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//Button　DOScaleバージョン

public class DoButton : MonoBehaviour
{
   
    public void OnButtonClick()
    {
       transform.DOPunchScale(Vector3.one * 1.05f, 0.3f, 1, 1f);
        ;
        Debug.Log("ボタン！");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
