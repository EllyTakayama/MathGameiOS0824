using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HaloRotate : MonoBehaviour
{
    public GameObject haloImage;
    // Start is called before the first frame update
    void Start()
    {
        Flash18();
        /*
        
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 3f,RotateMode.FastBeyond360)
    .SetEase(Ease.OutCubic);*/
    //Debug.Log("回転");
    //ChangeAlphaTo1();
    }
    public void Flash18(){
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 6f,
        RotateMode.FastBeyond360)
        .SetDelay(0.2f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetId("idFlash18");
        Debug.Log("idFlash18");
        ;  
        
    }

    public void ChangeAlphaTo1()
    {
        Image image;
        image = GetComponent<Image>();
        DOTween.ToAlpha(
            () => image.color,
            color => image.color = color,
            1, // alpha値を1(255/255)に
            0 //0秒で
        );
    }
    
}
