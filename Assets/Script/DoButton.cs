using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;//Button　DOScaleバージョン
using Sirenix.OdinInspector; // Odin Inspectorを追加

public class DoButton : MonoBehaviour
{
    [SerializeField] private Vector3 defaultScale;
    [SerializeField] private RectTransform buttonRectTransform;
    [SerializeField] private float animationDuration = 0.7f;
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float delayDuration = 0.7f; // 待機時間
    private Sequence _rewardButtonAnimationSequence; // シーケンスを保持する変数

    void Start()
    {
        defaultScale = transform.localScale;
        //StartRewardButtonAnimation();
    } 
   
    public void OnButtonClick()
    {
        transform.localScale = defaultScale;
        transform.DOPunchScale(Vector3.one * 1.2f, 0.3f, 1, 1.2f)
            .SetLink(gameObject);
        //Debug.Log("ボタン！");
    }

    [Button("OnButtonClick")]
    //1.4倍、0.8秒かけて、パンチは1回、弾力強め
    public void OnAnswerButtonClick()
    {
        transform.localScale = defaultScale;
        transform.DOPunchScale(Vector3.one * 0.4f, 0.8f, 1, 1.2f)
            .SetLink(gameObject);
        Debug.Log("giftボタン！");
    }
    //アタッチされたGameObjectをTweenで規定のアニメーションをさせる
    public void StartRewardButtonTween()
    {
        transform.localScale = Vector3.one;
        Debug.Log("DoButton");
      transform.DOScale(Vector3.one * scaleFactor, animationDuration)
                .SetLoops(-1, LoopType.Yoyo); // ループして元のサイズに戻る
     
    }
    //アタッチされたGameObjectを規定のSequenceでアニメーションをさせる
    public void StartRewardButtonAnimation()
    {
        transform.localScale = Vector3.one;
        _rewardButtonAnimationSequence = DOTween.Sequence();

        _rewardButtonAnimationSequence.Append(transform.DOScale(Vector3.one * scaleFactor, animationDuration));
        _rewardButtonAnimationSequence.AppendInterval(delayDuration);
        _rewardButtonAnimationSequence.Append(transform.DOScale(Vector3.one, animationDuration));
        _rewardButtonAnimationSequence.SetLink(gameObject);
    }
    //TweenをKillするスクリプト
    [Button("Sequence Kill")]
    public void StopRewardButtonAnimation()
    {
        if (_rewardButtonAnimationSequence != null)
        {
            // シーケンスを停止
            _rewardButtonAnimationSequence.Kill();
        }
        // Tweenアニメーションを停止
        //transform.DOKill();
    }
    [Button("Reset Vector3.zero")]
    public void ResetRewardButtonAnimation()
    {
        // Tweenアニメーションのリセット
        transform.localScale = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
    public void RewardGiftOn()
    {
        // Tweenアニメーションのリセット
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
    }
    
}
