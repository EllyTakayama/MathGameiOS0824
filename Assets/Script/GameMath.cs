using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System;
using Shapes2D;
using UnityEngine.Localization.Settings;

public class GameMath : MonoBehaviour
{
    //we make this script instance
    public static GameMath instance;
    [SerializeField] private DOGTweenPanel doTweenPanel;//DoTweenPanel.csを直接参照
    [SerializeField] private DORenshuButtonAnim _doRenshuButtonAnim;//AnsButtonのアニメーション
    [SerializeField] private DOQuesPanelRotate _doQuesPanelRotate;//QuesPanelを回転させる

    [SerializeField] private DOAnsButtonMove _doAnsButtonMove;//AnsButtonを動かすアニメーション

    public GameManagerMathsType mathsType;
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
    public Text valueA;//かけられる数
    public Text valueB;//かける数
    public Text ansText;//答えの表示
    private int currentMode;
    public bool multi1;//練習画面でのかける数の順番を制御するbool
    //public GameObject tableButton;
    private int n;//配列のスクリプトでiについてcs0103エラーが出たため宣言してます
    //int[] ary = new int[9];
    private int[] ary;//CurrentModeメソッドでシステム言語によって要素数を変更する
    [SerializeField] private int countText;//出題数を表記する
    [SerializeField] private GUIManager1 _guiManager1;

    [SerializeField] private GameObject questionImage;

    [SerializeField] private GameObject quesitonText;

    [SerializeField] private GameObject valueBQuesObj;

    [SerializeField] private GameObject valueVText;
    //GUIの管理マネージャーの変数取得
    //public CheckButton checkButton;

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
        countText = 0;
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
        SoundManager.instance.PlayBGM("Test");
        // GameManagerのmathsTypeをmultiplicationTestに設定
        GameManager.singleton.currentMathsType = GameManagerMathsType.multiplicationTest;
        //Invoke("MathsProblem",0.5f);
    }
    //this method keeps the track of mode 
    void CurrentMode()
    {
        //乱数を生成
        System.Random randomNum = new System.Random();
        //システム言語で要素数を切り分けシャッフルする
        if (Application.systemLanguage == SystemLanguage.English)
        {
            ary = new int[12];
            for (int i = 0; i < ary.Length; i++)
            {
                ary[i] = i + 1;
            }
        }
        else//英語以外の言語の場合
        {
            ary = new int[9];
            for (int i = 0; i < ary.Length; i++)
            {
                ary[i] = i + 1;
            }
        }
        //シャッフルする
        for (int i = ary.Length - 1; i >= 0; i--)
        {
            int j = randomNum.Next(i + 1);
            (ary[i], ary[j]) = (ary[j], ary[i]);
        }
        /*
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
        }*/

    }
    void PiyoSE()
    {
        SoundManager.instance.PlaySE3();
    }

    public void MathsProblem()
    {
        currentMode = GameManager.singleton.currentMode;
        Debug.Log($"currentMode_{GameManager.singleton.currentMode}");
        switch (mathsType)
        {
            case (GameManagerMathsType.multiplicationRenshuu):
                MultiplicationMethodRenshuu();
                break;
            case (GameManagerMathsType.multiplicationTest):
                MultiplicationMethodTest();
                break;
            case GameManagerMathsType.multiplicationMusikui: // 追加
                MultiplicationMethodMusikui();
                break;
        }
        SetQuesPanelTexts();
        _doQuesPanelRotate.VisibleQuesPanel();
        Invoke("DelayRenshuButton",0.2f);
    }
    
    //初期Text ,Imageの表示非表示の設定
    void SetQuesPanelTexts()
    {
        switch (mathsType)
        {
            case (GameManagerMathsType.multiplicationRenshuu):
                questionImage.SetActive(true);
                quesitonText.SetActive(false);
                valueBQuesObj.SetActive(false);
                valueVText.SetActive(true);
                break;
            case (GameManagerMathsType.multiplicationTest):
                questionImage.SetActive(true);
                quesitonText.SetActive(false);
                valueBQuesObj.SetActive(false);
                valueVText.SetActive(true);
                break;
            case GameManagerMathsType.multiplicationMusikui: // 追加
                questionImage.SetActive(false);
                quesitonText.SetActive(true);
                valueBQuesObj.SetActive(true);
                valueVText.SetActive(false);
                break;
        }
    }
    void DelayRenshuButton()
    {
        _doRenshuButtonAnim.GResetButton();
        _doAnsButtonMove.DoMoveAnsButton();
    }
    public void MultiplicationMethodRenshuu()//練習
    {
        if (GameManager.singleton.currentCount >= 9)
        {
            doTweenPanel.DoGameGradeCall();
            return;
        }
        countText++;
        //_guiManager1.countText.text = $"{countText} 問目";
        //Degubようコメント
        a = currentMode;
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
                                                          //AnsButtons.Lengthでも良かったかもしれませ

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
        //if (GameManager.singleton.currentCount > TestToggle.testQuestion - 1)
            if (GameManager.singleton.currentCount > 9)
        {
            doTweenPanel.DoGameGradeCall();
            return;
        }
        if (n >= ary.Length)
        {
            //return;
            n = 0;
        }
        countText++;
        //GCountTextLocalization();//出題数をローカライズして取得
        //_guiManager1.countText.text = $"{countText} 問目";
        //n++;
        print("n_" + n);
        Invoke("PiyoSE", 0.6f);
        int b = ary[n];//かける数にランダムに取得した配列データを代入
        int a = currentMode;//1の段
        
        valueA.text = a.ToString();//段の数を表示
        valueB.text = b.ToString();//かける数を表示
        Debug.Log($"a_{a}");
        Debug.Log($"b_{b}");
        answer = a * b;//かけ算の答えを求める
        Debug.Log($"answer_{answer}");

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
        n++;
        print($"{n}モン目");
    }
    //虫食い斬でかける数を回答させる
    public void MultiplicationMethodMusikui()
    {
        if (GameManager.singleton.currentCount > TestToggle.testQuestion - 1)
        {
            doTweenPanel.DoGameGradeCall();
            return;
        }
        if (n >= ary.Length)
        {
            //return;
            n = 0;
        }
        countText++;
        //_guiManager1.countText.text = $"{countText} もんめ";
        //n++;
        print("n_" + n);
        Invoke("PiyoSE", 0.6f);
        int b = ary[n];//かける数にランダムに取得した配列データを代入
        int a = currentMode % 10;//1の段
        
        valueA.text = a.ToString();//段の数を表示
        //valueB.text = b.ToString();//かける数を表示
        
        answer = a * b;//かけ算の答えを求める
        ansText.text = $"answer";

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
        n++;
        print($"{n}モン目");
        print("musikui");
    }

}
