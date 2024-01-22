using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SafeAreaAdjuster : MonoBehaviour
{
    //セーフエリアに合わせたい箇所をtrueにする。
    [SerializeField] bool left;
    [SerializeField] bool right;
    [SerializeField] bool top;
    [SerializeField] bool bottom;

    private IEnumerator Start() {
        
        yield return null; // 1フレーム待機
        yield return new WaitForSeconds(0.2f);
        AdjustSafeArea();
    }

    public void LateUpdate()
    {
        //AdjustSafeArea();
    }

    private void AdjustSafeArea()
    {
        // ここに位置調整のコードを追加
        
        var panel = GetComponent<RectTransform>();
        var area = Screen.safeArea;

        var anchorMin = area.position;
        var anchorMax = area.position + area.size;

        if(left) anchorMin.x /= Screen.width;
        else anchorMin.x = 0;

        if(right) anchorMax.x /= Screen.width;
        else anchorMax.x = 1;

        if(bottom) anchorMin.y /= Screen.height;
        else anchorMin.y = 0;

        if(top) anchorMax.y /= Screen.height;
        else anchorMax.y = 1;

        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
    }
}