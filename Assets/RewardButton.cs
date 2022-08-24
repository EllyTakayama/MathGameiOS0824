using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//0916
using TMPro;

public class RewardButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public void Reward(){
          transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.5f)
        .SetLoops(5, LoopType.Restart);
        ;
    }
   
}
