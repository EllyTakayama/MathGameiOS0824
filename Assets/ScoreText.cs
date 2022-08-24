using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//練習PanelのCountをDOTweenでカウントアップする

public class ScoreText : MonoBehaviour
{
    public GameObject markText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreMove(){
        DOTween.Sequence()
        .Append(
        transform.DOLocalMove(new Vector3(0, 20f, 0), 0.3f)
        .SetRelative(true)
        .SetEase(Ease.OutQuad))
        .Append(
        transform.DOLocalMove(new Vector3(0, -20f, 0), 0.1f)
        .SetRelative(true)
        .SetEase(Ease.OutQuad));
    }
}
