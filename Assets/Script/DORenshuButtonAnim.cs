using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DORenshuButtonAnim : MonoBehaviour
{
    public Button[] AnsButtons;
    public GameObject effectPrefab; // エフェクトのプレハブ
    [SerializeField] private int ansButtonIndex;
    
    void Start()
    {
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            int index = i; // クロージャのために必要
            AnsButtons[i].onClick.AddListener(() =>
            {
                AnimateOtherButtons(index);
                CreateEffectAtButton(index);
                ResetButton();
            });
        }
    }

    public void AnimateOtherButtons(int clickedButtonIndex)
    {
        Debug.Log($"AnimateOtherButton{clickedButtonIndex}");
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            if (i != clickedButtonIndex)
            {
                DOTween.Sequence()
                    .Append(AnsButtons[i].transform.DORotate(new Vector3(0f, 0f, 180f), 0.5f))
                    .Join(AnsButtons[i].transform.DOScale(Vector3.zero, 0.5f))
                    .SetDelay(0.2f);
            }
        }
    }

    public void CreateEffectAtButton(int targetButtonIndex)
    {
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, AnsButtons[targetButtonIndex].transform.position, Quaternion.identity);
            Destroy(effect, 2.0f);
        }
    }

    public void ResetButton()
    {
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].transform.localScale = Vector3.one;
            AnsButtons[i].transform.localRotation = Quaternion.identity;
        }
    }
}
