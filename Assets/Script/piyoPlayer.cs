using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//piyoアニメーションをコントロールするスクリプトです

public class piyoPlayer : MonoBehaviour
{
   //piyoアニメーション設定

    private Animator anim = null;//animator anim を宣言
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {

     }
    //piyoのHappyメソッド、Damageメソッドを設定　
    //CheckButton.csのif(Compare)の正誤で呼び出してアニメーションを実行する

    public void Happy(){
        anim.SetTrigger("happy 0");
    }

    public void Damage(){
        anim.SetTrigger("damage 0");
    }
}
