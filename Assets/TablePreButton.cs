using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TablePreButton : MonoBehaviour
{ 
    public GameObject imageTable1;
    public GameObject imageTable2;
    public GameObject imageTable3;
    public GameObject imageTable4;
    public GameObject imageTable5;
    public GameObject imageTable6;
    public GameObject imageTable7;
    public GameObject imageTable8;
     public GameObject imageTable9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ImageTable(){
        if(GameManager.singleton.currentMode ==1){
           imageTable1.SetActive(true);
        }
        else if(GameManager.singleton.currentMode ==2){
            imageTable2.SetActive(true);
        }
        else if(GameManager.singleton.currentMode ==3){
            imageTable3.SetActive(true);
        }
        else if(GameManager.singleton.currentMode ==4){
            imageTable4.SetActive(true);
        }
        else if(GameManager.singleton.currentMode ==5){
            imageTable6.SetActive(true);
        }
        else if(GameManager.singleton.currentMode ==7){
            imageTable7.SetActive(true);
        }
        else if(GameManager.singleton.currentMode ==8){
            imageTable8.SetActive(true);
        }
        else if(GameManager.singleton.currentMode ==9){
            imageTable9.SetActive(true);
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
