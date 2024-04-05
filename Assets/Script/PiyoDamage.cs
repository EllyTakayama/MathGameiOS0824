using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector; // Odin Inspectorを追加
public class PiyoDamage : MonoBehaviour
{
    [SerializeField] private GameObject piyoObject;
    private Color originalColor; // 元の色を保存する変数
    
    void Start()
    {
        originalColor = piyoObject.GetComponent<Renderer>().material.color; // 元の色を保存
    }
    [Button("PiyoDamage")]
    public void DamageCall(){
        StartCoroutine(PiyoD());
    }

    public IEnumerator PiyoD(){
        //時間、強さ、回数、手ぶれ値、スナップフラグ、dフェードアウト
        transform.DOShakePosition(1.0f, 30f, 10, 1, false, true)
            .SetLink(gameObject);

        // 無敵時間中の点滅
        for (int i = 0; i < 6; i++)
        {
            SetPiyoObjectColor(0f); // 透明にする
            yield return new WaitForSeconds(0.06f);
            SetPiyoObjectColor(1f); // 元の色に戻す
            yield return new WaitForSeconds(0.06f);
        }
        Debug.Log("piyoDamage");
    }
    // GameObjectの色を設定するメソッド
    private void SetPiyoObjectColor(float alpha)
    {
        Color color = piyoObject.GetComponent<Renderer>().material.color;
        color.a = alpha;
        piyoObject.GetComponent<Renderer>().material.color = color;
    }
    
}
