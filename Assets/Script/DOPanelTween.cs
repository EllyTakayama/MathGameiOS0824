using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOPanelTween : MonoBehaviour
{
    [SerializeField] private GameObject modeSettingPanel;
    //[SerializeField] private Vector3 initialScale;//初期値の取得

    void Start()
    {
        modeSettingPanel.transform.DOScale(Vector3.zero, 0f)
            .OnComplete(() => modeSettingPanel.SetActive(false))
            .SetLink(gameObject);
    }
    //open時のTween
    public void OpenSettingPanel()
    {
        modeSettingPanel.SetActive(true);
        modeSettingPanel.transform.DOScale(Vector3.one, 0.3f)
            .SetLink(gameObject);
        //Debug.Log($"open,{modeSettingPanel.transform}");
    }
    
    //close時のTween
    public void CloseSettingPanel()
    {
        modeSettingPanel.transform.DOScale(Vector3.zero, 0.2f)
            .OnComplete(() => modeSettingPanel.SetActive(false))
            .SetLink(gameObject);
        //Debug.Log($"close,{modeSettingPanel.transform}");
    }
}
