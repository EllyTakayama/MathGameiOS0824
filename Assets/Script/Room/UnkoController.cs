using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnkoController : MonoBehaviour
{
    public float unkoValidityTime;//unkoの有効期間
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DecreaseUnkoTime());
    }
    IEnumerator DecreaseUnkoTime()
    {
        while (unkoValidityTime > 0f)
        {
            yield return new WaitForSeconds(1f); // 1秒待つ
            unkoValidityTime -= 1f; // 1秒ずつ減らす
            //Debug.Log("Food Validity Time: " + unkoValidityTime);
        }

        // foodValidityTimeが0以下になったら、表示されているbirdObjectとunkoSliderを非表示にする
        if (unkoValidityTime <= 0f)
        {
            this.gameObject.SetActive(false);
            // コルーチンを終了する
            yield break;
        }
    }

}
