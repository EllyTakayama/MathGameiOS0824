using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    public Transform BallonCanvas;
    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(BallonCanvas, false);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }
}
