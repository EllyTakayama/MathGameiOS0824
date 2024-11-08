using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using System;

public class GachaItem : MonoBehaviour
{
    //public GameObject[] characters;//キャラクター画像の管理
    //public GameObject[] Hatenas;//クエスチョン画像の管理
    public List<int> CharaId = new List<int>();
    //public List<string> CharaName = new List<string>();//名前の管理
    //public List<string> setsumei = new List<string>();//キャラ説明テキストの管理
    public string[] GachaChara;//textアセットからキャラ名の代入
    public string[] setumeiText;//textアセットからキャラ説明テキストの代入
    public int[] charaKakuritu;//各キャラの確率を取得
    public string[] kakuritu;//テキストアセットからstringのまま確率を取得する
    public Sprite[] ItemNeko;//各キャラのスプライト画像
    [SerializeField] private GameObject NekoitemPanel;//Gachaでゲットした猫アイテムの説明
	[SerializeField] private Text NnameText;//アイテムPanelの猫の名前
	[SerializeField] private Text NsetumeiText;//アイテムPanelの猫の説明
    [SerializeField] private Image NnekoItemImage;//アイテムPanelの猫の説明
    [SerializeField] private GameObject LeftButton;//ひだりボタン
    [SerializeField] private GameObject RightButton;//みぎボタン
    [SerializeField] private BirdOnItemManager _birdOnItemManager;//birdOnItemManagerの取得　perch設定
    public int ItemButtonIndex;//Buttonの引数をIndexとして取得する
    [SerializeField] private GachaManager _gachaManager;//gachaManager.csを取得する
    [SerializeField] private SpawnUnko _spawnUnko;//SpawnUnko.csを取得する
     //テキストデータを読み込む
    [SerializeField] TextAsset GcharaName;
    [SerializeField] TextAsset GcharaSetumei;
    [SerializeField] TextAsset GcharaKakuritu;

    // Start is called before the first frame update
    void Start()
    {
        SetGachaText();
        DebugChara();
        DebugSetumei();
        DebugKKakuritu();
        DebugKakuritu();
       
    }
    public void SetGachaText(){
        GachaChara = GcharaName.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        setumeiText = GcharaSetumei.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        kakuritu = GcharaKakuritu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        charaKakuritu = new int[kakuritu.Length];
        for(int i = 0; i < kakuritu.Length;i++){
            charaKakuritu[i] = int.Parse(kakuritu[i]); 
        }

    }

     //Gachaでゲットしたアイテムの説明Panel表示
	public void ChoiceItem(int ButtonNum){
        int i = GetComponent<GachaManager>().GachaNum[ButtonNum];
        ItemButtonIndex = ButtonNum;
        print("i,"+i);
        NekoitemPanel.SetActive(true);
        // NekoNameのローカライズキーを設定
        string nekoNameKey = "NekoName_" + ButtonNum; // 例: "NekoName_0", "NekoName_1", ...
        // NekoNameのローカライズキーを設定
        // ItemNameのローカライズキーを設定
        string itemNameKey = "ItemName_" + ButtonNum; // 例: "ItemName_0", "ItemName_1", ...
        string NekoName = GachaChara[ButtonNum];
        NnameText.text = NekoName.Replace(".",System.Environment.NewLine);
        string ItemName = setumeiText[ButtonNum];
        NsetumeiText.text = ItemName.Replace(".",System.Environment.NewLine);
        NnameText.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference = nekoNameKey;
        NsetumeiText.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference = itemNameKey;
        NnekoItemImage.sprite = ItemNeko[ButtonNum];
        SoundManager.instance.PlaySE3();
        _gachaManager.OpenPanel();//スワイプを止める
        //RightButton.SetActive(false);
        //LeftButton.SetActive(false);
	}

    public void SetItem()
    {
        if (ItemButtonIndex < 9)
        {
            // BirdOnItemManagerにperchNumを設定してShowRandomPerchメソッドを呼び出す
            _birdOnItemManager.perchIndex = ItemButtonIndex; // perchNumを設定
            _birdOnItemManager.ShowRandomPerch(); // ShowRandomPerchメソッドとフレームセット
        }
        else
        {
            int foodIndex = ItemButtonIndex - 9;
            _spawnUnko.SetFoodType(foodIndex);//Indexをセーブまでできる
        }

        _gachaManager.CloseItemPanel();
    }

    void DebugChara()
    {
        for (int i = 0; i < GachaChara.Length; i++)
        {
            Debug.Log(i.ToString()+","+GachaChara[i]);
            }
    }
    void DebugSetumei()
    {
        for (int i = 0; i < setumeiText.Length; i++)
        {
            ///GachaChara[i] = GachaChara[i].Replace(".",System.Environment.NewLine);
            Debug.Log(i.ToString()+","+setumeiText[i]);
            }
    }
    void DebugKKakuritu()
    {
        for (int i = 0; i < kakuritu.Length; i++)
        {
            Debug.Log(i.ToString()+","+kakuritu[i]);
            }
        Debug.Log("確率テキスト要素数"+kakuritu.Length);
    }
    void DebugKakuritu()
    {
        for (int i = 0; i < charaKakuritu.Length; i++)
        {
            Debug.Log(i.ToString()+","+charaKakuritu[i]);
            }
        Debug.Log("確率要素数"+charaKakuritu.Length);
    }
   
}
