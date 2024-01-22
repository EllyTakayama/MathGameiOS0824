using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//gradePiyoを拡大縮小するDoTween設定

public class DoGradePiyo : MonoBehaviour
{
    [SerializeField] private Image gradePiyo;
    // Start is called before the first frame update
     /*
    1振動時の最大角度 2	トゥイーン時間 3振動数 4振動する範囲
    transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 2f, 1, 0.5f)
    */
    public void CallGradePiyo()
    {
        transform.DOPunchScale(new Vector3(0.5f, 0.5f,0), 1.3f,3,0.6f)
            .SetLink(gameObject)
            ;   
    }
}
