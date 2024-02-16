using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.Serialization;
using System.Text;
using TMPro;//TextMeshProを使う場合
using UniRx.Triggers;

/*かけ算アプリで解答ボタンを押したときに正誤判定スクリプト0628
*マルばつ画像表示
　正誤問題をgradePanel内に代入
*/

public class GCheckButton : MonoBehaviour {
    // 以下のように、currentCoinCountの初期化をAwake()メソッドで行う
    private ReactiveProperty<int> currentCoinCount = new ReactiveProperty<int>();
    // GameManagerのcoinNumを初期値とする
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
    [SerializeField] private GameObject piyo;
    public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御
    public Button[] AnsButtons;
    public int Index;//marubatuImageのインデックス取得のため
    [SerializeField] private Collider2D[] ansButtonCollider;//回答Buttonのコライダーの話
    float time=0.0f;
    public bool isPressed;//マルバツ画像の表示を調整する為のbool、ボタンが押されると時間の測定を開始
    [SerializeField] private DORenshuButtonAnim doRenshuButtonAnim;//ボタンアニメーションスクリプトを取得
    [SerializeField] private Text _quesAnsText;//答えをテキスト表示するため
    [SerializeField] private Text _valueA;//かけられる数のテキスト
    [SerializeField] private Text _valueB;//かける数のテキスト
    [SerializeField] private GameObject ansImage;//クエスチョンのイメージ
    [SerializeField] private DOAnsTextRotate _doAnsTextRotate;//クエスチョンを回転させる
    [SerializeField] private DOGTweenPanel doTweenPanel;//GradePanelを表示させるTween
    [SerializeField] private DoGameResultPanel _doGameResultPanel;//gameClearの最初の画面を表示させる
    [SerializeField] private DoCoinAnim coinAnimScript; // DoCoinAnim.csのインスタンスをインスペクターから参照するための変数
    [SerializeField] private ParticleManager particleManager; // ParticleManagerをインスペクターから参照するための変数
    [SerializeField] private TextMeshProUGUI coinText;//取得したコインの枚数を表示するText

    void Awake()
    {
        currentCoinCount.Value = GameManager.singleton.coinNum;
        coinText.text = GameManager.singleton.coinNum.ToString();
    }

    void Start()
    {
        //Startですること
        //score,count リセット
        //maruImage,batsuImage 非表示

        score = 0;
        count = 0;
        GameManager.singleton.currentScore = 0;
        GameManager.singleton.currentCount = 0;
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
        print($"GameManager.singleton.currentScore,{GameManager.singleton.currentScore}");
        print($"GameManager.singleton.currenCount,{GameManager.singleton.currentCount}");
        coinText.text = GameManager.singleton.coinNum.ToString();
// currentCoinCountの変更を購読してcoinTextを更新
        currentCoinCount.Subscribe(newCoinCount =>
        {
            coinText.text = newCoinCount.ToString();
            Debug.Log("coinCount updated: " + newCoinCount);
        }).AddTo(this);
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
        Debug.Log($"correctAnswerString{correctAnswerString}");
        Debug.Log($"wrongAnswerString{wrongAnswerString}");
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
        for(int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = false;
            ansButtonCollider[i].enabled = false;
        }
        yield return new WaitForSeconds(0.2f);
        //markText.text = $"{score}";
        //AnimateOtherButtons(AnsButtons[Index]);
        doRenshuButtonAnim.AnimateOtherButtons(buttonIndex);
        yield return new WaitForSeconds(0.4f);
        _doAnsTextRotate.RotatePanel();
        
        //正解の場所（値）と回答したボタンのタグ（回答ボタンは左から1、2、3の文字列をタグ付してあります）を比較し正誤を判定します
        //正解ボタンの場所（値）はMathAndScript.csで文字列に変換しtagOfButtonに代入されています
        //Debug.Log("buttonIndex_" + buttonIndex);
        //Debug.Log("i" + MathAndAnswer.instance.locationOfAnswer);
        yield return new WaitForSeconds(0.8f);
        if (buttonIndex == GameMath.instance.locationOfAnswer)
        {
            //正解ならマル画像表示、正解数score,問題出題数countが1ずつ増えます
            //countが9超えたらGameOver画面に切り替え出題を終了予定です
            //isPressed = true;
            
            //piyo.GetComponent<piyoPlayer>().Happy();//正解ならpiyoPlayer.csのHappy（）を実行
            SoundManager.instance.PlaySE0();//SoundManagerからPlaySE0を実行
            //UniRxのvalueの変化
            
            //maruImage[buttonIndex].SetActive(true);
            
            score++;
            GameManager.singleton.currentScore = score;
            GameManager.singleton.currentCount = count;
            _correctAnswerStringBuilder.AppendLine($"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}");
            
            correctAnswer.text += $"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}\n";
            //UpdateScore(scoreToAdd);
            markText.text = $"{score}";
            countText.GetComponent<CountText>().CountMove();
            markText.GetComponent<ScoreText>().ScoreMove();
            // 正解のボタンが押されたらDoCoinAnim.csのSpawnCoinOnButtonメソッドを実行してcoinを生成する
            if (coinAnimScript != null)
            {
                coinAnimScript.SpawnCoinOnButton(AnsButtons[buttonIndex]);
            }
            // 正解時にパーティクルを再生
            if (particleManager != null)
            {
                particleManager.PlayParticle(Random.Range(4, 7), AnsButtons[buttonIndex].transform.position);
            }
            currentCoinCount.Value += 10;
            GameManager.singleton.coinNum += 10;
            GameManager.singleton.CoinSave();
        }   
        else   
        {
            //不正解ならバツ画像表示、問題出題数countが1増えます
            //isPressed = true;
            //piyo.GetComponent<piyoPlayer>().Damage();//正解ならpiyoPlayer.csのHappy（）を実行
            
            //batsuImage[buttonIndex].SetActive(true);
            SoundManager.instance.PlaySE1();//SoundManagerからPlaySE1を実行
            GameManager.singleton.currentCount = count;
            countText.GetComponent<CountText>().CountMove();
            _wrongAnswerStringBuilder.AppendLine($"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}");
            wrongAnswer.text += $"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}\n";
            // 不正解時にパーティクルを再生
            if (particleManager != null)
            {
                particleManager.PlayParticle(7, AnsButtons[buttonIndex].transform.position);
            }

        }
        //countText.text = $"{count}/もんめ";
        markText.text = $"せいかい{score}コ";
        Invoke("DelayImageOff", 0.5f);
        Invoke("DelayMathAnswer", 1.5f);
        //MathAndAnswer.instance.MathsProblem();
        
    }

    //範囲外で出題をリセットしたい場合
    public void ChangeQues()
    {
        count++;
        GameManager.singleton.currentCount = count;
        Invoke("DelayImageOff", 0f);
        Invoke("DelayMathAnswer", 0.5f);
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
            ToStringAnswer();
            //doTweenPanel.DoGameGradeCall();
            _doGameResultPanel.SetResult();
            print("count9isRenshu");
            return;
        }
        _doAnsTextRotate.ResetAnswerText();
        GameMath.instance.MathsProblem();
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = true;
            ansButtonCollider[i].enabled = true;
        }
    }

        void DelayReset()
    {
        canAnswer = true;//問題に答えた時ボタン押せないブールをtrueに戻しておく
    }
  
}









