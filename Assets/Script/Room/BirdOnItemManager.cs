using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdOnItemManager : MonoBehaviour
{
public GameObject[] perches; // 止まり木オブジェクトの配列
//public GameObject[] birdObjects; // ピヨちゃんのGameObjectの配列
public GameObject[] birdPrefabs;//バードプレファブ
//public Slider[] sliderPrefabs;//sliderのプレファブ
private List<int> birdIndexList = new List<int>(); // 生成したbirdObjectPrefabのインデックスを格納するリスト
[SerializeField] private GameObject _BirdOnItemPoint;//選択する親のアイテム
[SerializeField] private int numBirds;//鳥の出現できる数
[SerializeField] private int percheNum;//選択する止まり木のインデックス
// 各perchesに対応した表示位置を表す整数のリスト
private int[] perchePositions = new int[] {1,4,4,5,4,3,5,1,4};//止まり木に応じたとまれる鳥の数
public Slider[] unkoSliders; // ピヨちゃんに対応するSliderの配列
private int birdIndex;//鳥をランダムに選ぶint
public int perchIndex;//perchのIndex
private int[] birdArray = new int[] {0, 120, -120, -240, 240};
public SpawnUnko spawnUnko; // SpawnUnkoスクリプトへの参照
[SerializeField] private float foodValidityTime; // 食べ物の有効時間
private int[] birdIndexShuffle = new int[] {0,1,2,3,4,5,6,7,8,9,10};//表示されたbirdIdexのシャッフルのため
private int[] birdIndexSave = new int[] {0,0,0,0,0,0,0,0,0,0,0};//表示されたbirdIdexの保存のため
[SerializeField] private Image foodImage;//foodイメージ
[SerializeField] private Sprite[] foodSprite;//foodSpriteの差し替え
[SerializeField] private DOFade _doFade;//フェード用メソッド
[SerializeField] private RectTransform parentUI;
[SerializeField] private List<GameObject> birdInstance= new List<GameObject>(); // 生成したbirdPrefabのインスタンスを格納するリスト
[SerializeField] private GameObject fingerImage;//餌やりを促すゆび
[SerializeField] private GameObject foodAnnounceText;//foodのセットを促すテキスト
[SerializeField] private GameObject[] perchChoiceFrame;//選択されたPerchを示すImage
[SerializeField] private ParticleSystem[] foodParticle; //foodImageのオンオフのパーティクル
private ParticleSystem instantiatedFoodParticle; //foodParticleのインスタンス用パーティクルシステム
[SerializeField] private GameObject GachaPanel0;//foodParticleの親
[SerializeField] private Sprite[] unkoImages;
void Start()
{
    LoadPerchIndex();
    //NumBirdsLoad();
    //numBirds = 3;
    ShowRandomPerch();//ロードされたIndexの止まり木を表示する
    SetPerchFrame();//ロードされたIndezの止まり木がわかるようにする
    //SetBirdsOnPerches(); // セーブデータに基づいてピヨちゃんを止まり木に配置する
   // foodParticle[0]をfoodImageの位置にInstantiate
    if (foodParticle.Length > 0 && foodImage != null)
    {
        /*
        instantiatedFoodParticle = Instantiate(foodParticle[0], foodImage.transform.localPosition, Quaternion.identity);
        */
        // foodImageのRectTransformから位置情報を取得
        Vector3 foodImagePosition = foodImage.transform.position;

        // foodImageの親オブジェクトがCanvasなどの場合は、worldPositionStaysをtrueにして、ローカル座標をワールド座標に変換する
        instantiatedFoodParticle = Instantiate(foodParticle[0], foodImage.transform.position, Quaternion.identity);
        instantiatedFoodParticle.transform.SetParent(GachaPanel0.transform);
        Debug.Log("foodParticle_Instantiate");
    }
}

public void SetPerchFrame()
{
    for (int i = 0; i < perchChoiceFrame.Length; i++)
    {
        perchChoiceFrame[i].SetActive(false);
    }
    perchChoiceFrame[perchIndex].SetActive(true);
}

public void SetFood()
{
    StartCoroutine(SetFoodBird());
}
//foodをセットするコルーチン
IEnumerator SetFoodBird()
{
    SoundManager.instance.PlaySE3();
    // インスタンス化されたfoodParticleが存在し、かつ再生中であれば停止
    if (instantiatedFoodParticle != null && instantiatedFoodParticle.isPlaying)
    {
        instantiatedFoodParticle.Stop();
        Debug.Log("foodParticle_Stop");
    }
    // 食べ物のイメージを設定
    foodImage.sprite = foodSprite[0];
    //ゆびでのえさやりアピールもオフ
    fingerImage.SetActive(false);
    //えさやりを促すテキストもオフ
    foodAnnounceText.SetActive(false);
    // 1秒待ってからSetFood()を呼び出す
    yield return new WaitForSeconds(0.2f);
    _doFade.FadeOutToIn();
 
    if (birdInstance != null)
    {
        foreach (GameObject bird in birdInstance)
        {
            Destroy(bird);
        }
        birdInstance.Clear();
    }
    // foodParticle[1]をfoodImageの位置にInstantiate
    if (foodParticle.Length > 1 && foodImage != null)
    {
        instantiatedFoodParticle =Instantiate(foodParticle[1], foodImage.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        instantiatedFoodParticle.transform.SetParent(GachaPanel0.transform);
        Debug.Log("foodParticle_Instantiate");
    }
    // delayBetweenPerches秒待ってからSetBirdsOnPerches()を呼び出す
    yield return new WaitForSeconds(2.6f);
    SetBirdsOnPerches();
    yield break;
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
            //Debug.Log("Food Validity Time: " + foodValidityTime);
        }

        // foodValidityTimeが0以下になったら、表示されているbirdObjectとunkoSliderを非表示にする
            if (foodValidityTime <= 0f)
            {
                // インスタンス化されたfoodParticleが存在し、かつ再生中であれば停止
                if (instantiatedFoodParticle != null && instantiatedFoodParticle.isPlaying)
                {
                    instantiatedFoodParticle.Stop();
                    Debug.Log("foodParticle_Stop");
                }
                if (birdInstance != null)
                {
                    foreach (GameObject birdInstance in birdInstance)
                    {
                        Destroy(birdInstance);
                    }
                }

                birdInstance.Clear(); // birdInstanceリストをクリアする
                birdInstance = null;
                foodImage.sprite = foodSprite[1];
                fingerImage.SetActive(true);
                fingerImage.GetComponent<FingerMove>().FingerRotate();//ゆびを表示させてからアニメーションを実施
                // コルーチンを終了する
                yield break;
            }
    }
    //元々はランダムにしようかと思ったけどやっぱり選択しきがいいと思ったので変更。Panelで設定変更できる。
    public void ShowRandomPerch()
    {
        // 切り替えのため全ての止まり木オブジェクトを非アクティブにする
        foreach (GameObject perch in perches)
        {
            perch.SetActive(false);
        }
        // 選択された止まり木オブジェクト
        GameObject selectedPerch = perches[perchIndex];
        numBirds = perchePositions[perchIndex];
        selectedPerch.SetActive(true); // 選択された止まり木オブジェクトをアクティブにする
        SetPerchFrame();//選択された止まり木がわかるようFrame表示を更新する
        SavePerchIndex();
    }
    // perchIndexをセーブするメソッド
    public void SavePerchIndex()
    {
        // ファイル名を指定してperchIndexをセーブ
        ES3.Save<int>("perchIndex", perchIndex, "perchIndexSave.es3");
    }

    // perchIndexをロードするメソッド
    public void LoadPerchIndex()
    {
        // ファイル名を指定してperchIndexをロードし、ロードに失敗した場合はデフォルト値として0を設定
        perchIndex = ES3.Load<int>("perchIndex", "perchIndexSave.es3", 0);
        Debug.Log($"perchIndex_{perchIndex}");
    }

    void SetBirdsOnPerches()
    {
        StartCoroutine((SetBirdOnPerchesCoroutine()));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator SetBirdOnPerchesCoroutine()
    {
        int NumBirds = Random.Range(1, numBirds + 1);
        int[] tempArray = new int[numBirds];
        for (int j = 0; j < numBirds; j++)
        {
            tempArray[j] = birdArray[j];
        }
        //birdObjectを配置する位置をシャッフルする
        ShuffleArray(tempArray);

        for (int i = 0; i < NumBirds; i++)
        {
            SoundManager.instance.PlaySE7End3();//ピヨ生成の効果音
            // ピヨちゃんのオブジェクトをランダムに選択
            birdIndex = Random.Range(0, birdPrefabs.Length);
            birdIndexList.Add(birdIndex);//birdIndexListにbirdPrefabのIndexを追加する
            Debug.Log($"bridIndexList{i}_{birdIndexList[i]}");
            GameObject birdObject = Instantiate(birdPrefabs[birdIndex],_BirdOnItemPoint.transform); // ランダムなbirdObjectPrefabをInstantiate
            birdInstance.Add(birdObject); // 生成したbirdPrefabをリストbirdInstanceに追加
            // BirdSlider.csへの参照を渡す
            BirdSlider birdSlider = birdObject.GetComponent<BirdSlider>();
            //birdSlider.GetComponent<BirdSlider>().SetSliderReference();
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
            birdObject.transform.localPosition = new Vector3(xPos, 0f, 0f); // ピヨちゃんのオブジェクトの位置を設定
            birdSlider.GetComponent<BirdSlider>().SetSpawnUnkoReference(spawnUnko);
  
            // birdIndexSave配列をセーブする
            //ES3.Save<int[]>("birdIndexSave",  birdIndexSave,"birdIndexSave.es3");
            yield return new WaitForSeconds(0.4f); // 0.4秒待つ
        }
        // foodValidityTimeの減少を開始するコルーチンを開始
        StartCoroutine(DecreaseValidityTime());
        yield break;
    }
    
    public void SetSpawnUnkoReference(SpawnUnko spawnUnko)
    {
        this.spawnUnko = spawnUnko;
    }
    // birdPrefabをInstantiateする際に、SpawnUnko.csの参照を受け取るためのメソッド

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
