using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] private GameObject fruitsEffect;
    [SerializeField] private GameObject gameOverMarkText;

    void Start()
    {
        fruitsEffect.SetActive(false);
        gameOverMarkText.SetActive(true);
    }
    public void DoGameOverPanel()
    {
        gameOverMarkText.SetActive(true);
        gameOverPanel.GetComponent<RectTransform>()
        .DOAnchorPos(new Vector2(0, 0), 1.5f)
    .SetEase(Ease.OutBack)
    .SetLink(gameObject)
    .OnComplete(() =>
            {
                gameOverMarkText.GetComponent<DoButton>().OnButtonClick();
                    FoodGenerator.instance.Spawn();
                    SoundManager.instance.PlaySE3();
                    FoodGenerator.instance.isOneTimeFood = true;
               
            });

        SoundManager.instance.PlayBGM("GameOverPanel");

    }
    public void SetFruitEffect()
    {
        fruitsEffect.SetActive(true);
    }

}
