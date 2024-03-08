using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;//0223更新
using Sirenix.OdinInspector; //Odin

public class DOGachaBall : MonoBehaviour
{
    // Start is called before the first frame update
    
    //練習・テストの初期値をリセットするメソッド
    [Button("BallShake実行")]　//←[Button("ラベル名")]
    public void BallShake(){
        transform.DOPunchPosition(new Vector3(0f, 30f, 0), 1f, 5, 1f)
        .SetLink(gameObject)
        .SetDelay(0.1f);  
    }


}
