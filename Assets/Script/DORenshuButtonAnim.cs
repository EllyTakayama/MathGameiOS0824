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
    
    private Vector3 parentStartPosition;//親の初期値
    public RectTransform AnsButtonParent;
    public Transform piyoObject; // 移動の目標となるオブジェクト
    //スタート時にボタンのAddListernerを登録する
    void Start()
    {
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            int index = i; // クロージャのために必要
            AnsButtons[i].onClick.AddListener(() =>
            {
                AnimateOtherButtons(index);
                //CreateEffectAtButton(index);
                ResetButton();
            });
        }
        parentStartPosition = AnsButtonParent.localPosition;
        
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
                    .AppendInterval(1.0f)
                    .Append(AnsButtons[i].transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InSine))
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
            AnsButtons[i].transform.localRotation = Quaternion.identity;
        }
    }
    //Gameシーンで回転させながらスケールを1.0にするスクリプト
    public void ShowButtonsWithRotation()
    {
        for (int i = 0; i < AnsButtons.Length; i++)
        {
            DOTween.Sequence()
                .Append(AnsButtons[i].transform.DORotate(new Vector3(0f, 0f, 360f), 0.8f,RotateMode.FastBeyond360))
                .Join(AnsButtons[i].transform.DOScale(Vector3.one, 0.4f))
                .SetEase(Ease.OutSine)
                .SetLink(gameObject);
        }
    }

    //AnsButtonをスタート時の位置に移動させる
    public void GResetButton()
    {
        AnsButtonParent.localPosition = parentStartPosition;
        ShowButtonsWithRotation();
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
