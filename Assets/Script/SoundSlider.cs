using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using UnityEngine.EventSystems;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;//BGMスライダー
    [SerializeField] private Slider seSlider;//SEスライダー
    [SerializeField] private SliderHelper sliderHelper;//スライダーイベント取得用
   

    void Start()
    {
        BgmLoadSlider();
        SeLoadSlider();
        
   }
    public void BgmVolume(){
        float a = bgmSlider.value*0.8f;
        SoundManager.instance.SetBgmVolume(a);
        //BgmSave();
        //print(a);
    }

    public void SeVolume(){
        float b = seSlider.value;
        SoundManager.instance.SetSeVolume(b);
        SeSave();
        print(b);
    }

    public void BgmSave(){
        ES3.Save<float>("bgmSliderValue", bgmSlider.value,"bgmSlider.es3");
        //print("bgmSliderValue");
    }
    public void SeSave(){
        ES3.Save<float>("seSliderValue", seSlider.value,"seSlider.es3");
    }
    public void BgmLoadSlider(){
      bgmSlider.value = ES3.Load<float>("bgmSliderValue","bgmSlider.es3",0.6f);
      float a = bgmSlider.value*0.8f;
      SoundManager.instance.SetBgmVolume(a);
      //print(a);
    }
    public void SeLoadSlider(){
      seSlider.value = ES3.Load<float>("seSliderValue","seSlider.es3",0.6f);
      float b = seSlider.value;
      SoundManager.instance.SetSeVolume(b);
      //print(b);
    }
    
}
