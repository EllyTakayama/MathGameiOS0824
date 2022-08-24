using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StudyRecord : MonoBehaviour
{
    public bool modemenu;//練習　テスト問題
    public int score;//正解数
    DateTime TodayNow;//勉強した日付を取得

    public static StudyRecord instance; 
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
