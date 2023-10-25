using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallonClick : MonoBehaviour
{
    public int balloonIndex;
    [SerializeField] private Sprite[] _brokenBallons;
    [SerializeField] private TextMeshProUGUI kuku;
    
    public void OnClick()
    {
        StartCoroutine((BreakB()));
    }

    IEnumerator BreakB()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<Image>().sprite = _brokenBallons[balloonIndex];
        //bPrefabs1[num].GetComponent<DOScale>().BallonScale();

        Destroy(gameObject, 0.5f);
    }
}
