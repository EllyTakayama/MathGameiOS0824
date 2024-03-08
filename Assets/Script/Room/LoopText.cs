using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopText : MonoBehaviour
{
    public Text textField;
    private string originalText = "フードをセットことりをよぼうフードアイテムガチャでゲット";
    private int displayCharacters = 7; // 一度に表示する文字数
    private float displayInterval = 1.8f; // 文字を更新する間隔

    private void Start()
    {
        // テキストをループ表示するCoroutineを開始
        StartCoroutine(LoopTextCoroutine());
    }

    private IEnumerator LoopTextCoroutine()
    {
        int currentIndex = 0;

        while (true)
        {
            // テキストを更新
            textField.text = originalText.Substring(currentIndex, Mathf.Min(displayCharacters, originalText.Length - currentIndex));

            // インデックスを進める
            currentIndex += displayCharacters;

            // テキストが末尾に到達したら、コルーチンを終了
            if (currentIndex >= originalText.Length)
            {
                textField.gameObject.SetActive(false); // textFieldを非表示にする
                yield break;
            }
            // 指定した時間待機
            yield return new WaitForSeconds(displayInterval);
        }

    }
}
