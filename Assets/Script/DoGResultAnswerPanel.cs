using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DoGResultAnswerPanel : MonoBehaviour
{//解答後のリザルトパネルに表示される
    [SerializeField] private GameObject answerBackImage;
    [SerializeField] private Text correctAnswer;
    [SerializeField] private Text wrongAnswer;
    [SerializeField] private GCheckButton _GcheckButton;//CheckButton.csの参照
    // Start is called before the first frame update

    public void SetAnsResultPanel()
    {
        DOTween.Sequence()
            .Append(answerBackImage.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutSine))
            //.AppendInterval(0.2f)
            .AppendCallback(() => 
            {
                print("correctAnswer");
                // correctAnswerのDOTextアニメーション
                correctAnswer.DOText(_GcheckButton.correctAnswerString, _GcheckButton.correctAnswerString.Length * 0.01f)
                    .OnComplete(() => 
                    {
                        // wrongAnswerのDOTextアニメーション
                        print("wrongAnswer");
                        wrongAnswer.DOText(_GcheckButton.wrongAnswerString, _GcheckButton.wrongAnswerString.Length * 0.01f);
                    });
            })
            .SetLink(gameObject);
    }

    public void ResetAnsResultPanel()
    {
        correctAnswer.text = "";
        wrongAnswer.text = "";
        // answerBackImageのスケールをゼロにする
        answerBackImage.transform.localScale = Vector3.zero;
    }
}