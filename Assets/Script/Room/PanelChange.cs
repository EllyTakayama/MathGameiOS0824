using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//6月16日更新

public class PanelChange : MonoBehaviour
{
   enum Panel
    {
        gachaPanel,
        Panel1chara,
        Panel2chara,
        Panel3item,
        Panel4item,
    }
    // 現在表示しているパネル
    Panel currentPanel;

    public GameObject RightButton;
    public GameObject LeftButton;
    public bool LeftMuki;

    //float int デフォルトだと0が入る
    private float FingerPosX0;//タップし、指が画面に触れた瞬間の指のx座標
    private float FingerPosX1;//タップし、指が画面から離れた瞬間のx座標
    private float FingerPosNow;///現在の指のx座標
    private float PosDiff=20.0f;////x座標の差のいき値
    private Vector2 fingerPosStart;
    private Vector2 fingerPosEnd;
    private const float swipeThreshold = 20.0f;
    [SerializeField] private GachaManager _gachaManager;

    // 矢印の表示/非表示
    
    // Start is called before the first frame update
    void Start()
    {
        currentPanel = Panel.gachaPanel;
        
    }
    public void ShowGachaPanel0() {
        _gachaManager.CloseItemPanel();
        ShowGachaPanel(Panel.gachaPanel);
    }

    public void ShowGachaPanel1() {
        _gachaManager.CloseItemPanel();
        ShowGachaPanel(Panel.Panel1chara);
    }

    public void ShowGachaPanel2() {
        _gachaManager.CloseItemPanel();
        ShowGachaPanel(Panel.Panel2chara);
    }

    public void ShowGachaPanel3() {
        _gachaManager.CloseItemPanel();
        ShowGachaPanel(Panel.Panel3item);
    }
  void Update(){
// タッチ入力を取得する
      if (Input.touchCount > 0)
      {
          Touch touch = Input.GetTouch(0);

          if (touch.phase == TouchPhase.Began)
          {
              fingerPosStart = touch.position;
          }

          if (touch.phase == TouchPhase.Ended)
          {
              fingerPosEnd = touch.position;
              CheckSwipeDirection();
          }
      }
  }
  void CheckSwipeDirection()
  {
      float swipeDistance = fingerPosEnd.x - fingerPosStart.x;

      if (Mathf.Abs(swipeDistance) >= swipeThreshold)
      {
          if (swipeDistance > 0)
          {
              LeftMuki = false;
              SwipeRight();
             
          }
          else
          {
              LeftMuki = true;
              SwipeLeft();
          }
      }
  }
    
    void ShowGachaPanel(Panel panel){
        SoundManager.instance.PlaySE3();
        currentPanel = panel;
        switch(panel){
            case Panel.gachaPanel:
            transform.localPosition = new Vector2(0, 0);
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            break;

            case Panel.Panel1chara:
                //SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel2chara:
                //SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-2000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel3item:
                transform.localPosition = new Vector2(-3000, 0);
                break;
            
            case Panel.Panel4item:
                transform.localPosition = new Vector2(-4000, 0);
                //kihonButton.SetActive(true);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
        }
    }
    //スイプ用のGachaPanel移動について
    void ShowGachaPanel1(Panel panel){
        currentPanel = panel;
        switch(panel){
            case Panel.gachaPanel:
            if(LeftMuki==true){
                transform.localPosition = new Vector2(600, 0);//回転ができるよう初期位置を設定している
                transform.DOLocalMove(new Vector3(0, 0, 0), 0.4f);  
            }
            else{
                transform.DOLocalMove(new Vector3(0, 0, 0), 0.6f);    
            }
            //transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
            //transform.localPosition = new Vector2(0, 0);
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            break;

            case Panel.Panel1chara:
                SoundManager.instance.StopSE();
                if(LeftMuki==true){
                transform.DOLocalMove(new Vector3(-1000, 0, 0), 0.6f);
                }
                else{
                transform.DOLocalMove(new Vector3(-1000, 0, 0), 0.6f);
                }
                //transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel2chara:
                SoundManager.instance.StopSE();
                if(LeftMuki==true){
                transform.DOLocalMove(new Vector3(-2000, 0, 0), 0.6f);
                }
                else{
                transform.DOLocalMove(new Vector3(-2000, 0, 0), 0.6f);
                }
                //transform.localPosition = new Vector2(-2000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel3item:
                if(LeftMuki==true){
                //transform.localPosition = new Vector2(0, 1500);
                transform.DOLocalMove(new Vector3(-3000,0, 0), 0.6f);
            }
            else{
                    transform.localPosition = new Vector2(-3600, 0);
                transform.DOLocalMove(new Vector3(-3000, 0, 0), 0.6f);
            }
                
                break;
            
            case Panel.Panel4item:
            if(LeftMuki==true){
                transform.DOLocalMove(new Vector3(-4000, 0, 0), 0.6f);
            }
            else{
                transform.localPosition = new Vector2(-4600, 0);
                transform.DOLocalMove(new Vector3(-4000, 0, 0), 0.4f);
            }
                //transform.localPosition = new Vector2(-2000, 1500);
                
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
        }
    }
    public void ButtonLR(){
        SoundManager.instance.PlaySE3();
    }

