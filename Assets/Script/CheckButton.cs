using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

/*かけ算アプリで解答ボタンを押したときに正誤判定スクリプト0628
*マルばつ画像表示
　正誤問題をgradePanel内に代入

*/

public class CheckButton : MonoBehaviour {

/// <summary>
/// This script help to identify the button tag and increases score if button is correct
/// </summary>
    //ref to the button
    private Button thisButton;
    //正誤チェックと共にスコア、問題表示回数、正解数の表示を行うために宣言です
    private int hiScore;
    private int count;
    public int score;
    public int tmpScore;                   // 現在のスコアを一旦記憶
    public int tmpScoreGoal; 
    public int scoreToAdd;
    public Text markText;
    public Text countText;
    public Text correctAnswer;
    public Text wrongAnswer;
    public GameObject scoreMove;
    [SerializeField] private GameObject maruImage;  
    [SerializeField] private GameObject batsuImage;
    public GameObject piyo;
    public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御
    
    float time=0.0f;
    public bool isPressed;//マルバツ画像の表示を調整する為のbool、ボタンが押されると時間の測定を開始

    void Start()
    {
        //Startですること
        //score,count リセット
        //maruImage,batsuImage 非表示
        //当初は1つのオブジェクトにかけ算の結果判定に対してマル、バツを代入するつもりでしたがうまくいかなくてやめました
        score = 0;
        count = 0;
        time = 0.0f;
        maruImage.SetActive(false);
        batsuImage.SetActive(false);   
       
       
        //回答ボタン（3こ）のコンポーネントを取得 
        thisButton = GetComponent<Button>();
        hiScore = GameManager.singleton.hiScore;
        GameManager.singleton.currentScore=score;
        GameManager.singleton.currentCount=count;
        canAnswer = true;
       correctAnswer.text = "せいかいしたもんだい\n";
       wrongAnswer.text = "まちがえたもんだい\n";
    
    }
  
    //Updateですること
    //正誤判定のマルバツ画像の表示時間調整のため
    //回答ボタンが押されてからの時間を取得
    //1秒経ってからマルバツ表記を消す
    void Update()
    {
        score = GameManager.singleton.currentScore;
        count = GameManager.singleton.currentCount;

        if (hiScore < score)
        {
            //we check if the hiScore is greater or less than score if its less we then save that score as hiScore
            hiScore = score;
            GameManager.singleton.hiScore = hiScore;
            GameManager.singleton.Save();
        }
        
    if(isPressed == true)
    {    
    time += Time.deltaTime;
        if(time> 0.7f)
        {
            if (maruImage.gameObject.activeSelf == true)
            {   
                maruImage.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;
             
            }
            else
            {                
                batsuImage.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;

            }
        }
     
    }

    }

    
    //回答ボタンの正誤判定
    public void checkTheTextofButton()
    {
        if (canAnswer == false)
        {
            return;//canAnswerがfalseなら正誤判定は実行しない
        }
        canAnswer = false;//bool canAnswerがfalseの時繰り返し、DelayResetを0.01秒ごに実行
        Invoke("DelayReset", 0.01f);
        //正解の場所（値）と回答したボタンのタグ（回答ボタンは左から1、2、3の文字列をタグ付してあります）を比較し正誤を判定します
        //正解ボタンの場所（値）はMathAndScript.csで文字列に変換しtagOfButtonに代入されています
        if (gameObject.CompareTag( MathAndAnswer.instance.tagOfButton))
        {
            //正解ならマル画像表示、正解数score,問題出題数countが1ずつ増えます
            //countが9超えたらGameOver画面に切り替え出題を終了予定です
            isPressed = true;
            maruImage.SetActive(true);
            piyo.GetComponent<piyoPlayer>().Happy();//正解ならpiyoPlayer.csのHappy（）を実行
            SoundManager.instance.PlaySE0();//SoundManagerからPlaySE0を実行
            score++;
            count++;
            GameManager.singleton.currentScore = score;
            GameManager.singleton.currentCount = count;
            correctAnswer.text += MathAndAnswer.instance.valueA.text+
            "×"+
            MathAndAnswer.instance.valueB.text+
            "="+MathAndAnswer.instance.answer+"\n";
            //UpdateScore(scoreToAdd);
            countText.GetComponent<CountText>().CountMove();
            markText.GetComponent<ScoreText>().ScoreMove();
           
        }   
            
        else   
        {
            //不正解ならバツ画像表示、問題出題数countが1増えます
            isPressed = true;
            piyo.GetComponent<piyoPlayer>().Damage();//正解ならpiyoPlayer.csのHappy（）を実行
            batsuImage.SetActive(true);
            SoundManager.instance.PlaySE1();//SoundManagerからPlaySE1を実行
            count++;
            GameManager.singleton.currentCount = count;
            countText.GetComponent<CountText>().CountMove();
            wrongAnswer.text += MathAndAnswer.instance.valueA.text+
            "×"+
            MathAndAnswer.instance.valueB.text+
            "="+MathAndAnswer.instance.answer+"\n";

        }
        MathAndAnswer.instance.MathsProblem();
    }
    /*
       public void UpdateScore(int scoreToAdd) {
        tmpScore = score;                   // 現在のスコアを一旦記憶
        tmpScoreGoal = score + scoreToAdd;  // 今回の加算でどこまで増えるか
        score = tmpScoreGoal;                   // スコア自体はすぐ反映してしまう

        // 表示する部分だけDOTweenで見せかける
        DOTween.To(() => tmpScore, (value) => tmpScore = value, tmpScoreGoal, 2f)
            .OnUpdate(()=> { countText.text = tmpScore.ToString(); });
    }
    */
        void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }

    
  
}









