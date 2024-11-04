using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class GachaManager : MonoBehaviour
{
	// アイテムのデータを保持する辞書
	Dictionary<int, string> itemInfo;

	// 敵がドロップするアイテムの辞書
	Dictionary<int, float> itemDropDict;

    // 抽選結果を保持する辞書
	Dictionary<int, int> itemResultDict;

	// 抽選回数
	int rollNum = 1000;
	public GameObject GachaObject;
	public GameObject GachaObject1;
	public GameObject GachaMana;
	public GameObject closeButton;
	public Button gachaButton;//ガチャガチャのButton
    public List<int> GachaNum = new List<int>();//各要素の基準のインデックスを管理
	public List<int> DeNum = new List<int>();//各要素のデフォルト用List
    public List<string> nameChara = new List<string>();//名前の管理
	public string[] names;
	public string[] setumeis;
	public int[] kakuritu;
	public int NameNum;//名前の個数を取得する
	public GameObject getNekoPanel;
	public Text nameText;//ガチャの結果表示
	public GameObject nekoImage;
	public Image nekochanImage;//Sprite差し替えよう
	public Sprite[] nekoSprites;
	public int nekoNum;
	public GameObject[] neko;//ガチャのアイテム表示
	public GameObject[] balls;//ガチャのカプセル表示
	public GameObject openBallImage;//ガチャの開くBall Imageオブジェクト
	public GameObject pOpenBallImage;//ガチャの開く前BallImageオブジェクト
	public GameObject flashImage;//ガチャの開く前BallImageオブジェクト
	public Text coinText;//所持するcoinの枚数を表示する
	//Gachaのセーブは他のSceneに影響ないはずなのでガチャないでセーブロードする
	public GameObject RightButton;
    public GameObject LeftButton;
	public GameObject PanelAd;//コインが足りない時に表示するようPanel
	public CanvasGroup fadePanel;//fadeよう
	[SerializeField] private GameObject NekoitemPanel;//Gachaでゲットした猫アイテムの説明
	[SerializeField] private GameObject AdButton;//AdPanel内のReward広告を呼び出すButton
	public GameObject rewardText0;//コインが足りませんテキスト
    [SerializeField] private GameObject AdMobManager;
	[SerializeField] private GameObject afterAdPanel;
	public GameObject PanelParent;//ガチャを引いている間に画面が動かないよう一時停止にする
	
	[SerializeField] AdmobGReward GachaAdReward;
	//Debug用
	//public int itemID =1;
	

	void Start(){ 
		if(GameManager.singleton.coinNum < 150){
			GachaAdReward.CreateAndLoadRewardedAd();
			//AdMobManager.GetComponent<AdMobReward>().CreateAndLoadRewardedAd();
		}
		Invoke("StartInvoke",1f);
		coinText.text = GameManager.singleton.coinNum.ToString();
	}

	void StartInvoke(){
		getNekoPanel.SetActive(false);
		NekoitemPanel.SetActive(false);
		PanelAd.SetActive(false);
		flashImage.SetActive(false);
		GameManager.singleton.LoadCoin();
		GachaMana.GetComponent<GachaItem>().SetGachaText();
		//Debug.Log("coinGoukei"+GameManager.instance.totalCoin);
		
		//gachaButton.enabled = true;
		//初回時の取得キャラ反映用defaltの作成 Debugにも使える
		int a = GetComponent<GachaItem>().GachaChara.Length;
		for(int i = 0 ; i < a ;i++){
			DeNum.Add(0);
		}
		DeNum[0]=1;
		
		for(int i = 0 ; i < a ;i++){
			Debug.Log(DeNum[i]);
		}
		//Debug.Log(DeNum.Count);
		GachaNum = ES3.Load("GachaNum","GachaNum.es3",DeNum );
		//Debug.Log(GachaNum.Count);
		
		SetChara();

		InitializeDicts();
		
		/*
		names = GachaObject.GetComponent<GachaItem>().GachaChara;
		setumeis = GachaObject.GetComponent<GachaItem>().setumeiText;
		DebugNames();
		
		GetDropItem();*/
		
		/*
		 //現段階ではGameManagerにレビュー機能はない
		if(GameManager.singleton.SceneCount==5||GameManager.singleton.SceneCount==30||
        GameManager.singleton.SceneCount==800||GameManager.singleton.SceneCount==150){
            GameManager.singleton.RequestReview();
        }*/
		//GetDropItem();Debug用スタート時に抽選を開始する
		PanelParent.GetComponent<PanelChange>().enabled = true;
	}
	void DebugNames()
    {
        for (int i = 0; i < names.Length; i++)
        {
            Debug.Log(i.ToString()+","+names[i]);
            }
    }
	void DebugSetumeis()
    {
        for (int i = 0; i < setumeis.Length; i++)
        {
            Debug.Log(i.ToString()+","+setumeis[i]);
            }
    }
	public void GachaSE(){
		SoundManager.instance.PlaySE3();
	}
	public void GachaReward(){
		afterAdPanel.SetActive(true);
		GachaAdReward.ShowAdMobReward();
		//PanelAd.SetActive(false);
		
	}
	//アイテムPanel,GetPanel共通のOkButton
	public void CloseAdPanelManager(){
		SoundManager.instance.PlaySE3();
		
		PanelAd.SetActive(false);
		RightButton.SetActive(true);
		LeftButton.SetActive(true);
		PanelParent.GetComponent<PanelChange>().enabled = true;
	}

    //ガチャのGetNekoPanelないのガチャ終了後のOKボタン
	public void OkButton(){
		if(GameManager.singleton.coinNum < 150){
			GachaAdReward.CreateAndLoadRewardedAd();
			}
		RightButton.SetActive(true);
		LeftButton.SetActive(true);
		PanelParent.GetComponent<PanelChange>().enabled = true;
		
		closeButton.SetActive(false);
		SoundManager.instance.PlaySE3();
		NekoitemPanel.SetActive(false);
	
		flashImage.SetActive(false);
		openBallImage.SetActive(true);
		getNekoPanel.SetActive(false);

		rewardText0.SetActive(true);
		PanelAd.SetActive(false);
	}

	public void OpenPanel()
	{
		RightButton.SetActive(false);
		LeftButton.SetActive(false);
		PanelParent.GetComponent<PanelChange>().enabled = false;
	}
	public void CloseItemPanel()
	{
		NekoitemPanel.SetActive(false);
		RightButton.SetActive(true);
		LeftButton.SetActive(true);
		PanelParent.GetComponent<PanelChange>().enabled = true;
		SoundManager.instance.PlaySE3();
	}
	//コインボタンを押すとAdPanelが出てくる
	public void GetCoin(){
		GachaAdReward.CreateAndLoadRewardedAd();
		PanelAd.SetActive(true);
		rewardText0.SetActive(false);
		SoundManager.instance.PlaySE3();
		RightButton.SetActive(false);
		LeftButton.SetActive(false);
		PanelParent.GetComponent<PanelChange>().enabled = false;
	}

	public void GetDropItem(){	
		//画面遷移までガチャボタン押せなくなる
		gachaButton.enabled = false;
		//画面遷移までスワイプできなくなる
		PanelParent.GetComponent<PanelChange>().enabled = false;
		/*coinが150枚以下ならガチャはできない*/
		
		if(GameManager.singleton.coinNum < 150){
			GachaAdReward.CreateAndLoadRewardedAd();
			PanelAd.SetActive(true);
			RightButton.SetActive(false);
			LeftButton.SetActive(false);
			SoundManager.instance.PlaySE3();
			return;
		}
	
		//見せかけだけコインを減らす。ガチャ実行後にcoinNumを減らして保存
		int temptCoin = GameManager.singleton.coinNum;
		temptCoin -= 150;
		Debug.Log($"temptCoin_{temptCoin}");
		coinText.text = temptCoin.ToString();
		//Debug時はオフ
		
		RightButton.SetActive(false);
		LeftButton.SetActive(false);
	
        //ドロップアイテムの抽選の時
        int itemId = Choose();

		// アイテムIDに応じたメッセージ出力
		nekoNum = itemId;
	
		  //nekoNum = itemID;//Debug
		  string itemName = itemInfo[itemId];
			//nameText.text = itemName + "\n をゲット!";
			Debug.Log(itemName + " をゲット!");
			Debug.Log("nekoNum"+nekoNum);
		
		int ringiNum = GachaNum[nekoNum];
		
		//Debug表示用
		/*itemID++;//DEbug
		if(itemID>GachaNum.Count){
			itemID = 0;
		}
		Debug.Log("itemID"+itemID);
		//
		*/

		ringiNum++;
		GachaNum[nekoNum] = ringiNum;
		Debug.Log(GachaNum[nekoNum]);
		SetChara();
		ES3.Save("GachaNum",GachaNum,"GachaNum.es3" );
		Debug.Log(GachaNum[nekoNum]);

        // Debugで確率を確認したい時のスクリプトここから確認用
        
         //rollNum回数分だけ発注を実績できる
         /*
		for (int i = 0 ; i < rollNum; i++){
			int itemId = Choose();
			if (itemResultDict.ContainsKey(itemId)){
				itemResultDict[itemId]++;
			} else {
				itemResultDict.Add(itemId, 1);
			}
		}
		foreach (KeyValuePair<int, int> pair in itemResultDict){
			string itemName = itemInfo[pair.Key];
			Debug.Log(itemName + " は " + pair.Value + " 回でした。");
		}*/
		//DebugNames();
		
		//デバッグのため一時削除
		StartCoroutine(ItemGet());
	}
	public void SetChara(){
		for(int i = 0; i < GachaNum.Count; i++)
		{
				int a = GachaNum[i];
				if(a > 0)
				{
					neko[i].SetActive(true);
					balls[i].SetActive(false);
				}
		}
	}

	//ガチャでアイテムを表示させる
	IEnumerator ItemGet()
	{
		SoundManager.instance.PlaySE15Gacha();//ガチャ音
		yield return new WaitForSeconds(0.4f);
		getNekoPanel.SetActive(true);
		gachaButton.enabled = true;
		nameText.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference ="getNekop_nameText";
		nekoImage.SetActive(false);
		closeButton.SetActive(false);
		openBallImage.SetActive(true);
		openBallImage.GetComponent<DOGachaBall>().BallShake();//アニメーション
		yield return new WaitForSeconds(0.8f);
		yield return fadePanel.DOFade(0.9f,0.4f).WaitForCompletion();
		fadePanel.DOFade(0,0.4f);
		openBallImage.SetActive(false);
		nameText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.4f);
		//yield return new WaitForSeconds(0.2f);
		//string name = GetComponent<GachaItem>().GachaChara[nekoNum];//nameで取得した"."を改行に置き換える
		string nekoNameKey = "NekoName_" + nekoNum; // 例: "NekoName_0", "NekoName_1", ...
		nameText.gameObject.SetActive(true);
		nameText.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference = nekoNameKey;
		//nameText.text = name.Replace(".",System.Environment.NewLine);
		//nameText.text = GetComponent<GachaItem>().GachaChara[nekoNum];
		nekoImage.SetActive(true);
		nekochanImage.sprite = GetComponent<GachaItem>().ItemNeko[nekoNum];
		nekoImage.GetComponent<DOGachaBall>().BallShakeLoop();
		//SoundManager.instance.PlaySE3();
		flashImage.SetActive(true);
		flashImage.GetComponent<DOflash>().Flash18();
		SoundManager.instance.PlaySE18();//ジャン音
		//ガチャ成功後コインを減らして保存する
		GameManager.singleton.coinNum -= 150;
		GameManager.singleton.CoinSave();
		//nameText.text = itemName + "\nをゲットした"
		yield return new WaitForSeconds(0.4f);
		closeButton.SetActive(true);
		yield break;
	}

	//読み込む内容を辞書に格納する
	void InitializeDicts(){
		names = GachaObject.GetComponent<GachaItem>().GachaChara;
		itemInfo = new Dictionary<int, string>();
		for(int i =0;i<names.Length;i++){
			itemInfo.Add(i, names[i]);
		}
		kakuritu = GachaObject.GetComponent<GachaItem>().charaKakuritu;
		itemDropDict = new Dictionary<int, float>();
		for(int i =0;i<kakuritu.Length;i++){
			itemDropDict.Add(i, kakuritu[i]);
		}
        
        //Debugで確率の設定による実行結果を見たいときは以下ののスクリプト
        itemResultDict = new Dictionary<int, int>();
	}

	//ガチャを行う
	int Choose(){
		// 確率の合計値を格納
		float total = 0;

		// 敵ドロップ用の辞書からドロップ率を合計する
		foreach (KeyValuePair<int, float> elem in itemDropDict){
			total += elem.Value;
		}

		// Random.valueでは0から1までのfloat値を返すので
		// そこにドロップ率の合計を掛ける
		float randomPoint = UnityEngine.Random.value * total;

		// randomPointの位置に該当するキーを返す
		foreach (KeyValuePair<int, float> elem in itemDropDict){
			if (randomPoint < elem.Value){
				return elem.Key;
			} else {
				randomPoint -= elem.Value;
			}
		}
		return 0;
	}

     /*0ゆうしゃネコ
	 1おさかなくわえたネコ,2ふくめんネコ,3ねこかぶりネコ,4たまごかぶりネコ
	 5カボチャかぶりネコ,6ガクランネコ,7スイカかぶりネコ,8まほうつかいネコ
	 9メイドネコ　なつ,10ダイビングねこ,11ゆうれいネコ,12メイドネコ　ふゆ
	 13ギャングネコ,14セーラーふくネコ,15てんしネコ,16にんぎょネコ
	 17まおうネコ,18ねこかん　かつお,19ねこかん　しらす
	 20ねこかん　サーモン,21キャットフードまぐろ,22キャットフードチキン
	 23キャットフードかつお,24ねずみのおもちゃ,25ねこのぬいぐるみ
	 26さかなのおもちゃ,27ラグビーボールおもちゃ,28テニスボールおもちゃ
	 29かわいいくびわ,30ブラシ,31ねこシャンプー
	 32ねこトリートメント,33みみベッド,34リボンクッション
	 35キャットタワー*/

}

