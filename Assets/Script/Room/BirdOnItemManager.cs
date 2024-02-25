using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdOnItemManager : MonoBehaviour
{
public GameObject[] perches; // 止まり木オブジェクトの配列
public GameObject[] birdObjects; // ピヨちゃんのGameObjectの配列
[SerializeField] private GameObject _BirdOnItemPoint;//選択する親のアイテム
[SerializeField] private int numBirds;//鳥の出現できる数
[SerializeField] private int percheNum;//選択する止まり木のインデックス
// 各perchesに対応した表示位置を表す整数のリスト
private int[] perchePositions = new int[] {5,4,4,5,4,3,5,1,4};
public Slider[] unkoSliders; // ピヨちゃんに対応するSliderの配列
private int birdIndex;//鳥をランダムに選ぶint
private int[] birdArray = new int[] {0, 120, -120, -240, 240};
[SerializeField] private SpawnUnko spawnUnko; // SpawnUnkoスクリプトへの参照
[SerializeField] private float foodValidityTime; // 食べ物の有効時間
private int[] birdIndexShuffle = new int[] {0,1,2,3,4,5,6,7,8,9,10};//表示されたbirdIdexのシャッフルのため
private int[] birdIndexSave = new int[] {0,0,0,0,0,0,0,0,0,0,0};//表示されたbirdIdexの保存のため
[SerializeField] private Image foodImage;//foodイメージ
[SerializeField] private Sprite[] foodSprite;//foodSpriteの差し替え

void Start()
{
    NumBirdsLoad();
    numBirds = 3;
    //SetBirdsOnPerches(); // セーブデータに基づいてピヨちゃんを止まり木に配置する
    
}

public void SetFood()
{
    StartCoroutine(SetFoodBird());
}

IEnumerator SetFoodBird()
{
    // 食べ物のイメージを設定
    foodImage.sprite = foodSprite[0];
    // 1秒待ってからSetFood()を呼び出す
    yield return new WaitForSeconds(0.2f);
    // delayBetweenPerches秒待ってからSetBirdsOnPerches()を呼び出す
    yield return new WaitForSeconds(0.2f);
    SetBirdsOnPerches();
}

public void NumBirdsLoad()
{
    numBirds = ES3.Load<int>("numBirds","numBirds.es3",1);
}
// 食べ物の有効時間を減らすコルーチン
    IEnumerator DecreaseValidityTime()
    {
        foodValidityTime = GetComponent<SpawnUnko>().foodValidityTime;
        while (foodValidityTime > 0f)
        {
            yield return new WaitForSeconds(1f); // 1秒待つ
            foodValidityTime -= 1f; // 1秒ずつ減らす
            Debug.Log("Food Validity Time: " + foodValidityTime);
        }

        // foodValidityTimeが0以下になったら、表示されているbirdObjectとunkoSliderを非表示にする
            if (foodValidityTime <= 0f)
            {
                foreach (GameObject birdObject in birdObjects)
                {
                    birdObject.SetActive(false);
                }

                foreach (Slider slider in unkoSliders)
                {
                    slider.gameObject.SetActive(false);
                }

                foodImage.sprite = foodSprite[1];
                // コルーチンを終了する
                yield break;
            }
    }

    void SetBirdsOnPerches()
{
    // ランダムに選択された止まり木オブジェクトのインデックス
    int perchIndex = Random.Range(0, perches.Length);

    // 選択された止まり木オブジェクト
    GameObject selectedPerch = perches[perchIndex];
    numBirds = perchePositions[perchIndex];
    selectedPerch.SetActive(true); // 選択された止まり木オブジェクトをアクティブにする
    int[] tempArray = new int[numBirds];
    for (int j = 0; j < numBirds; j++)
    {
        tempArray[j] = birdArray[j];
    }
    //birdObjectを配置する位置をシャッフルする
    ShuffleArray(tempArray);
    //birdを重複しないようシャッフルする
    ShuffleArray(birdIndexShuffle);
    ClearBirdIndexSave();
    // ピヨちゃんの数だけ配置
    for (int i = 0; i < numBirds; i++)
    {
        // ピヨちゃんのオブジェクトをランダムに選択
        //birdIndex = Random.Range(0, birdObjects.Length);
        birdIndex = birdIndexShuffle[i];
        birdIndexSave[birdIndex] = 1;
        Debug.Log($"bridIndex_{birdIndex}");
        GameObject selectedBirdObject = birdObjects[birdIndex];
        selectedBirdObject.SetActive(true); // 選択されたピヨちゃんのオブジェクトをアクティブにする

        // 表示位置の設定
        float xPos = 0f;
        // numBirdsによって分岐
        if (numBirds == 1)
        {
            xPos = 0f;
        }
        else
        {
           xPos = tempArray[i];
            Debug.Log($"xPos_{xPos}");
        }
        
        selectedBirdObject.transform.localPosition = new Vector3(xPos, 0f, 0f); // ピヨちゃんのオブジェクトの位置を設定

        if (birdIndex < unkoSliders.Length)
        {
            Slider slider = unkoSliders[birdIndex];
            slider.gameObject.SetActive(true);
            RectTransform sliderRectTransform = slider.GetComponent<RectTransform>();
            if (sliderRectTransform != null)
            {
                sliderRectTransform.localPosition = new Vector3(xPos, sliderRectTransform.localPosition.y, sliderRectTransform.localPosition.z);
            }
        }
        // birdIndexSave配列をセーブする
        ES3.Save<int[]>("birdIndexSave",  birdIndexSave,"birdIndexSave.es3");
    }
    // foodValidityTimeの減少を開始するコルーチンを開始
    StartCoroutine(DecreaseValidityTime());
}
    //birdIndexSave配列を0にするメソッド
    void ClearBirdIndexSave()
    {
        // birdIndexSave配列の要素をすべて0にする
        for (int i = 0; i < birdIndexSave.Length; i++)
        {
            birdIndexSave[i] = 0;
        }
    }

// 配列をシャッフルするメソッド
    void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            T temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

}