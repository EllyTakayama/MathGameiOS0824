using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//piyoのリワード広告後に呼び出されるメソッドImage

public class EndHalo : MonoBehaviour
{
    public GameObject endHaloImage;
    // Start is called before the first frame update
    void Start()
    {
        /*transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.6f)
        .SetRelative()
        ;*/
       transform.DOLocalRotate(new Vector3(0, 0, 360f), 1.5f,RotateMode.FastBeyond360)
        .SetEase(Ease.OutCubic)
        .SetLink(gameObject)
        .OnComplete(() => endHaloImage.SetActive(false))
        ;
    }

}
