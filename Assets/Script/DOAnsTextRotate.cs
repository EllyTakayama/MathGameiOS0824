using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Shapes2D; //DOTween
using Sirenix.OdinInspector; //Odin

public class DOAnsTextRotate : MonoBehaviour
{
    [SerializeField] private GameObject quesAnswer;
    [SerializeField] private GameObject questionImage;
    Vector3 initialPosition;  // 追加: 初期位置を保存する変数
    private Vector3 initialRotation;
    private Vector3 finalRotation;
    private Sequence resetRotation;
    void Start()
    {
        initialPosition = questionImage.transform.localPosition;
        initialRotation = new Vector3(0, 0, 0); // 初期のRotationを設定
        finalRotation = new Vector3(0, 90, 0);  // 終了時のRotationを設定
    }

    public void DeleyRotetePanel()
    {
        Invoke("RotatePanel",0.2f);
    }
    [Button("AnswerRotete実行")]　//←[Button("ラベル名")]
    public void RotatePanel()
    {
        // 初期位置に戻す
        questionImage.transform.localPosition = initialPosition;
      
        // アニメーションの設定
        questionImage.transform.rotation = Quaternion.Euler(initialRotation);
        questionImage.transform.DORotate(finalRotation, 0.8f)
            .SetLink(gameObject)
            .OnComplete(() => SetAnswerText());
    }

    //答えを表示させるメソッド
    [Button("SetAnwerText実行")]　//←[Button("ラベル名")]
    void SetAnswerText()
    {
        quesAnswer.SetActive(true);
    }

    public void ResetAnswerText()
    {
        questionImage.transform.rotation = Quaternion.Euler(initialRotation);
        quesAnswer.SetActive(false);
    }

    IEnumerator DelayedMovePanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        MovePanel();
    }

    [Button("StartPanelOut実行")]　//←[Button("ラベル名")]
    void MovePanel()
    {

        // アニメーションの設定
        transform.DOLocalMoveX(-1500f, 0.4f).SetLink(gameObject).OnComplete(() => RotatePanelComplete(initialRotation));
    }
    private void RotatePanelComplete(Vector3 finalRotation)
    {
        MathAndAnswer.instance.MathsProblem();
        // アニメーション完了後の処理
        transform.DORotate(finalRotation,0);
        
    }
}
