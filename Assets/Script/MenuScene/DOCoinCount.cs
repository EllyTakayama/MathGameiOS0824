using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DOCoinCount : MonoBehaviour
{
    public TextMeshProUGUI coinAddText;
    public float duration = 2f; // カウンターのアニメーション時間

    public void CountCoin(){
        coinAddText.DOCounter(GameManager.singleton.beforeTotalCoin,GameManager.singleton.totalCoin,duration)
            .SetLink(gameObject);
        Debug.Log("DOCoinCount");
    }
}
