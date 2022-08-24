using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//TopPanelのCloudをDOTweenで動かす0908

public class CloudMove : MonoBehaviour
{
      [SerializeField] private Image cloud1Image;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence()
        .Append(transform.DOLocalMoveY(-120f, 2f))
        .Append(transform.DOLocalMoveY(120f, 2f))
        .SetRelative()
        .SetLoops(-1, LoopType.Restart)
        ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
