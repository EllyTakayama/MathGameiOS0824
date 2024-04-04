using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//TopPanelのCloudをDOTweenで動かす0908

public class CloudMove : MonoBehaviour
{
    [SerializeField] private float moveDistance = 120f; // 移動距離
    [SerializeField] private float moveDuration = 2f; // 移動時間
      void Start()
      {
          CloudUpDown();
      }
    public void CloudUpDown()
    {
        DOTween.Sequence()
            .Append(transform.DOLocalMoveY(-moveDistance, moveDuration))
            .Append(transform.DOLocalMoveY(moveDistance, moveDuration))
            .SetRelative()
            .SetLink(gameObject)
            .SetLoops(-1, LoopType.Restart)
            ;
    }
}
