using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public interface IToggleAnimation
{
    void AnimateToggle(float scale);
}

public class DOToggleAnimation : MonoBehaviour, IToggleAnimation
{
    private Toggle toggle;
    private Vector3 defaultScale;
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        defaultScale = transform.localScale;
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            AnimateToggle(1.2f);
        }
        /*
        else
        {
            AnimateToggle(1f);
        }*/
    }

    public void AnimateToggle(float scale)
    {
        transform.DOScale(scale, 0.2f)
            .SetLink(gameObject).OnComplete(() => transform.DOScale(defaultScale, 0.1f));
    }
    
}

