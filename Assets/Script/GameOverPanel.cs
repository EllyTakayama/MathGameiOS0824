using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    
    public void DoGameOverPanel(){

        gameOverPanel.GetComponent<RectTransform>()   
        .DOAnchorPos(new Vector2(0,0), 1.5f)
    .SetEase(Ease.OutBack)
    ;
    DOTween.TweensById("idFlash18").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("idFlash18");
            });
    SoundManager.instance.PlayBGM("GameOverPanel");
     if (!FoodGenerator.instance.isOneTimeFood)
                {
                    FoodGenerator.instance.Spawn();
                    FoodGenerator.instance.isOneTimeFood = true;   
                    
                }

    }
   
}
