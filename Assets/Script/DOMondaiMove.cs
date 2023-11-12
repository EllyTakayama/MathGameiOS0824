using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween
using Sirenix.OdinInspector; //Odin
using UnityEngine.UI;

public class DOMondaiMove : MonoBehaviour
{
    private Sequence mondaiPanelIn;
    private Sequence mondaiPanelOut;
    private Tween resetMondaiPanel;//問題パネルを初期位置に移動させる
    //[SerializeField] DOPanelWave doPanelWave;//出題後パネルを上下に動かすDOTweenの取得
    // Start is called before the first frame update

    void Awake(){
        mondaiPanelIn = DOTween.Sequence()
            //.Append(transform.DOLocalMove(new Vector3(1500, 120, 0), 0))
            .Append(transform.DOLocalMove(new Vector3(0, 120, 0), 0.4f))
            .OnComplete(() => {
                // コールバック処理を記述する
                //doPanelWave.PlayWaveAnimation();
                Debug.Log("mondaiPanelOut のアニメーションを始めます");})
            //.SetEase(Ease.OutQuint)
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);

        mondaiPanelOut = DOTween.Sequence()
            .Append(transform.DOLocalMove(new Vector3(-1500, 120, 0), 0.3f))
            .OnComplete(() => {
                // コールバック処理を記述する
                //doPanelWave.StopWaveAnimation();
                Debug.Log("mondaiPanelOut のアニメーションが完了しました");})
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);

        resetMondaiPanel = transform.DOLocalMove(new Vector3(0, 1100, 0), 0.5f)
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);

    }

    [Button("mondaiPanelリセット")]　//←[Button("ラベル名")]
    public void CallmondaiPanelReset(){
        //doPanelWave.StopWaveAnimation();
        resetMondaiPanel.Restart();
    }
    

    [Button("mondaiPanelイン")]　//←[Button("ラベル名")]
    public void CallmondaiPanelIn(){
        mondaiPanelIn.Restart();
    }


    [Button("mondaiPanelアウト")]　//←[Button("ラベル名")]
    public void CallmondaiPanelOut(){
        mondaiPanelOut.Restart();
    }

}

