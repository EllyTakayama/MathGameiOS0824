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

    //スタート時にボタンのAddListernerを登録する
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
    //押されたButtonのIndexを取得しそれ以外のButtonのサイズを0にして見えなくする
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
                    .SetDelay(0.2f)
                    .SetLink(gameObject);
            }
            else//clickButtonIndexがtrueの時　おさればボタンのアニメーション
            {
                DOTween.Sequence()
                    .Append(AnsButtons[i].transform.DOScale(Vector3.one * 1.2f, 0.4f).SetEase(Ease.OutSine))
                    .AppendInterval(1.2f)
                    //.Append(AnsButtons[i].transform.DOScale(Vector3.one, 0).SetEase(Ease.InSine))
                    .SetLink(gameObject);
            }
        }
    }

    //押したButtonにエフェクトを作成する
    public void CreateEffectAtButton(int targetButtonIndex)
    {
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, AnsButtons[targetButtonIndex].transform.position, Quaternion.identity);
            Destroy(effect, 2.0f);
        }
    }
    
    //出題変更時にButtonを非表示させる
    public void InvisibleButton()
    {
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].transform.localScale = Vector3.zero;
            //AnsButtons[i].transform.localRotation = Quaternion.identity;
        }
    }

    //出題時にButtonを再表示させる
    public void ResetButton()
    {
        StartCoroutine(InitButton());
    }
    IEnumerator InitButton()
    {
        
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            AnsButtons[i].transform.localScale = Vector3.one;
            AnsButtons[i].transform.localRotation = Quaternion.identity;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
