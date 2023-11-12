using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween
using Sirenix.OdinInspector; //Odin

public class DOAnswerMove : MonoBehaviour
{
    private Sequence _answerPanelIn;
    private Sequence _answerPanelOut;
    private Tween _resetAnswerPanel;//問題パネルを初期位置に移動させる
    //[SerializeField] DOPanelWave doPanelWave;//出題後パネルを上下に動かすDOTweenの取得
    // Start is called before the first frame update

    void Awake()
    {
        _answerPanelIn = DOTween.Sequence()
            //.Append(transform.DOLocalMove(new Vector3(1500, 120, 0), 0))
            .Append(transform.DOLocalMove(new Vector3(0, 120, 0), 0.4f))
            .OnComplete(() =>
            {
                // コールバック処理を記述する
                //doPanelWave.PlayWaveAnimation();
                Debug.Log("mondaiPanelOut のアニメーションを始めます");
            })
            //.SetEase(Ease.OutQuint)
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);

        _answerPanelOut = DOTween.Sequence()
            .Append(transform.DOLocalMove(new Vector3(-1500, 120, 0), 0.3f))
            .OnComplete(() =>
            {
                // コールバック処理を記述する
                //doPanelWave.StopWaveAnimation();
                Debug.Log("mondaiPanelOut のアニメーションが完了しました");
            })
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);
            
            _resetAnswerPanel = transform.DOLocalMove(new Vector3(0, 1100, 0), 0.5f)
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);

    }

    [Button("answerPanelリセット")]　//←[Button("ラベル名")]
    public void CallAnswerPanelReset(){
        //doPanelWave.StopWaveAnimation();
        _resetAnswerPanel.Restart();
    }
    

    [Button("answerPanelイン")]　//←[Button("ラベル名")]
    public void CallAnswerPanelIn(){
        _answerPanelIn.Restart();
    }


    [Button("answerPanelアウト")]　//←[Button("ラベル名")]
    public void CallAnswerPanelOut(){
        _answerPanelOut.Restart();
    }
}
