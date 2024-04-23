using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.Serialization;
using System.Text;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

/*かけ算アプリで解答ボタンを押したときに正誤判定スクリプト0628
*マルばつ画像表示
　正誤問題をgradePanel内に代入
*/

public class CheckButton : MonoBehaviour {
    //ReactiveProperty<int> currentCount = new ReactiveProperty<int>(0);
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
    [SerializeField] private ParticleManager _particleManager; // ParticleManagerをインスペクターから参照するための変数
    [SerializeField] private BackgroundControll _backgroundControll;//背景Spriteの取得のため
    private string markT;//markTextのローカライズ変数を取得するため
    private string correctText;//ローカライズを代入
    private string wrongText;//ローカライズを代入

    void Start()
    {
        //Startですること
        //score,count リセット
        //maruImage,batsuImage 非表示
        const string tableName = "RenshuuScene";
        // ローカライズされた文字列を取得
        correctText = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "correctAnswerString");
        wrongText  = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "wrongAnswerString");
        markT = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "markTextBefore");
        Debug.Log($"markT_{markT}");
        // ローカライズされた文字列を取得
        //string correctAnswerText = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "correctAnswerString");
        //string wrongAnswerText  = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "wrongAnswerString");
        score = 0;
        count = 0;
        time = 0.0f;
        _correctAnswerStringBuilder = new StringBuilder(correctText);
        _wrongAnswerStringBuilder = new StringBuilder(wrongText);

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
        correctAnswer.text = "";
        wrongAnswer.text = "";
        //MarkTextLocalization();
    }

    public void ResetButtonScore()
    {
        score = 0;
        count = 0;
        GameManager.singleton.currentCount = 0;
        GameManager.singleton.currentScore = 0;
        correctAnswer.text = "";
        wrongAnswer.text = "";
        _correctAnswerStringBuilder.Clear();
        _wrongAnswerStringBuilder.Clear();
        _correctAnswerStringBuilder.Append(correctText);
        _wrongAnswerStringBuilder.Append(wrongText);
    }

    void ToStringAnswer()
    {
        int correctNum = _correctAnswerStringBuilder.Length;
        int wrongNum = _wrongAnswerStringBuilder.Length;
        //print($"correctNum,{correctNum}");//11文字
        //print($"wrongNum,{wrongNum}");//10文字
        if (correctNum == 11)
        {
            //correctAnswerString = "せいかいしたもんだい\nなし";
        }
        else
        {
            correctAnswerString = _correctAnswerStringBuilder.ToString();
        }

        if (wrongNum == 10)
        {
            //wrongAnswerString = "まちがえたもんだい\nなし";
        }
        else
        {
            wrongAnswerString = _wrongAnswerStringBuilder.ToString();
        }
        
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
            //currentCount.Value ++;
            score++;
            GameManager.singleton.currentScore = score;
            GameManager.singleton.currentCount = count;
            _correctAnswerStringBuilder.AppendLine($"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}");
            
            //correctAnswer.text += $"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}\n";
            //UpdateScore(scoreToAdd);
            //markText.text = $"markT{score}";
            //countText.GetComponent<CountText>().CountMove();
            //markText.GetComponent<ScoreText>().ScoreMove();
            // 正解時にパーティクルを再生
            if (_particleManager != null)
            {//パーティクルマネージャーの範囲で正解ようエフェクトは4~10
                _particleManager.PlayParticle(Random.Range(4, 11), piyo.transform.position);
            }
        }   
        else   
        {
            //不正解ならバツ画像表示、問題出題数countが1増えます
            //isPressed = true;
            piyo.GetComponent<piyoPlayer>().Damage();//正解ならpiyoPlayer.csのHappy（）を実行
            batsuImage[buttonIndex].SetActive(true);
            SoundManager.instance.PlaySE1();//SoundManagerからPlaySE1を実行
            GameManager.singleton.currentCount = count;
            //countText.GetComponent<CountText>().CountMove();
            _wrongAnswerStringBuilder.AppendLine($"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}");
            //wrongAnswer.text += $"{MathAndAnswer.instance.valueA.text}×{MathAndAnswer.instance.valueB.text}={MathAndAnswer.instance.answer}\n";
            //不正解時にパーティクルを再生
            if (_particleManager != null)
            {
                _particleManager.PlayParticle(11, piyo.transform.position);
            }
        }
        //countText.text = $"{count}/もんめ";
        //markText.text = markT+$"{score}";
        MarkTextLocalization();//markTextスコアをローカライズしつつ表示する
        Invoke("DelayImageOff", 0.5f);
        Invoke("DelayMathAnswer", 1.2f);
        //MathAndAnswer.instance.MathsProblem();
    }
    void MarkTextLocalization()
    {
        const string tableName = "RenshuuScene"; // ローカライズテーブルの名前
        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(tableName, "markText");
        string localizedText = localizedString.Replace("{score}", score.ToString());; // 報酬コインの値を文字列に埋め込む
        markText.text = localizedText; // ローカライズされた報酬テキストを更新
    }

    void DelayImageOff()
    {
        doRenshuButtonAnim.InvisibleButton();
        batsuImage[Index].SetActive(false);
        maruImage[Index].SetActive(false);

    }
    void DelayMathAnswer()
    {
        if (GameManager.singleton.currentCount >= 9)
        {
            _backgroundControll.StopBackGroundMove();
            _backgroundControll.NotActiveBackground();
            ToStringAnswer();
            doTweenPanel.DoGradeCall();
            Debug.Log("GameManager.singleton.currentCount >= 9 && GameManager.singleton.isRenshu");
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









