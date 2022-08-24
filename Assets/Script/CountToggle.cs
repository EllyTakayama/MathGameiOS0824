using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountToggle : MonoBehaviour
{
     public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御
    
    public int T2Count;
    // Start is called before the first frame update
    void Start()
    { canAnswer = true;
        
    }
    public void ToggleOnClick(){
        if (canAnswer == false)
        {
            return;//canAnswerがfalseなら正誤判定は実行しない
        }
        canAnswer = false;//bool canAnswerがfalseの時繰り返し、DelayResetを0.01秒ごに実行
        Invoke("DelayReset", 0.01f);
        Debug.Log("T2Click");
        T2Count++;
        Debug.Log("T2"+T2Count);
    }
     void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
