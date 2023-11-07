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
            //Toggleが選択された時呼ばれるメソッド
            AnimateToggle(1.2f);//引数で大きくした倍率を入力
        }
        /*
        else
        {
            Toggleが選択されなくなった時の操作
        }*/
    }

    public void AnimateToggle(float scale)
    {
        //0.2秒かけてscaleを1.2倍にし、それが完了したら0.1秒かけてデフォルトの大きさに戻す
        transform.DOScale(scale, 0.2f)
            .SetEase(Ease.InOutQuart)//イージング
            //.SetEase(Ease.OutBounce)//イージング
            .SetLink(gameObject).OnComplete(() => transform.DOScale(defaultScale, 0.1f));
    }
    
}

