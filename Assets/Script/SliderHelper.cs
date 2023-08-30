using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHelper : MonoBehaviour,IPointerUpHandler
{
    //コピペUniRx
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject TestSliderManager;
    /*void Start()
    {
        print("name_"+name);
        print("tag_"+tag);
        
    }*/
    public void OnPointerUp(PointerEventData eventData)
    {
        print("event");
  
       if(gameObject.name == "bgmSlider"){
           TestSliderManager.GetComponent<SoundSlider>().BgmSave();
        }else{
           TestSliderManager.GetComponent<SoundSlider>().SeSave();
        }
    }

    /*
 
    protected Subject<float> changeValueSubject = new Subject<float>();
 
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    
 
    public IObservable<float> OnChangeValue
    {
        get { return changeValueSubject; }
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        changeValueSubject.OnNext(slider.value);
    }*/
}