    public void OnRightButton(){
        
    if(currentPanel == Panel.gachaPanel ){
        ShowGachaPanel(Panel.Panel1chara);
        //Debug.Log("2");
        }
    else if(currentPanel == Panel.Panel1chara ){
        ShowGachaPanel(Panel.Panel2chara);
        //Debug.Log("3");
        }

    else if(currentPanel == Panel.Panel2chara ){
        ShowGachaPanel(Panel.Panel3item);
        //Debug.Log("3");
        }
    else if(currentPanel == Panel.Panel3item ){
        ShowGachaPanel(Panel.gachaPanel);
        //Debug.Log("3");
        }
    /*
    else if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel(Panel.gachaPanel);
        //Debug.Log("3");
        }*/
    }
    //左へスワイプスする右へ移動する　LeftMuki=true
    public void SwipeLeft(){ 
        SoundManager.instance.StopSE(); 
        //Debug.Log("左スワイプで右移動"+LeftMuki);
    if(currentPanel == Panel.gachaPanel ){
        ShowGachaPanel1(Panel.Panel1chara);
        //Debug.Log("2");
        }
    else if(currentPanel == Panel.Panel1chara ){
        ShowGachaPanel1(Panel.Panel2chara);
        //Debug.Log("3");
        }

    else if(currentPanel == Panel.Panel2chara ){
        ShowGachaPanel1(Panel.Panel3item);
        //Debug.Log("3");
        }
    else if(currentPanel == Panel.Panel3item ){
        ShowGachaPanel1(Panel.gachaPanel);
        //Debug.Log("3");
        }
    /*
    else if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel1(Panel.gachaPanel);
        //Debug.Log("3");
        }*/
        SoundManager.instance.PlaySE3();
    }

    public void OnLeftButton(){
        
        /*if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel(Panel.Panel3item);
        //Debug.Log("2");
    }*/
    if(currentPanel == Panel.Panel3item){
        ShowGachaPanel(Panel.Panel2chara);
        //Debug.Log("1");
        }
    else if(currentPanel == Panel.Panel2chara){
        ShowGachaPanel(Panel.Panel1chara);
        //Debug.Log("1");
        }

    else if(currentPanel == Panel.Panel1chara){
        ShowGachaPanel(Panel.gachaPanel);
        //Debug.Log("5");
        }
    else if(currentPanel == Panel.gachaPanel){
        ShowGachaPanel(Panel.Panel3item);
        //Debug.Log("5");
        }
    }
    //みぎ方向へスワイプ左へへ移動する
    public void SwipeRight(){
        SoundManager.instance.StopSE(); 
        //Debug.Log("右スワイプで左移動"+LeftMuki);
        /*if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel1(Panel.Panel3item);
        //Debug.Log("2");
    }*/
    if(currentPanel == Panel.Panel3item){
        ShowGachaPanel1(Panel.Panel2chara);
        //Debug.Log("1");
        }
    else if(currentPanel == Panel.Panel2chara){
        ShowGachaPanel1(Panel.Panel1chara);
        //Debug.Log("1");
        }

    else if(currentPanel == Panel.Panel1chara){
        ShowGachaPanel1(Panel.gachaPanel);
        //Debug.Log("5");
        }
    else if(currentPanel == Panel.gachaPanel){
        ShowGachaPanel1(Panel.Panel3item);
        //Debug.Log("5");
        }
        SoundManager.instance.PlaySE3();
    }
    
}
