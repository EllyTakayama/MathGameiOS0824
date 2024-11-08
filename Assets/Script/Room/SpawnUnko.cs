using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum FoodType
{
    Basic,
    Blue,
    Pink,
    Green,
    Purple,
    Yellow,
    Happy,
    Gold,
    Rainbow,
   
}
public class SpawnUnko : MonoBehaviour
{
    [SerializeField] private GameObject[] birdObjects; // ピヨちゃんのGameObjectの配列
    //[SerializeField] private Slider[] unkoSliders; // ピヨちゃんに対応するSliderの配列
    [SerializeField] private GameObject[] unkoPrefabs;//unkoPrefab
    public int foodIndex;//foodのインデックス
    public FoodType currentFoodType;
    public float foodValidityTime = 300f;//Foodの有効時間の取得と参照のため
    [SerializeField] private GameObject gachaPanel0;//親の設定のため
    [SerializeField] private GameObject[] foodChoiceFrame;//選択されたPerchを示すImage
    private Dictionary<FoodType, float> foodTypeToTime = new Dictionary<FoodType, float>()
    {
        { FoodType.Basic, 5f * 60f }, // 5分を秒に変換
        { FoodType.Blue, 10f * 60f },
        { FoodType.Pink, 10f * 60f },
        { FoodType.Green, 10f * 60f },
        { FoodType.Purple, 10f * 60f },
        { FoodType.Yellow, 10f * 60f },
        { FoodType.Happy, 20f * 60f },
        { FoodType.Gold, 20f * 60f },
        { FoodType.Rainbow, 20f * 60f }
    };
    
    void Start()
    {
        // デフォルト値の設定
            currentFoodType = FoodType.Basic;
        // ロード処理
        LoadCurrentFoodType();//foodIndexをロード
        SetFoodFrame();//フレームを設定
        // birdObjectsにEventTriggerを追加し、OnClickメソッドをアタッチ
        /*
        foreach (GameObject birdObject in birdObjects)
        {
            EventTrigger trigger = birdObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = birdObject.AddComponent<EventTrigger>();
            }
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnBirdClick(birdObject); });
            trigger.triggers.Add(entry);
        }*/
    }
    public void SpawnUnkoByFoodType(Vector3 spawnPosition)
    {
        // FoodTypeに応じてSpawnするUnkoの種類を選択
        GameObject unkoPrefab = GetUnkoPrefabByFoodType(currentFoodType); 
        // UnkoをSpawnして親を設定する
        GameObject unkoInstance = Instantiate(unkoPrefab, spawnPosition, Quaternion.identity);
        unkoInstance.transform.SetParent(gachaPanel0.transform);
        SoundManager.instance.PlaySE2();//Unkoのサウンド音
    }
    
    //FoodTypeのセーブ
    void SaveCurrentFoodType()
    {
        // currentFoodTypeをintに変換してセーブ
        int foodTypeValue = (int)currentFoodType;
        ES3.Save<int>("CurrentFoodType", foodTypeValue, "CurrentFoodType.es3");
    }

    //FoodTypeのダウンロード
    void LoadCurrentFoodType()
    {
        // セーブデータからintをロードし、FoodTypeに変換
        if (ES3.KeyExists("CurrentFoodType","CurrentFoodType.es3"))
        {
            int foodTypeValue = ES3.Load<int>("CurrentFoodType", "CurrentFoodType.es3");
            foodIndex = foodTypeValue;//foodIndexにロードする
            currentFoodType = (FoodType)foodTypeValue;
        }
        else
        {
            currentFoodType = FoodType.Basic; // Keyが存在しない場合はデフォルト値を使用
        }
    }

    // 他の処理やイベントでcurrentFoodTypeが変更された場合に呼び出す
    void OnFoodTypeChanged()
    {
        SaveCurrentFoodType();
    }
    //Sliderのvalueをコルーチンで時間をかけて1まで戻したい
    IEnumerator IncreaseUnkoValue(Slider slider)
    {
        while (slider.value < 1f)
        {
            slider.value += Time.deltaTime * 0.1f; // 増加率は適宜調整
            yield return null;
        }
        yield break;
    }
    // birdObjectsのOnClickイベントで呼ばれるメソッド
    public void OnBirdClick(GameObject selectedBirdObject)
    {
        // birdObjectsの配列内でのインデックスを取得
        //int birdIndex = System.Array.IndexOf(birdObjects, selectedBirdObject);
        // 選択された鳥に対する処理を実行
        //SpawnUnkoByFoodType(birdIndex,currentFoodType);
        //Debug.Log("Selected bird index: " + birdIndex);
    }
    //Foodアイコンを選択することでcurrentFoodTypeを変更する
    public void SetFoodType(int foodI)
    {
        foodIndex = foodI;
        switch (foodI)
        {
            case 0://普通の茶色のうんこ
                currentFoodType = FoodType.Basic;
                break;
            case 1://
                currentFoodType = FoodType.Blue;
                break;
            case 2:
                currentFoodType = FoodType.Pink;
                break;
            case 3://普通の茶色のうんこ
                currentFoodType = FoodType.Green;
                break;
            case 4://
                currentFoodType = FoodType.Purple;
                break;
            case 5:
                currentFoodType = FoodType.Yellow;
                break;
            case 6://普通の茶色のうんこ
                currentFoodType = FoodType.Happy;
                break;
            case 7://
                currentFoodType = FoodType.Gold;
                break;
            case 8:
                currentFoodType = FoodType.Rainbow;
                break;
            // 他のfoodIに対するFoodTypeの割り当てを追加する
            default:
                currentFoodType = FoodType.Basic;
                Debug.LogWarning("Unknown foodI specified for SetFoodType method.");
                return;
        }
        // 有効時間の取得
        foodValidityTime = foodTypeToTime[currentFoodType];
        Debug.Log("Selected FoodType: " + currentFoodType + ", foodValidityTime: " + foodValidityTime);
        SaveCurrentFoodType();//セットしたfoodTypeの取得
        SetFoodFrame();
    }
    public void SetFoodFrame()
    {
        for (int i = 0; i < foodChoiceFrame.Length; i++)
        {
            foodChoiceFrame[i].SetActive(false);
        }
        foodChoiceFrame[foodIndex].SetActive(true);
    }

    public GameObject GetUnkoPrefabByFoodType(FoodType foodType)
    {
        switch (foodType)
        {
            case FoodType.Basic:
                foodIndex = 0;
                break;
            case FoodType.Blue:
                foodIndex = 1;
                break;
            case FoodType.Pink:
                foodIndex = 2;
                break;
            case FoodType.Green:
                foodIndex = 3;
                break;
            case FoodType.Purple:
                foodIndex = 4;
                break;
            case FoodType.Yellow:
                foodIndex = 5;
                break;
            case FoodType.Happy:
                foodIndex = 6;
                break;
            case FoodType.Gold:
                foodIndex = 7;
                break;
            case FoodType.Rainbow:
                foodIndex = 8;
                break;
            // 他のFoodTypeに対するPrefabのロードを追加する
            default:
                foodIndex = 1;
                Debug.LogWarning("Unknown FoodType specified for GetUnkoPrefabByFoodType method.");
                break;
        }
        // foodIndexに基づいて適切なUnkoのPrefabを返す
        return unkoPrefabs[foodIndex];
    }
}
