using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;//BGMスライダー
    [SerializeField] private Slider seSlider;//SEスライダー

    public void BgmVolume(){
        float a = bgmSlider.value*0.8f;
        SoundManager.instance.SetBgmVolume(a);
        print(a);
    }

    public void SeVolume(){
        float b = seSlider.value;
        SoundManager.instance.SetSeVolume(b);
        print(b);
    }
    
}
