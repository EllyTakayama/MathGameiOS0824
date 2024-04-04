using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HaloRotate : MonoBehaviour
{
    void Start()
    {
        Flash18();

    }
    public void Flash18()
    {
        //transform.eulerAngles = new Vector3(0, 0, 0);
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 8f,
        RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)//途切れのない回転のため
            .SetLoops(-1)
            .SetLink(gameObject);
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
