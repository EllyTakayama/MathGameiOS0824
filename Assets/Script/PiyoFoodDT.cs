using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PiyoFoodDT : MonoBehaviour
{
    public GameObject flyFoodPiyo;
    // Start is called before the first frame update
    void Start()
    {
        flyFoodPiyo.GetComponent<RectTransform>()
        .DOJumpAnchorPos(new Vector2(300,0), 20f, 5, 1f)
        .SetDelay(3f)
        .SetRelative()
    ;
    Debug.Log("jump");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        
  

}
