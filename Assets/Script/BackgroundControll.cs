using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; //Odin

public class BackgroundControll : MonoBehaviour
{
    [SerializeField] private GameObject[] _ScrollBackgrounds;//スクロールする背景の親 

    [SerializeField] private BackgroundMove[] _backgroundMove;//スクロールさせるスクリプトの参照

    //スクロールさせる背景をアクティブ（表示させる）
    public void ActiveBackground()
    {
        for (int i = 0; i < _ScrollBackgrounds.Length; i++)
        {
            _ScrollBackgrounds[i].SetActive(true);
        }
    }
    public void NotActiveBackground()
    {
        for (int i = 0; i < _ScrollBackgrounds.Length; i++)
        {
            _ScrollBackgrounds[i].SetActive(false);
        }
    }
    
    //スクロールさせるスクリプトのアクティブにする
    public void SetBackGroundMove()
    {
        for (int i = 0; i < _backgroundMove.Length; i++)
        {
            _backgroundMove[i].enabled= true;
            Debug.Log($"SetGround_{i}");
        }
    }
    
    //スクロールさせるスクリプトのアクティブにする
    public void StopBackGroundMove()
    {
        for (int i = 0; i < _backgroundMove.Length; i++)
        {
            _backgroundMove[i].enabled= false;
        }
    }
    
}
