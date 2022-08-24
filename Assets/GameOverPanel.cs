using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class gameOverPanel : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.singleton.currentMode>2){
            GetComponent<RectTransform>()
        .DOAnchorPos(new Vector2(240f, 0), 0.6f)
    .SetEase(Ease.OutBack);
        }

    }
   
}
