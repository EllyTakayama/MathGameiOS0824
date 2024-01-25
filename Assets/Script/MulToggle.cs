using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulToggle : MonoBehaviour
{
    public static bool Table1_OnOf = true;//falseで×1　trueで×9
    // Start is called before the first frame update
    public Toggle mul1Toggle;//かける1からのスタート
    public Toggle mul9Toggle;


    void Start()
    {
        GameManager.singleton.LoadOrderJun();//isAsendingOrderかける数のロード
        if (GameManager.singleton.isAsendingOrder)
        {
            mul1Toggle.isOn = true;
        }
        else
        {
            mul9Toggle.isOn = true;
        }
    }
    public void OnClickToggle1(){
        Table1_OnOf = true;
        GameManager.singleton.isAsendingOrder = true;//かける数が1から出題される
        GameManager.singleton.SaveOrderJun();//isAsendingOrderをセーブする
        SoundManager.instance.PlaySE11Button3();
        //Debug.Log("1toggle"+mul1Toggle.isOn);//toggleのboolの状態をisOnで取得
        //Debug.Log("9toggle"+mul9Toggle.isOn);
        //Debug.Log("Table1_OnOf"+Table1_OnOf);
    }

    public void OnClickToggle9(){
        Table1_OnOf = false;
        GameManager.singleton.isAsendingOrder = false;//かける数が9から出題される
        GameManager.singleton.SaveOrderJun();//isAsendingOrderをセーブする
        SoundManager.instance.PlaySE11Button3();
        //Debug.Log("1toggle"+mul1Toggle.isOn);//toggleのboolの状態をisOnで取得
        //Debug.Log("9toggle"+mul9Toggle.isOn);
        //Debug.Log("Table1_OnOf"+Table1_OnOf);
    }
    
}
