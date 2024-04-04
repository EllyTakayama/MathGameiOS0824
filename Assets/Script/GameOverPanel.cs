using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    //[SerializeField] private GameObject fruitsEffect;
    [SerializeField] private GameObject gameOverMarkText;
    [SerializeField] private Button[] _rewardButtons;//GiftのImageをした3つのButton
    [SerializeField] private Sprite[] _rewardGiftSprites;//ButtonのImage用Sprite
    //[SerializeField] private DORenshuButtonAnim _doRenshuButtonAnim;//rewardButtonを表示させるスクリプト
    [SerializeField] private DoButton[] _doButtons;//rewardButtonをアニメーションさせるスクリプトを格納する配列
    [SerializeField] private GameObject _rewardPanel;//gameOverPanelのgiftを選択したら表示される
    [SerializeField] private Image _rewardGiftBoxImage;
    [SerializeField] private int[] GiftImageIndex = new int[3];
    [SerializeField] private GameObject _flyPiyo;
    private bool giftReward;//trueの時のみ_doButtons.StartRewardButtonAnimationを実行する
    [SerializeField] private DoCoinAnim coinAnimScript; // DoCoinAnim.csのインスタンスをインスペクターから参照するための変数
    private int _rewardCoin;//抽選で獲得したcoinの値
    [SerializeField] private TextMeshProUGUI coinValueText;//coinStatusの孫要素
    [SerializeField] private Text rewardText;//コインの報酬を表示する
    [SerializeField] private EndImage _endImage;//giftゲットの後のパネル表示
    [SerializeField] private GameObject _flashImage;//flash
    [SerializeField] private GameObject giftRewardPanel;
    //[SerializeField] private DOTweenPanel _doTweenPanel;//DOTweenPanel.csの参照
    [SerializeField] private CheckButton _checkButton;//リセットのため
    [SerializeField] private GameObject[] clouds;//雲のGameObject
    /*確率の実行のため
    public int totalExecutions = 100; // 実行回数
    public Dictionary<int, int> rewardCoinCounts = new Dictionary<int, int>(); // 各報酬の出現回数を記録する辞書
    void Start()
    {
      
        // 辞書を初期化
        rewardCoinCounts.Add(50, 0);
        rewardCoinCounts.Add(100, 0);
        rewardCoinCounts.Add(300, 0);
        
        //gameOverMarkText.SetActive(true);
        
        Kakuritu();
    }

    public void Kakuritu()
    {
        for (int i = 0; i < totalExecutions; i++)
        {
            RandomResult();
        }

        ShowRewardCoinCounts();
    }
    void ShowRewardCoinCounts()
    {
        Debug.Log("Reward Coin Counts:");
        foreach (var kvp in rewardCoinCounts)
        {
            Debug.Log("Coin: " + kvp.Key + ", Count: " + kvp.Value);
        }
    }確率の試算終わり
    */
    //リセットするときに
    public void ResetGameOverPanel()
    {
        _rewardPanel.SetActive(false);
        gameOverPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(1200, 0);
    }
    public void DoGameOverPanel()
    {
        coinValueText.text = GameManager.singleton.coinNum.ToString();
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].SetActive(true);
            clouds[i].GetComponent<CloudMove>().CloudUpDown();//雲を有効にしてアニメーションを実行する
        }
        int[] shuffledIndexes = new int[_rewardGiftSprites.Length];
        for (int i = 0; i < _rewardGiftSprites.Length; i++)
        {
            shuffledIndexes[i] = i;
        }
        // シャッフル
        ShuffleArray(shuffledIndexes);

        // 各ボタンの Sprite をランダムに設定
        for (int buttonIndex = 0; buttonIndex < _rewardButtons.Length; buttonIndex++)
        {//シャッフルしたインデックスでRewardButtonのImageにSpriteを割り当てていく
            _rewardButtons[buttonIndex].image.sprite = _rewardGiftSprites[shuffledIndexes[buttonIndex]];
            GiftImageIndex[buttonIndex] = shuffledIndexes[buttonIndex];//各GiftButtonへのSpriteインデックスを取得
            _doButtons[buttonIndex].RewardGiftOn();//Giftの表示
        }
        //_doRenshuButtonAnim.ResetButton();
        DoButtonCoroutine();//animation
        //gameOverMarkText.SetActive(true);
        gameOverPanel.GetComponent<RectTransform>()
        .DOAnchorPos(new Vector2(0, 0), 1.5f)
    .SetEase(Ease.OutBack)
    .SetLink(gameObject)
    .OnComplete(() =>
            {
                SoundManager.instance.PlaySE3();
                _checkButton.ResetButtonScore();
                
            });//ここまでOnCompleteで呼ばれるメソッド
        SoundManager.instance.PlayBGM("GameOverPanel");

    }

    public void AftreGift()
    {
        StartCoroutine(AfterGiftCoroutine());
    }

    IEnumerator AfterGiftCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        
        _flyPiyo.SetActive(true);
        _flashImage.SetActive(false);
        _rewardPanel.SetActive(false);
        _endImage.DoEndImage();//EndImage.csを呼び出す
        yield return new WaitForSeconds(0.8f);
        _endImage.RewardCall();
    }
    //_doButtonを実行する
    public void DoButtonCoroutine()
    {
        giftReward = true;//whileの条件をtrueにする
        StartCoroutine(GiftTweenCoroutine());
    }
    
    //ButtonのDOTweenを時間差で実行するためのコルーチン
    IEnumerator GiftTweenCoroutine()
    {
        while (giftReward)
        {
            for (int i = 0; i < _doButtons.Length; i++)
            {
                // giftReward の値が変更されたらループを抜ける
                if (!giftReward)
                    yield break;
                _doButtons[i].StartRewardButtonAnimation();
                yield return new WaitForSeconds(0.9f);
                //各Buttonのアニメーションは0.3*3他の2ボタン分待機する
                
            }
            // すべてのアニメーションが終了した後、再度 Coroutine を呼び出す
            yield return null; // 全てのアニメーションが完了するのを待つ
        }
    }
    //Giftを選ぶと実行されるメソッド
    public void ChoiceRewardGift(int ButtonNum)
    {
        giftReward = false;//GiftTweenCoroutineのwhileの条件をfalseにする

        StartCoroutine(ChoiceGiftCoroutine(ButtonNum));
        RandomResult();//抽選でcoinの枚数を取得する
    }



    IEnumerator ChoiceGiftCoroutine(int Button)
    {
        // Tweenアニメーションを停止する
        foreach (var button in _doButtons)
        {
            button.StopRewardButtonAnimation();
        }
        yield return new WaitForSeconds(0.2f);
        foreach (var button in _doButtons)
        {
            button.ResetRewardButtonAnimation();
        }
        foreach (var button in _doButtons)
        {
            button.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.4f);
        _flyPiyo.SetActive(false);//ピヨを非表示にする
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].SetActive(false);
            
        }
        SoundManager.instance.PlaySE19();//Panel出現の効果音パッ
        _rewardPanel.SetActive(true);
        _rewardGiftBoxImage.sprite = _rewardGiftSprites[GiftImageIndex[Button]];
        _flashImage.SetActive(true);
        _flashImage.GetComponent<DOflash>().Flash18();
        yield return new WaitForSeconds(0.8f);
        _rewardGiftBoxImage.GetComponent<DoButton>().OnAnswerButtonClick();
        yield return new WaitForSeconds(1.2f);
        SoundManager.instance.PlaySE16GetCoin();//コインの音を長くならす
        if (coinAnimScript != null)
        {
            coinAnimScript.SpawnRewardCoin();
        }
        yield return new WaitForSeconds(0.5f);
        //coinValueText.gameObject.SetActive(true);
        coinValueText.text = (GameManager.singleton.coinNum).ToString();
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlaySE18();//効果音ジャン
        rewardText.gameObject.SetActive(true);
        rewardText.text = $"コイン{_rewardCoin}まい";
        rewardText.GetComponent<DOTextBounceAnim>().TextBounce();//rewardTextをバウンスさせるアニメーション
        
    }
    
    
    public void RandomResult()
    {
        //rewardCoin = 50の場合20%、rewardCoin = 100の場合30%、rewardCoin = 100の場合50%
        _rewardCoin = 0;

        // 0から99のランダムな数値を生成
        int randomValue = Random.Range(0, 101);

        // 確率に基づいて報酬を抽選
        if (randomValue < 21)
        {
            _rewardCoin = 50;
        }
        else if (randomValue < 51)
        {
            _rewardCoin = 100;
        }
        else
        {
            _rewardCoin = 300;
        }
        Debug.Log($"前：coinNum_{GameManager.singleton.coinNum}");
        GameManager.singleton.coinNum += _rewardCoin;
        Debug.Log($"リワードコインをプラス後：coinNum_{GameManager.singleton.coinNum}");
        /*　確率を試算したい時はコメントアウト外す
        // 報酬の出現回数を更新
        rewardCoinCounts[rewardCoin]++;*/

    }
    
    // 配列をシャッフルするメソッド
    void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }
}
