using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//練習PanelのCountをDOTweenでカウントアップする

public class CountText : MonoBehaviour
{
    public GameObject countText;

    public void CountMove(){
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
/*
    public void CountNum(){
       // 表示する部分だけDOTweenで見せかける
        
    }
    
        DOTween.To(() => GameManager.singleton.currentScore, (value) => 
        GameManager.singleton.currentScore = value, count, 1f)
            .OnUpdate(()=> { countText.GetComponent<Text>().text = GameManager.singleton.currentScore.ToString(); });
    */
}
