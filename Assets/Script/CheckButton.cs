using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.Serialization;

/*かけ算アプリで解答ボタンを押したときに正誤判定スクリプト0628
*マルばつ画像表示
　正誤問題をgradePanel内に代入

*/

public class CheckButton : MonoBehaviour {

    /// <summary>
    /// This script help to identify the button tag and increases score if button is correct
    /// </summary>
    //ref to the button
    ReactiveProperty<int> currentCount = new ReactiveProperty<int>(0);
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
    [SerializeField] private GameObject[] maruImage;  
    [SerializeField] private GameObject[] batsuImage;
    public GameObject piyo;
    public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御
    public Button[] AnsButtons;
    public int Index;//marubatuImageのインデックス取得のため

    float time=0.0f;
    public bool isPressed;//マルバツ画像の表示を調整する為のbool、ボタンが押されると時間の測定を開始
    [SerializeField] private DORenshuButtonAnim doRenshuButtonAnim;//ボタンアニメーションスクリプトを取得

    void Start()
    {
        //Startですること
        //score,count リセット
        //maruImage,batsuImage 非表示
        //当初は1つのオブジェクトにかけ算の結果判定に対してマル、バツを代入するつもりでしたがうまくいかなくてやめました
        score = 0;
        count = 1;
        time = 0.0f;

        for(int i = 0;i < maruImage.Length; i++)
        {
            maruImage[i].SetActive(false);
            batsuImage[i].SetActive(false);
        } 
       
       
        //回答ボタン（3こ）のコンポーネントを取得 
        thisButton = GetComponent<Button>();
        hiScore = GameManager.singleton.hiScore;
        GameManager.singleton.currentScore=score;
        GameManager.singleton.currentCount=count;
        canAnswer = true;
       correctAnswer.text = "せいかいしたもんだい\n";
       wrongAnswer.text = "まちがえたもんだい\n";
       // ボタンごとにクリックイベントを設定
       /*
       foreach (Button button in AnsButtons)
       {
           button.onClick.AddListener(() =>
           {
               // クリックされたボタン以外のボタンをアニメーションさせる
               AnimateOtherButtons(button);
           });
       }*/
    }
    /*private void AnimateOtherButtons(Button clickedButton)
    {
        foreach (Button otherButton in AnsButtons)
        {
            if (otherButton != clickedButton)
            {
                // 回転しながらスケールを0にするアニメーション
                otherButton.transform.DORotate(new Vector3(0f, 0f, 180f), 0.5f);
                otherButton.transform.DOScale(Vector3.zero, 0.5f);
            }
        }
    }*/
    public void ResetButtonScore()
    {
        score = 0;
        count = 0;
    }
  

    //回答ボタンの正誤判定
    public void checkTheTextofButton(int buttonIndex)
    {
        Index = buttonIndex;
        count++;
        markText.text = $"{score}";
        //AnimateOtherButtons(AnsButtons[Index]);
        doRenshuButtonAnim.AnimateOtherButtons(buttonIndex);
        for(int i = 0; i > AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = false;
        }
        //正解の場所（値）と回答したボタンのタグ（回答ボタンは左から1、2、3の文字列をタグ付してあります）を比較し正誤を判定します
        //正解ボタンの場所（値）はMathAndScript.csで文字列に変換しtagOfButtonに代入されています
        //Debug.Log("buttonIndex_" + buttonIndex);
        //Debug.Log("i" + MathAndAnswer.instance.locationOfAnswer);
        if (buttonIndex == MathAndAnswer.instance.locationOfAnswer)
        {
            //正解ならマル画像表示、正解数score,問題出題数countが1ずつ増えます
            //countが9超えたらGameOver画面に切り替え出題を終了予定です
            //isPressed = true;
            maruImage[buttonIndex].SetActive(true);
            piyo.GetComponent<piyoPlayer>().Happy();//正解ならpiyoPlayer.csのHappy（）を実行
            SoundManager.instance.PlaySE0();//SoundManagerからPlaySE0を実行
            //UniRxのvalueの変化
            currentCount.Value ++;
            score++;
            GameManager.singleton.currentScore = score;
            GameManager.singleton.currentCount = count;
            correctAnswer.text += $"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}\n";
            //UpdateScore(scoreToAdd);
            markText.text = $"{score}";
            countText.GetComponent<CountText>().CountMove();
            markText.GetComponent<ScoreText>().ScoreMove();
        }   
        else   
        {
            //不正解ならバツ画像表示、問題出題数countが1増えます
            //isPressed = true;
            piyo.GetComponent<piyoPlayer>().Damage();//正解ならpiyoPlayer.csのHappy（）を実行
            batsuImage[buttonIndex].SetActive(true);
            SoundManager.instance.PlaySE1();//SoundManagerからPlaySE1を実行
            GameManager.singleton.currentCount = count;
            countText.GetComponent<CountText>().CountMove();
            correctAnswer.text += $"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}\n";

        }
        countText.text = $"{count}";
        Invoke("DelayImageOff", 0.4f);
        Invoke("DelayMathAnswer", 1.0f);
        //MathAndAnswer.instance.MathsProblem();
    }
    void DelayImageOff()
    {
        batsuImage[Index].SetActive(false);
        maruImage[Index].SetActive(false);

    }
    void DelayMathAnswer()
    {
        MathAndAnswer.instance.MathsProblem();
        for (int i = 0; i > AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = true;
        }

    }

        void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }

    
  
}









