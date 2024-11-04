using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.Serialization;
using System.Text;
using TMPro;//TextMeshProを使う場合
using UniRx.Triggers;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

/*かけ算アプリで解答ボタンを押したときに正誤判定スクリプト0628
*マルばつ画像表示
　正誤問題をgradePanel内に代入
*/

public class GCheckButton : MonoBehaviour {
    private Button thisButton;
    //正誤チェックと共にスコア、問題表示回数、正解数の表示を行うために宣言です
    private int hiScore;
    private int count;
    public int score;
    public int tmpScore;                   // 現在のスコアを一旦記憶
    public int tmpScoreGoal; 
    public int scoreToAdd;
    public TextMeshProUGUI markText;
    public Text countText;
    public Text correctAnswer;
    public Text wrongAnswer;
    private StringBuilder _correctAnswerStringBuilder;
    private StringBuilder _wrongAnswerStringBuilder;
    public string correctAnswerString;
    public string wrongAnswerString;
    public GameObject scoreMove;
    //[SerializeField] private GameObject[] maruImage;  
    //[SerializeField] private GameObject[] batsuImage;
    [SerializeField] private GameObject piyo;
    //public bool canAnswer;//Buttonの不具合を解消するため連続してボタンを押せなないよう制御
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
    [SerializeField] private DOAnsButtonMove _doAnsButtonMove;//AnsButtonを移動させるアニメーション
    [SerializeField] private DOQuesPanelRotate _doQuesPanelRotate;//QuesPanelを非表示にする
    [SerializeField] private PiyoDamage _piyoDamage;//不正解時にpiyoを点滅させる
    [SerializeField] private QuesChangeCollider _quesChangeCollider;//AnsButtonParentのコライダーのスクリプトをfalseにする
    [SerializeField] private GameObject damageParticlePrefab;
    [SerializeField] private ParticleManager _particleManager1;
    [SerializeField] private BoxCollider2D _ansButtonCollider;//DOPanel呼び出し時に衝突感知のコライダーをオフする
    [SerializeField] private CapsuleCollider2D _piyoPlayer;//ピヨと衝突時のコライダーオフ
    private string markT;//markTextのローカライズ変数を取得するため
    private string correctText;//ローカライズを代入
    private string wrongText;//ローカライズを代入
    private bool piyoCollision = false;

    void Awake()
    {
        coinText.text = GameManager.singleton.coinNum.ToString();
    }

