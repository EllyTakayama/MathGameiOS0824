using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoCoinAnim : MonoBehaviour//coinプレファブを
{
    public GameObject coinGenerator;//コインプレハブの生成位置
    public GameObject coinPrefab;//コインのプレハブ
    public Transform coinParent;    // コインの親オブジェクト
    public GameObject gameOverPanel;//coinの親
    [SerializeField] private Transform coinFrame;//移動させるTransformを取得させるため
    [SerializeField] private Button ansButton;

    void Start()
    {//Debug用
        //SpawnRewardCoin();
    }
    public void SpawnCoinOnButton(Button button)
    {
        StartCoroutine(CoinSpawnOnButton(button));
    }
    IEnumerator CoinSpawnOnButton(Button button)
    {
        ansButton = button;
        for(int i =0; i<10;i++){
            // ボタンの位置に対してランダムな位置にコインを生成
            Vector3 randomOffset = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f);
            Vector3 spawnPosition = button.transform.position + randomOffset;
            GameObject coin = Instantiate(coinPrefab,spawnPosition, Quaternion.identity, coinParent);
        
            // coinPrefabにアタッチされたDoCoinMoveコンポーネントを取得
            DoCoinMove coinMoveComponent = coin.GetComponent<DoCoinMove>();
        
            if (coinMoveComponent != null)
            {
                coinMoveComponent.coinFrame = coinFrame;
            }
            else
            {
                Debug.LogWarning("Coin prefab does not have DoCoinMove component attached.");
            }
            yield return new WaitForSeconds(0.02f);
            //Debug.Log("coinPrefab");
        }
    }
    
    public void SpawnRewardCoin()
    {
        StartCoroutine(RewardCoinSpawn());
    }

    IEnumerator RewardCoinSpawn(){
        for(int i =0; i<10;i++){
            // coinPrefabをcoinFrameの前後左右-200、200の範囲で生成
            Vector3 spawnPosition = gameOverPanel.transform.position + new Vector3(Random.Range(-200f, 200f), Random.Range(-200f, 200f), 0f);
            coinGenerator = Instantiate(coinPrefab,spawnPosition, transform.rotation);
            coinGenerator.transform.SetParent(gameOverPanel.transform,true);  
            // coinPrefabにアタッチされたDoCoinMoveコンポーネントを取得
            DoCoinMove coinMoveComponent = coinGenerator.GetComponent<DoCoinMove>();
            if (coinMoveComponent != null)
            {
                // coinFrame変数に値を代入
                coinMoveComponent.coinFrame = coinFrame;
            }
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("coinPrefab");
        }
    }
}
