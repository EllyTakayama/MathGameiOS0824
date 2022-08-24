using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
//using TouchScript.Gestures;

public class FoodTouch : MonoBehaviour
{
   public GameObject apple;
   public GameObject banana;
   public GameObject orange;

    //public TapGesture tapGesture;


   public void TapFood(){
    transform.DOLocalMove(new Vector3(0, 200f, 0), 1.5f)
    .SetEase(Ease.InOutQuart)
    .SetLink( gameObject )
  ;
     SoundManager.instance.PlaySE2();//SoundManagerからPlaySE2を実行
    }
    

}
