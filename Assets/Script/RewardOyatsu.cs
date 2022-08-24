using System.Collections;
using System.Collections.Generic;
using UnityEngine;//0909リワード広告後に呼び出すおやつメソッド

public class RewardOyatsu : MonoBehaviour
{
  public static RewardOyatsu instance;
　　　public int foodCount;//フード生成する回数をGamaManagerのscoreから取得したい
    public bool rewardFood;//rewardで呼び出すようのbool
    public GameObject[] foodPrefab;//配列にGameObjectを代入inspector上でプレハブを指定
    private int number;//配列のスクリプト
    public GameObject gameOverPanel;
    public GameObject foodPiyo;
    public int endCount;
    public int foodScore;
    public int RenshuuNumber;
     public int rewardNumber;

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
  
    // Start is called before the first frame update
    void Start()
    {
    foodCount = 0;
     endCount = 0;
     foodScore =0;
     rewardNumber = 5;//動画見たら5こおやつあげる予定
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RewardPiyo(){
       if(foodCount < rewardNumber){
       Debug.Log("Food"+RenshuuNumber);  
       foodScore = rewardNumber;
       //foodPrefabの配列の中からランダムにフードを生成
       number = Random.Range(0,foodPrefab.Length);
       GameObject foodPiyo = Instantiate(foodPrefab[number],//生成するプレハブ
            new Vector3 (0.0f, -200f, 0.0f),//生成時の位置xをランダムするVector3を指定
            transform.rotation);//生成時の向き
       foodPiyo.transform.SetParent(gameOverPanel.transform,false);   
          //FoodGeneratorと同じ位置にPrefabを生成する
             
      }
        foodCount++;
        endCount = foodScore-foodCount;
        //Debug.Log("foodCount"+foodCount);
        //Debug.Log("endCount"+endCount);
        }
}
