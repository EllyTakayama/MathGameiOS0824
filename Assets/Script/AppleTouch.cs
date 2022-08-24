using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TouchScript.Gestures;
using DG.Tweening;//0619

public class AppleTouch : MonoBehaviour
{
   
    //public TapGesture tapGesture;//TapGesture配列を宣言。TapするGameObjectはinspectorから取得する

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    

   public void TapFood(){
    transform.DOLocalMove(new Vector3(0, 200f, 0), 1.5f)
    .SetEase(Ease.InOutQuart)
    .SetLink( gameObject )
  ;
     SoundManager.instance.PlaySE2();//SoundManagerからPlaySE2を実行
    }
　

}

