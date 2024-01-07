
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


//かけ算アプリ問題出題のScriptだよ

public class MathAndAnswer : MonoBehaviour
{
    //we make this script instance
    public static MathAndAnswer instance;
    [SerializeField] private DOTweenPanel doTweenPanel;//DoTweenPanel.csを直接参照
    [SerializeField] private DORenshuButtonAnim _doRenshuButtonAnim;//AnsButtonのアニメーション
    [SerializeField] private DOQuesPanelRotate _doQuesPanelRotate;//QuesPanelを回転させる
    //MathTypeをれんしゅうボタンmultiplication1-9
    //テストボタンmultiplication11-19で設定します
    public enum MathsType
    {
        multiplicationRenshuu,
        multiplicationTest,

    }
    public MathsType mathsType;
    //2 private floats this are the question values a and b
    public int a, b;
    //the variable for answer value
    [HideInInspector] public int answer;
    //varible whihc will assign ans to any one of the 4 answer button
    public int locationOfAnswer;
    //ref to the button
    public GameObject[] AnsButtons;
    //get the tag of button 
    public string tagOfButton;
    //ref to text in scene where we will assign a and b values of question
    public Text valueA;
    public Text valueB;
    private int currentMode;
    public bool multi1;//練習画面でのかける数の順番を制御するbool
    public GameObject tableButton;
    private int n;//配列のスクリプトでiについてcs0103エラーが出たため宣言してます
    int[] ary = new int[9];

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (MulToggle.Table1_OnOf == true)
        {
            multi1 = true;
            //Debug.Log("Startmulti1"+multi1);
        }
        else
        {
            multi1 = false;
            //Debug.Log("Startmulti1"+multi1);
        }

        //トップメニューのれんしゅうORテストボタンからかけ算の段のボタンを押すと
        //currentMode（現在の段）が選択されます
        //

        if (GameManager.singleton != null)
        {

            currentMode = GameManager.singleton.currentMode;
        }

