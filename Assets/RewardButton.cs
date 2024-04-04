using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//0916
using TMPro;

public class RewardButton : MonoBehaviour
{
    public void Reward(){
          transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 1.0f)
              .SetLink(gameObject)
        .SetLoops(-1);
        ;
    }
   
}
