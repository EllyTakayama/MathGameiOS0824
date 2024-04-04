using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//GameOver画面でピヨにあげるフードを生成するスクリプト0825

public class FoodGenerator : MonoBehaviour
{
    public static FoodGenerator instance;
    public int foodCount; //フード生成する回数をGamaManagerのscoreから取得したい
    public bool isOneTimeFood; //DoTweenPanel.csで呼び出すようのbool
    public GameObject[] foodPrefab; //配列にGameObjectを代入inspector上でプレハブを指定
    private int number; //配列のスクリプト
    public GameObject gameOverPanel;
    public GameObject foodPiyo;
    public int endCount;
    public int foodScore;
    public int RenshuuNumber;
    public int rewardNumber;

    public GameObject AdMobManager;

    //public GameObject fruitEffect;//フルーツ用エフェクト
    //問題の解答後、GameOverPanelにフードを出現させピヨにあげたい
    void Awake()
    {
        MakeInstance();

    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        foodCount = 0;
        endCount = 0;
        foodScore = 0;
        //fruitEffect.SetActive(false);

    }

    public void Spawn()
    {
        rewardNumber = 5;
        if (foodCount < rewardNumber)
        {
            //gameOverPanel.GetComponent<GameOverPanel>().SetFruitEffect();
            Debug.Log("Food" + rewardNumber);
            foodScore = rewardNumber;
            //foodPrefabの配列の中からランダムにフードを生成
            number = Random.Range(0, foodPrefab.Length);

            //fruitEffect.SetActive(true);
            GameObject foodPiyo = Instantiate(foodPrefab[number], //生成するプレハブ
                new Vector3(0.0f, -200f, 0.0f), //生成時の位置xをランダムするVector3を指定
                transform.rotation); //生成時の向き
            foodPiyo.transform.SetParent(gameOverPanel.transform, false);
            //FoodGeneratorと同じ位置にPrefabを生成する


            foodCount++;
            endCount = foodScore - foodCount;
            Debug.Log("foodCount" + foodCount);
            Debug.Log("endCount" + endCount);
        }
        else if (GameManager.singleton.currentMode < 10)
        {
            RenshuuNumber = 3;
            if (foodCount < RenshuuNumber)
            {
                //gameOverPanel.GetComponent<GameOverPanel>().SetFruitEffect();
                Debug.Log("Food" + RenshuuNumber);
                foodScore = RenshuuNumber;
                //foodPrefabの配列の中からランダムにフードを生成
                number = Random.Range(0, foodPrefab.Length);
                //gameOverPanel.GetComponent<GameOverPanel>().SetFruitEffect();
                //fruitEffect.SetActive(true);
                GameObject foodPiyo = Instantiate(foodPrefab[number], //生成するプレハブ
                    new Vector3(0.0f, -200f, 0.0f), //生成時の位置xをランダムするVector3を指定
                    transform.rotation); //生成時の向き
                foodPiyo.transform.SetParent(gameOverPanel.transform, false);
                //FoodGeneratorと同じ位置にPrefabを生成する

            }

            foodCount++;
            endCount = foodScore - foodCount;
            //Debug.Log("foodCount"+foodCount);
            //Debug.Log("endCount"+endCount);
        }
        else if (GameManager.singleton.currentMode > 10)
        {
            if (foodCount < GameManager.singleton.currentScore)
            {
                //gameOverPanel.GetComponent<GameOverPanel>().SetFruitEffect();
                Debug.Log("Food" + GameManager.singleton.currentScore);
                foodScore = GameManager.singleton.currentScore;
                //foodPrefabの配列の中からランダムにフードを生成
                number = Random.Range(0, foodPrefab.Length);
                GameObject foodPiyo = Instantiate(foodPrefab[number], //生成するプレハブ
                    new Vector3(0.0f, -200f, 0.0f), //生成時の位置xをランダムするVector3を指定
                    transform.rotation); //生成時の向き
                foodPiyo.transform.SetParent(gameOverPanel.transform, false);
                //FoodGeneratorと同じ位置にPrefabを生成する

            }

            foodCount++;
            endCount = foodScore - foodCount;
            //Debug.Log("foodCount"+foodCount);
            //Debug.Log("endCount"+endCount);
        }
    }
}






