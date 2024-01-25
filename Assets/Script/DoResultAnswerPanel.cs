using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DoResultAnswerPanel : MonoBehaviour
{//解答後のリザルトパネルに表示される
    [SerializeField] private GameObject answerBackImage;
    [SerializeField] private Text correctAnswer;
    [SerializeField] private Text wrongAnswer;
    [SerializeField] private CheckButton _checkButton;//CheckButton.csの参照
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
                correctAnswer.DOText(_checkButton.correctAnswerString, _checkButton.correctAnswerString.Length * 0.01f)
                    .OnComplete(() => 
                    {
                        // wrongAnswerのDOTextアニメーション
                        print("wrongAnswer");
                        wrongAnswer.DOText(_checkButton.wrongAnswerString, _checkButton.wrongAnswerString.Length * 0.01f);
                    });
            })
            .SetLink(gameObject);
    }

    public void ResetAnsResultPanel()
    {
        correctAnswer.text = "";
        wrongAnswer.text = "";
    }
}