        //we call the methods
        CurrentMode();
        //MathsProblem();
        SoundManager.instance.PlayBGM("Renshuu");

    }
    //this method keeps the track of mode 
    void CurrentMode()
    {
        if (currentMode < 10)
        {
            //depending on the currentmode value we assign the mode
            mathsType = MathsType.multiplicationRenshuu;

        }
        if (currentMode >= 11)
        {
            //depending on the currentmode value we assign the mode
            mathsType = MathsType.multiplicationTest;

        }


        //乱数を生成
        System.Random randomNum = new System.Random();

        //配列内1~9の値を格納
        for (int i = 0; i < ary.Length; i++)
        {
            ary[i] = i + 1;
        }
        //配列をシャッフル
        for (int i = ary.Length - 1; i >= 0; i--)
        {
            //Nextメソッドは引数未満の数値がランダムで返る
            int j = randomNum.Next(i + 1);
            //tmpは配列間でやりとりする値を一時的に格納する変数
            (ary[i], ary[j]) = (ary[j], ary[i]);
            //ログに乱数を表
        }

    }
    // 回答は3つのボタンから選択します
    // 正解ボタンの場所（値）を回答ボタンのタグに代入し正誤判定に使用します
    /*
    void Update()
    {
        tagOfButton = locationOfAnswer.ToString();

    }*/

    void PiyoSE()
    {
        SoundManager.instance.PlaySE3();
    }

    public void MathsProblem()
    {
        //SoundManager.instance.PlaySE3();
        
        switch (mathsType)
        {
            case (MathsType.multiplicationRenshuu):
                MultiplicationMethodRenshuu();
                break;
            case (MathsType.multiplicationTest):

                MultiplicationMethodTest();
                break;

        }

        _doQuesPanelRotate.VisibleQuesPanel();
        Invoke("DelayRenshuButton",0.2f);
    }

    void DelayRenshuButton()
    {
        _doRenshuButtonAnim.ResetButton();
    }
    /*
    public void Mul1Toggle(){
    multi9 = true;
    }

    public void Mul9Toggle(){
         multi9 = false;
    }
    */

    public void MultiplicationMethodRenshuu()//練習
    {
        if (GameManager.singleton.currentCount >= 9)
        {
            doTweenPanel.DoGradeCall();
            return;
        }
        int a = currentMode % 10; // 10で割ったあまりなのでcurrentModeが2なら2, 12なら2が出る
        if (multi1 == true)
        {//かける9降順、デフォルトでは×数は9ではない
            b++;
            if (b > 9)
            {
                b = 1;
                //Debug.Log("truemulti1"+multi1);
            }
        }
        else if (multi1 == false)
        {
            b--; //かける1順、デフォルトではこちら。
            if (b < 1)
            {
                return;
                //b = 9;
                //Debug.Log("falsemulti1"+multi1);
            }
        }
        Invoke("PiyoSE", 0.6f);
        //テストの段は11ー19での数がMultiplicationMethodにつきます。テストの2段は12です

        /*int a = currentMode%10; // 10で割ったあまりなのでcurrentModeが2なら2, 12なら2が出る
        b++;                    // こうすると出題するたびに1ずつ増えます。
        if(b>9){
            b=1;
        }
        */


        valueA.text = a.ToString();//段の数を表示
        valueB.text = b.ToString();//かける数を表示

        answer = a * b;//かけ算の答えを求める

        locationOfAnswer = UnityEngine.Random.Range(0, 3);//正解のボタンの場所をランダムに代入
                                                          //AnsButtons.Lengthでも良かったかもしれません

        if (locationOfAnswer == 0)//正解ボタンが一番左の場合の回答の値の範囲の設定
        {
            AnsButtons[0].GetComponentInChildren<Text>().text = answer.ToString();
            AnsButtons[1].GetComponentInChildren<Text>().text = (answer + a).ToString();
            AnsButtons[2].GetComponentInChildren<Text>().text = (answer + a + a).ToString();
        }

        else if (locationOfAnswer == 1)//正解ボタンが真ん中の場合の回答の値の範囲の設定
        {
            AnsButtons[1].GetComponentInChildren<Text>().text = answer.ToString();
            AnsButtons[2].GetComponentInChildren<Text>().text = (answer + a).ToString();
            AnsButtons[0].GetComponentInChildren<Text>().text = (answer + a + a).ToString();
        }

        else if (locationOfAnswer == 2)//正解ボタンが右の場合の回答の値の範囲の設定
        {
            AnsButtons[2].GetComponentInChildren<Text>().text = answer.ToString();
            AnsButtons[1].GetComponentInChildren<Text>().text = (answer + a).ToString();
            AnsButtons[0].GetComponentInChildren<Text>().text = (answer + a + a).ToString();
        }
    }




    public void MultiplicationMethodTest()//力だめし
    {
        if (GameManager.singleton.currentCount > TestToggle.testQuestion - 1)
        {
            doTweenPanel.DoGradeCall();
            return;
        }

        if (n >= ary.Length)
        {
            //return;
            n = 0;
        }
        n++;
        print("n_" + n);
        Invoke("PiyoSE", 0.6f);
        int b = ary[n];//かける数にランダムに取得した配列データを代入
        int a = currentMode % 10;//1の段


        valueA.text = a.ToString();//段の数を表示
        valueB.text = b.ToString();//かける数を表示

        answer = a * b;//かけ算の答えを求める

        locationOfAnswer = UnityEngine.Random.Range(0, 3);//正解のボタンの場所をランダムに代入
                                                          //AnsButtons.Lengthでも良かったかもしれません

        if (locationOfAnswer == 0)//正解ボタンが一番左の場合の回答の値の範囲の設定
        {
            AnsButtons[0].GetComponentInChildren<Text>().text = answer.ToString();
            AnsButtons[1].GetComponentInChildren<Text>().text = (answer + a).ToString();
            AnsButtons[2].GetComponentInChildren<Text>().text = (answer + a + a).ToString();
        }

        else if (locationOfAnswer == 1)//正解ボタンが真ん中の場合の回答の値の範囲の設定
        {
            AnsButtons[1].GetComponentInChildren<Text>().text = answer.ToString();
            AnsButtons[2].GetComponentInChildren<Text>().text = (answer + a).ToString();
            AnsButtons[0].GetComponentInChildren<Text>().text = (answer + a + a).ToString();
        }

        else if (locationOfAnswer == 2)//正解ボタンが右の場合の回答の値の範囲の設定
        {
            AnsButtons[2].GetComponentInChildren<Text>().text = answer.ToString();
            AnsButtons[1].GetComponentInChildren<Text>().text = (answer + a).ToString();
            AnsButtons[0].GetComponentInChildren<Text>().text = (answer + a + a).ToString();
        }
    }

}
