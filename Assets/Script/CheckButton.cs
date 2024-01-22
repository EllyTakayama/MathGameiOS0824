using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.Serialization;
using System.Text;

/*かけ算アプリで解答ボタンを押したときに正誤判定スクリプト0628
*マルばつ画像表示
　正誤問題をgradePanel内に代入
*/

public class CheckButton : MonoBehaviour {
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
    private StringBuilder _correctAnswerStringBuilder;
    private StringBuilder _wrongAnswerStringBuilder;
    public string correctAnswerString;
    public string wrongAnswerString;
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
    [SerializeField] private Text _quesAnsText;//答えをテキスト表示するため
    [SerializeField] private Text _valueA;//かけられる数のテキスト
    [SerializeField] private Text _valueB;//かける数のテキスト
    [SerializeField] private GameObject ansImage;//クエスチョンのイメージ
    [SerializeField] private DOAnsTextRotate _doAnsTextRotate;//クエスチョンを回転させる
    [SerializeField] private DOTweenPanel doTweenPanel;//GradePanelを表示させるTween
    
    void Start()
    {
        //Startですること
        //score,count リセット
        //maruImage,batsuImage 非表示

        score = 0;
        count = 0;
        time = 0.0f;
        _correctAnswerStringBuilder = new StringBuilder("せいかいしたもんだい\n");
        _wrongAnswerStringBuilder = new StringBuilder("まちがえたもんだい\n");

        for (int i = 0; i < maruImage.Length; i++)
        {
            maruImage[i].SetActive(false);
            batsuImage[i].SetActive(false);
        }


        //回答ボタン（3こ）のコンポーネントを取得 
        thisButton = GetComponent<Button>();
        hiScore = GameManager.singleton.hiScore;
        GameManager.singleton.currentScore = score;
        GameManager.singleton.currentCount = count;
        canAnswer = true;
        correctAnswer.text = "せいかいしたもんだい\n";
        wrongAnswer.text = "まちがえたもんだい\n";
    }

    public void ResetButtonScore()
    {
        score = 0;
        count = 0;
        _correctAnswerStringBuilder.Clear();
        _wrongAnswerStringBuilder.Clear();
        _correctAnswerStringBuilder.Append("せいかいしたもんだい\n");
        _wrongAnswerStringBuilder.Append("まちがえたもんだい\n");
    }

    void ToStringAnswer()
    {
        correctAnswerString = _correctAnswerStringBuilder.ToString();
        wrongAnswerString = _wrongAnswerStringBuilder.ToString();
    }
    
    //回答ボタンの正誤判定
    public void CheckTheTextofButton(int buttonIndex)
    {
        StartCoroutine(CheckButtonCoroutine(buttonIndex));
    }
    //掛け算の答えを表示させる
    void CalculateMultiplication()
    {
        // valueAとvalueBのテキストをintに変換
        if (int.TryParse(_valueA.text, out int valueA) && int.TryParse(_valueB.text, out int valueB))
        {
            // 掛け算を実行
            int result = valueA * valueB;

            // 結果を表示（例えばデバッグログに表示）
            _quesAnsText.text = $"{result}";
            Debug.Log($"Result of multiplication: {result}");
            
        }
        else
        {
            // テキストをintに変換できなかった場合のエラー処理
            Debug.LogError("Failed to convert one or both values to integers.");
        }
    }
    IEnumerator CheckButtonCoroutine(int buttonIndex)
    {
        CalculateMultiplication();
        Index = buttonIndex;
        count++;
        yield return new WaitForSeconds(0.2f);
        //markText.text = $"{score}";
        //AnimateOtherButtons(AnsButtons[Index]);
        doRenshuButtonAnim.AnimateOtherButtons(buttonIndex);
        for(int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = false;
        }
        yield return new WaitForSeconds(0.4f);
        _doAnsTextRotate.RotatePanel();
        
        //正解の場所（値）と回答したボタンのタグ（回答ボタンは左から1、2、3の文字列をタグ付してあります）を比較し正誤を判定します
        //正解ボタンの場所（値）はMathAndScript.csで文字列に変換しtagOfButtonに代入されています
        //Debug.Log("buttonIndex_" + buttonIndex);
        //Debug.Log("i" + MathAndAnswer.instance.locationOfAnswer);
        yield return new WaitForSeconds(0.8f);
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
            _correctAnswerStringBuilder.AppendLine($"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}");
            
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
            _wrongAnswerStringBuilder.AppendLine($"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}");
            wrongAnswer.text += $"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}\n";

        }
        //countText.text = $"{count}/もんめ";
        markText.text = $"せいかい{score}コ";
        Invoke("DelayImageOff", 0.5f);
        Invoke("DelayMathAnswer", 1.5f);
        //MathAndAnswer.instance.MathsProblem();
        
    }

    void DelayImageOff()
    {
        doRenshuButtonAnim.InvisibleButton();
        batsuImage[Index].SetActive(false);
        maruImage[Index].SetActive(false);

    }
    void DelayMathAnswer()
    {
        if (GameManager.singleton.currentCount >= 9 && GameManager.singleton.isRenshu)
        {
            ToStringAnswer();
            doTweenPanel.DoGradeCall();
            return;
        }else if 
            (GameManager.singleton.currentCount > TestToggle.testQuestion - 1 && !GameManager.singleton.isRenshu)
        {
            ToStringAnswer();
            doTweenPanel.DoGradeCall();
            return;
        }

        _doAnsTextRotate.ResetAnswerText();
        MathAndAnswer.instance.MathsProblem();
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = true;
        }
    }

        void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }

    
  
}









