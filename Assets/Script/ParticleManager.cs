using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
  public GameObject[] particlePrefabs; // パーティクルのプレハブをアサイン
    public int poolSizePerPrefab = 3; // 各プレハブごとのプールのサイズ
    private GameObject[][] particlePools; // パーティクルのプール
    /*
     * 0~3インデックスはConfetti
     * 
     * GameSceneでは
     * 4~6は正解時のButtonへのエフェクト
     * 7は不正解時のButtonへのエフェクト
     *
     * RenshuSceneでは
     * 4~9は正解時のエフェクト
     * 10は不正解のエフェクト
     */
    void Start()
    {
        // プールの初期化
        InitializePools();
    }

    // パーティクルのプールを初期化
    void InitializePools()
    {
        particlePools = new GameObject[particlePrefabs.Length][];

        for (int i = 0; i < particlePrefabs.Length; i++)
        {
            particlePools[i] = new GameObject[poolSizePerPrefab];

            for (int j = 0; j < poolSizePerPrefab; j++)
            {
                // プレハブからインスタンスを生成
                GameObject particle = Instantiate(particlePrefabs[i], transform.position, Quaternion.identity);
                particle.SetActive(false); // 非表示にする
                particlePools[i][j] = particle; // プールに追加
                Debug.Log($"パーティクル{j}のインスタンス作成");
            }
        }
    }

    // パーティクルを再生
    public void PlayParticle(int prefabIndex, Vector3 position)
    {
        if (prefabIndex >= 0 && prefabIndex < particlePrefabs.Length)
        {
            GameObject availableParticle = GetAvailableParticle(prefabIndex);

            if (availableParticle != null)
            {
                availableParticle.transform.position = position;
                availableParticle.SetActive(true); // 表示にする
                availableParticle.GetComponent<ParticleSystem>().Play(); // パーティクル再生
                StartCoroutine(DisableParticleAfterPlay(availableParticle));
                Debug.Log($"パーティクルの再生");
            }
            else
            {
                Debug.LogWarning("No available particles in the pool for prefab index: " + prefabIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid prefab index: " + prefabIndex);
        }
    }

    // 使用可能なパーティクルを取得
    GameObject GetAvailableParticle(int prefabIndex)
    {
        foreach (GameObject particle in particlePools[prefabIndex])
        {
            if (!particle.activeInHierarchy)
            {
                return particle;
            }
        }
        return null;
    }

    // パーティクル再生後に非表示にする
    IEnumerator DisableParticleAfterPlay(GameObject particle)
    {
        yield return new WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration);
        particle.SetActive(false); // 非表示にする
    }
}
