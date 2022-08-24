using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//DoTweenでgradePanelをスライドいんさせるスクリプト0901
public class DOTweenPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] private GameObject gradePanel;
    [SerializeField] private GameObject gradePiyoSprite;
    [SerializeField] private Image gradeImage;
    [SerializeField] private Sprite grade1;
    [SerializeField] private Sprite grade2;
    [SerializeField] private Sprite grade3;
    [SerializeField] private Sprite grade4;
    [SerializeField] private Sprite grade5;
    [SerializeField] private Text gradeText;
  
    
    // Start is called before the first frame update
   
    void Start()
    {
     gradePanel.SetActive(false);
     gradeImage.enabled = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        if(gradePanel.activeSelf){
            return;
        }

    if(GameManager.singleton.currentMode>10)
        {
        if(GameManager.singleton.currentCount > TestToggle.testQuestion-1){
            gradePanel.SetActive(true);
            SoundManager.instance.PlaySE12GradePanel();
        Invoke("DoGradePanel",1.0f);
        //Debug.Log("QuesCount"+TestToggle.testQuestion);
        
        }
     
    }
    if(GameManager.singleton.currentMode<10){
        if(GameManager.singleton.currentCount >=9){
            gradePanel.SetActive(true);
            SoundManager.instance.PlaySE12GradePanel();
        Invoke("DoGradePanel",1.0f);
       }
    }
     
    }  

    public void DoGradePanel(){

    Debug.Log("DoGradePanel");
        gradePanel.SetActive(true);
       if(GameManager.singleton.currentMode>10){
            gradeText.text = "ちからだめし\nがんばったね";
            gradeImage.enabled = true;
            gradeImage.GetComponent<DoGradeImage>().DoImageChange();
            
        } else {
             gradeText.text = "れんしゅう\nがんばったね";
             gradeImage.enabled = true;
            gradeImage.GetComponent<DoGradeImage>().DoImageChange();
        }
        GradeImage();
        gradePanel.GetComponent<RectTransform>()   
        .DOAnchorPos(new Vector2(0,0), 1.5f)
    .SetEase(Ease.OutBack)
    ; 
   
    }
    void GradeImage(){
        //gradeImage.enabled = true;
        //gradeImage.GetComponent<DoGradeImage>().DoImageChange();
        if(GameManager.singleton.currentScore==9){
            //9点満点
            gradeImage.sprite = grade1;
            SoundManager.instance.PlaySE5End1();
        }
        if((GameManager.singleton.currentScore<9)&&(GameManager.singleton.currentScore>6)){
            //7-8点
            gradeImage.sprite = grade2;
            SoundManager.instance.PlaySE6End2();
        }
        if((GameManager.singleton.currentScore<7)&&(GameManager.singleton.currentScore>3)){
            //4-6点
            gradeImage.sprite = grade3;
            SoundManager.instance.PlaySE7End3();
        }
        if((GameManager.singleton.currentScore<4)&&(GameManager.singleton.currentScore>0)){
            //1-3点
            gradeImage.sprite = grade4;
            SoundManager.instance.PlaySE8End4();
        }
        if(GameManager.singleton.currentScore==0){
            //0点
            SoundManager.instance.PlaySE8End4();
            gradeImage.sprite = grade5;
        }
    }


}
