using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public float birdTime;//秒:とりによってうんこが回復する時間が違う
    private float currentTime; // 増加中の現在の時間
    private Coroutine increaseCoroutine; // Sliderの値を増加させるコルーチン
    
    public void FullSlider()
    {
        if (increaseCoroutine != null)
        {
            StopCoroutine(increaseCoroutine); // 既存のコルーチンを停止
        } 
        increaseCoroutine = StartCoroutine(IncreaseSliderValue()); // 新しいコルーチンを開始
    }
    // Sliderの値を減少させるコルーチン
    IEnumerator IncreaseSliderValue()
    {
        currentTime = 0f;
        while (currentTime < birdTime)
        {
            currentTime += 1f; // 時間を増やす
            slider.value = currentTime / birdTime; // Sliderの値を更新
            yield return new WaitForSeconds(1f);//1秒待機
        }
    }
    
}
