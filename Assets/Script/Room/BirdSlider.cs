using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdSlider : MonoBehaviour
{ 
    public Slider slider;//このGameObjectの子ようそ
    public float birdTime;//秒:とりによってうんこが回復する時間が違う
    private float currentTime; // 増加中の現在の時間
    private Coroutine increaseCoroutine; // Sliderの値を増加させるコルーチン
    [SerializeField] private SpawnUnko _spawnUnko;//SpawnUnko.cs参照
    [SerializeField] private int index;//何番目のインデックスか


    void Start()
    {
        this.gameObject.GetComponent<BirdSlider>();

    }
    // SetSpawnUnkoReferenceメソッドを追加
    public void SetSpawnUnkoReference(SpawnUnko spawnUnko)
    {
        _spawnUnko = spawnUnko;
        Debug.Log("spawnUnko");
    }
    public void SetSliderReference()
    {
        slider = GetComponentInChildren<Slider>();
        Debug.Log("slider");
    }
    public void OnBirdSpawnUnko()
    {
        // Sliderを取得する
        slider = GetComponentInChildren<Slider>();;
        
        // Sliderがnullでないことを確認する
        if (slider == null)
        {
            Debug.LogError("Slider component not found.");
        }
        // Sliderの値が1未満の場合は処理を終了
        if (slider.value < 1f)
        {
            return;
        }
        _spawnUnko.SpawnUnkoByFoodType(transform.position);
        slider.value = 0;
        FullSlider();
    }
    // Unkoを生成するメソッド
    
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
