using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//gradeImageを拡大縮小するDoTween設定0614

public class DoGradeImage : MonoBehaviour
{
    public Image gradeImage;
    public GameObject gameOverPanel;

    public void DoImageChange(){
         transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.6f)
        .SetRelative()
        .SetLink(gameObject)
        ;
    }
}
