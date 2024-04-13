using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;//TextMeshPro

//6月29日更新

public class TopLogin : MonoBehaviour
{
    private enum LOGIN_TYPE
    {
        FIRST_USER_LOGIN, //初回ログイン
        TODAY_LOGIN,      //ログイン
        ALREADY_LOGIN,    //ログイン済
        ERROR_LOGIN       //不正ログイン
    }
    public GameObject loginBonusPanel;
    public TextMeshProUGUI coinText;//coinAddImageのtext
    private int todayDate = 0;
    private int lastDate;
    [SerializeField] private GameObject piyo;
    [SerializeField] private RectTransform piyoTopPosition;
    [SerializeField] private DOCoinCount _doCoinCount;//コイン数表示のアニメーション
    [SerializeField] private DoCoinAnim _doCoinAnim;//coinを生成するスクリプト
    [SerializeField] private GameObject flash;
    [SerializeField] private DOflash _doFlash;//flashをアニメーションさせるスクリプト
    [SerializeField] private TextMeshProUGUI coinAddText;
    [SerializeField] private DOGachaBall _gachaBall;
    [SerializeField] private DoButton _doButton;//coinImageのアニメーション
    private LOGIN_TYPE judge_type;

    public void SetPiyoTop()
    {
        // piyoを移動させる
        piyo.transform.position = piyoTopPosition.position;
        piyo.SetActive(true);
        piyo.GetComponent<PiyoMove>().PiyoMoveAnim();//ピヨを表示させてアニメーションさせる
    }
    // Startメソッドの中で実行するコルーチン
    IEnumerator ActivateLoginBonusPanel()
    {
        yield return new WaitForSeconds(0.8f); // 0.8秒待機する
        piyo.GetComponent<PiyoMove>().StopPiyoAnimation();//ピヨのアニメーションを停止
        piyo.SetActive(false);
        loginBonusPanel.SetActive(true);
        coinAddText.text = $"{GameManager.singleton.beforeTotalCoin}";
        SoundManager.instance.PlaySE13rewardButton();
        _gachaBall.BallShakeLoop();//ピヨのアニメーション
        yield return new WaitForSeconds(0.4f); //待機する
        flash.SetActive(true);
        _doButton.OnScale();//スケールを拡大アニメーション
        _doFlash.Flash18();//回転させる
        _doCoinAnim.SpawnRewardCoin();
        SoundManager.instance.PlaySE16GetCoin();
        yield return new WaitForSeconds(0.6f); //待機する
        _doCoinCount.CountCoin();
        yield return new WaitForSeconds(1.2f); //待機する
        SoundManager.instance.PlaySE18();
    }
    // Start is called before the first frame update
    void Start()
    {
        DateTime now = DateTime.Now;//端末の現在時刻の取得        
        todayDate = now.Year * 10000 + now.Month * 100 + now.Day;
        //日付を数値化　2020年9月1日だと20200901になる
        
        lastDate = ES3.Load<int>("lastDate","lastDate.es3",0 );

        //前回と今回の日付データ比較
        
        if (lastDate < todayDate)//日付が進んでいる場合
        {
            judge_type = LOGIN_TYPE.TODAY_LOGIN;
        }        
        else if (lastDate == todayDate)//日付が進んでいない場合
        {
            judge_type = LOGIN_TYPE.ALREADY_LOGIN;
        }
        else if (lastDate > todayDate)//日付が逆転している場合
        {
            judge_type = LOGIN_TYPE.ERROR_LOGIN;
        }
        switch (judge_type)
        {
            //ログインボーナス
            case LOGIN_TYPE.TODAY_LOGIN:
            if (lastDate == 0)
                {
                    print("初回ログイン");
                }else{
                    //GameManager.singleton.LoadCoin();
                    GameManager.singleton.beforeTotalCoin = GameManager.singleton.coinNum;
                    Debug.Log($"beforeTotalCoin_{GameManager.singleton.beforeTotalCoin}");
                    GameManager.singleton.coinNum += 150;
                    coinAddText.text = GameManager.singleton.beforeTotalCoin.ToString();
                    GameManager.singleton.CoinSave();
                    StartCoroutine(ActivateLoginBonusPanel()); // コルーチンを実行
                    print("今日のログボ"+todayDate);
                }
                break;

            //すでにログイン済み
            case LOGIN_TYPE.ALREADY_LOGIN:
                //なにもしない
                break;

            //不正ログイン
            case LOGIN_TYPE.ERROR_LOGIN:
                //不正ログイン時の処理
                break;
        }
        ES3.Save<int>("lastDate",todayDate,"lastDate.es3" );
        Debug.Log("lastDate"+lastDate);  
    }
}
