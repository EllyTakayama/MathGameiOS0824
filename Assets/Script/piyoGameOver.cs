using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;//0619

//GameOverPanelにいるpiyoちゃんのスクリプト0906

public class piyoGameOver : MonoBehaviour
{
    public GameObject[] Effects;
    //public GameObject pickSkullEffect; 
    public GameObject gameOverPanel;
    [SerializeField] private Animator piyo;

    public int p;
    //public GameObject[] effectPrefab;//配列にGameObjectを代入inspector上でプレハブを指定
    private int n;//配列のスクリプトでiについてcs0103エラーが出たため宣言してます
    int[] ary = new int[7];


    // Start is called before the first frame update
    void Start()
    {
        //乱数を生成
        System.Random randomNum = new System.Random();

        //配列内1~9の値を格納
        for (int i = 0; i < ary.Length; i++)
        {
            ary[i] = i;
        }
        //配列をシャッフル
        for (int i = ary.Length - 1; i >= 0; i--)
        {
            //Nextメソッドは引数未満の数値がランダムで返る
            int j = randomNum.Next(i + 1);
            //tmpは配列間でやりとりする値を一時的に格納する変数
            int tmp = ary[i];
            ary[i] = ary[j];
            ary[j] = tmp;
            //Debug.Log(ary[i]);//ログに乱数を表示
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((GameManager.singleton.currentScore < 10) && (GameManager.singleton.currentScore > 0))
        {
            //collisitonにぶつかった相手の情報が入っている:food
            //int p = ary[n];//かける数にランダムに取得した配列データを代入
            piyo.SetTrigger("happy 0");
            int p = UnityEngine.Random.Range(0, Effects.Length - 1);
            Instantiate(Effects[p], gameOverPanel.transform, false);
            SoundManager.instance.PlaySE3();//SoundManagerからPlaySE3を実行
            Destroy(collision.gameObject);
            collision.transform.DOKill();
            //GameOverPanelにいるpiyoに当たったオブジェクトを破壊
            FoodGenerator.instance.Spawn();//FoodのSpawメソッドを呼び出してfoodを生成
                                           //Debug.Log("Effect");
        }
        /*
        else {
            Instantiate(pickSkullEffect,gameOverPanel.transform,false);
            SoundManager.instance.PlaySE3();//SoundManagerからPlaySE3を実行
            Destroy(collision.gameObject);//GameOverPanelにいるpiyoに当たったオブジェクトを破壊
            FoodGenerator.instance.Spawn();//FoodのSpawメソッドを呼び出してfoodを生成
            Debug.Log("Effect");
        }
        */
    }


}