    void Start()
    {
        const string tableName = "RenshuuScene";
        // ローカライズされた文字列を取得
        correctText = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "correctAnswerString");
        wrongText  = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "wrongAnswerString");
        //markT = LocalizationSettings.StringDatabase.GetLocalizedString( tableReference:tableName,tableEntryReference: "markTextBefore");
        Debug.Log($"markT_{markT}");
        score = 0;
        count = 0;
        GameManager.singleton.currentScore = 0;
        GameManager.singleton.currentCount = 0;
        time = 0.0f;
        Debug.Log($"correctText_{correctText}");
        Debug.Log($"wrongText_{wrongText}");
        _correctAnswerStringBuilder = new StringBuilder(correctText);
        _wrongAnswerStringBuilder = new StringBuilder(wrongText);

        //回答ボタン（3こ）のコンポーネントを取得 
        thisButton = GetComponent<Button>();
        hiScore = GameManager.singleton.hiScore;
        GameManager.singleton.currentScore = score;
        GameManager.singleton.currentCount = count;
        correctAnswer.text = "";
        wrongAnswer.text = "";
        //print($"GameManager.singleton.currentScore,{GameManager.singleton.currentScore}");
        //print($"GameManager.singleton.currenCount,{GameManager.singleton.currentCount}");
        coinText.text = GameManager.singleton.coinNum.ToString();
    }

    public void ResetButtonScore()
    {
        score = 0;
        count = 0;
        _correctAnswerStringBuilder.Clear();
        _wrongAnswerStringBuilder.Clear();
        _correctAnswerStringBuilder.Append(correctText);
        _wrongAnswerStringBuilder.Append(wrongText);
    }

    void ToStringAnswer()
    {
        correctAnswerString = _correctAnswerStringBuilder.ToString();
        wrongAnswerString = _wrongAnswerStringBuilder.ToString();
        Debug.Log($"correctAnswerString_{correctAnswerString}");
        Debug.Log($"wrongAnswerString_{wrongAnswerString}");
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
        SoundManager.instance.PlaySE11Button3();
        _doAnsButtonMove.PauseButtonMovement();//AnsButtonの動きを止める
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
        yield return new WaitForSeconds(0.8f);
        if (buttonIndex == GameMath.instance.locationOfAnswer)
        {
            //正解ならマル画像表示、正解数score,問題出題数countが1ずつ増えます
            //countが9超えたらGameOver画面に切り替え出題を終了予定です
            SoundManager.instance.PlaySE21();//SoundManagerからPlaySE21子供の歓声を実行
      
            score++;
            GameManager.singleton.currentScore = score;
            GameManager.singleton.currentCount = count;
            _correctAnswerStringBuilder.AppendLine($"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}");
            
            //correctAnswer.text += $"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}\n";
            Debug.Log($"score_{score}");
            // 正解のボタンが押されたらDoCoinAnim.csのSpawnCoinOnButtonメソッドを実行してcoinを生成する
            if (coinAnimScript != null)
            {
                coinAnimScript.SpawnCoinOnButton(AnsButtons[buttonIndex]);
            }
            // 正解時にパーティクルを再生
            if (particleManager != null)
            {
                particleManager.PlayParticle(Random.Range(4, 7), AnsButtons[buttonIndex].transform.position);
                // piyoの位置にパーティクルを再生する
                particleManager.PlayParticle(Random.Range(8, 11), piyo.transform.position);
            }
            //currentCoinCount.Value += 10;
            GameManager.singleton.coinNum += 10;
            GameManager.singleton.CoinSave();
            yield return new WaitForSeconds(0.4f);
            markText.text = $"{GameManager.singleton.coinNum}";
        }   
        else   
        {
            SoundManager.instance.PlaySE1();//SoundManagerからPlaySE1を実行
            GameManager.singleton.currentCount = count;
            _wrongAnswerStringBuilder.AppendLine($"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}");
            //wrongAnswer.text += $"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}\n";
            // 不正解時にパーティクルを再生
            if (particleManager != null)
            {
                particleManager.PlayParticle(7, AnsButtons[buttonIndex].transform.position);
                SoundManager.instance.PlaySE20();//爆発音
                // piyoの位置にパーティクルを再生する
                particleManager.PlayParticle(Random.Range(12, 15), piyo.transform.position);
            }
            _piyoDamage.DamageCall();//ピヨをダメージを受けさせる

        }
        //MarkTextLocalization();//markTextスコアをローカライズしつつ表示する
        Invoke("DelayImageOff", 0.5f);
        Invoke("DelayMathAnswer", 1.5f);
      
    }
    void MarkTextLocalization()
    {
        const string tableName = "RenshuuScene"; // ローカライズテーブルの名前
        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(tableName, "markText");
        string localizedText = localizedString.Replace("{score}", score.ToString());; // 報酬コインの値を文字列に埋め込む
        markText.text = localizedText; // ローカライズされた報酬テキストを更新
    }

    //不回答で全問題不正解にしたい場合
    public void ChangeQues()
    {
        if (piyoCollision == true)
        {
            return;
        }
        StartCoroutine(ChangeQuesCoroutine());
    }
    IEnumerator ChangeQuesCoroutine()
    {
        piyoCollision　= true;
        _doAnsButtonMove.PauseButtonMovement();//AnsButtonの動きを止める
        CalculateMultiplication();
        count++;
        for(int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = false;
            ansButtonCollider[i].enabled = false;
        }
        GameManager.singleton.currentCount = count;
        _wrongAnswerStringBuilder.AppendLine($"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}");
        //wrongAnswer.text += $"{GameMath.instance.valueA.text}×{GameMath.instance.valueB.text}={GameMath.instance.answer}\n";
        // パーティクルを再生
         for (int i = 0; i < AnsButtons.Length; i++)
         {
         //particleManager.PlayParticle(7, AnsButtons[i].transform.position);
         InstantiateParticle(AnsButtons[i].transform.position);
         SoundManager.instance.PlaySE20();//爆発音
         }
         _piyoDamage.DamageCall();//ピヨをダメージを受けさせる
        yield return new WaitForSeconds(0.4f);
        _doAnsTextRotate.RotatePanel();
        yield return new WaitForSeconds(0.5f);
        DelayImageOff();
        DelayMathAnswer();
       
    }
    private void InstantiateParticle(Vector3 position)
    {
        // パーティクルを指定位置にインスタンス化する
        GameObject particleInstance = Instantiate(damageParticlePrefab, position, Quaternion.identity);
        
        // 必要に応じてパーティクルの設定を行う
        // 例: インスタンス化後にパーティクルの速度や色を設定する処理など
    }
    void DelayImageOff()
    {
        _doQuesPanelRotate.InVisibleQuesPanel();
        doRenshuButtonAnim.InvisibleButton();
    }
    void DelayMathAnswer()
    {
        _doAnsTextRotate.ResetAnswerText();
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].enabled = true;
            ansButtonCollider[i].enabled = true;
        }
        if (GameManager.singleton.currentCount >= 9)
        {
            ToStringAnswer();
            _ansButtonCollider.enabled = false;
            _piyoDamage.enabled = false;
            _quesChangeCollider.enabled = false;//コライダーを管理するスクリプトをオフする
            _doGameResultPanel.SetResult();
            print("count9isRenshu");
            return;
        }
        //SoundManager.instance.StopPlaySE();//SEの効果音を停止
        GameMath.instance.MathsProblem();
        piyoCollision　= false;
    }

}









