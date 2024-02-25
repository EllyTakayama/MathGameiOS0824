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
    [SerializeField] private Slider[] unkoSliders; // ピヨちゃんに対応するSliderの配列
    [SerializeField] private GameObject[] unkoPrefabs;//unkoPrefab
    private int foodIndex;//foodのインデックス
    public FoodType currentFoodType;
    public float foodValidityTime = 300f;//Foodの有効時間の取得と参照のため
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
        LoadCurrentFoodType();
        // birdObjectsにEventTriggerを追加し、OnClickメソッドをアタッチ
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
        }
    }
    public void SpawnUnkoByFoodType(int birdIndex,FoodType foodType)
    {
        Debug.Log("Spawn1");
        // birdIndexが配列の範囲内にあることを確認
        if (birdIndex < 0 || birdIndex >= birdObjects.Length)
        {
            Debug.LogWarning("Invalid birdIndex specified for SpawnUnkoByFoodType method.");
            return;
        }

        // 指定されたbirdObjectとunkoSliderを取得
        GameObject selectedBirdObject = birdObjects[birdIndex];
        Slider selectedUnkoSlider = unkoSliders[birdIndex];
        Debug.Log($"birdIndex_{birdIndex}");
        
        // selectedUnkoSliderのvalueが1未満の場合はうんこを生成しない
        if (selectedUnkoSlider.value < 1f)
        {
            Debug.Log("selectedUnkoSlider.valueが1未満");
            return;
        }
        // FoodTypeに応じてSpawnするUnkoの種類を選択
        GameObject unkoPrefab = GetUnkoPrefabByFoodType(currentFoodType);

        // birdObjectの位置にUnkoをSpawnする
        Instantiate(unkoPrefab, selectedBirdObject.transform.position, Quaternion.identity);

        // unkoSliderのvalueを0に設定
        selectedUnkoSlider.value = 0f;
        selectedUnkoSlider.gameObject.GetComponent<BirdSlider>().FullSlider();

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
        if (ES3.KeyExists("CurrentFoodType"))
        {
            int foodTypeValue = ES3.Load<int>("CurrentFoodType", "CurrentFoodType.es3");
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
    }
    // birdObjectsのOnClickイベントで呼ばれるメソッド
    public void OnBirdClick(GameObject selectedBirdObject)
    {
        // birdObjectsの配列内でのインデックスを取得
        int birdIndex = System.Array.IndexOf(birdObjects, selectedBirdObject);
        // 選択された鳥に対する処理を実行
        SpawnUnkoByFoodType(birdIndex,currentFoodType);
        Debug.Log("Selected bird index: " + birdIndex);
    }
    //Foodアイコンを選択することでcurrentFoodTypeを変更する
    public void SetFoodType(int foodI)
    {
        FoodType currentFoodType;
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
    }

    GameObject GetUnkoPrefabByFoodType(FoodType foodType)
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
