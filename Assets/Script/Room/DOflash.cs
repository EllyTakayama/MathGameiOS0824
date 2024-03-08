using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;//0416更新
using Sirenix.OdinInspector; //Odin
public class DOflash : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    void Start()
    {
        Flash18();
    }
    */

    //練習・テストの初期値をリセットするメソッド
    [Button("FlashRotate実行")]　//←[Button("ラベル名")]
    public void Flash18(){
        //transform.eulerAngles = new Vector3(0, 0, 0);
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 6f,
        RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)//途切れのない回転のため
        .SetLoops(-1)
        .SetLink(gameObject)
        ;  
        
    }
    public void Flash360(){
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 1.8f,
        RotateMode.FastBeyond360)
        .SetLink(gameObject)
        .SetDelay(0.2f);  
        Debug.Log("flash");
    }

}
