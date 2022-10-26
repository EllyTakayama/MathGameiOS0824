using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//0905

public class TestToggle : MonoBehaviour
{ 
    public Toggle test5Toggle;
    public Toggle test7Toggle;
     public Toggle test9Toggle;
     public static int testQuestion;
    
    // Start is called before the first frame update

    public void OnClickTog(){
        Debug.Log("toggle");
    }

    void Start()
    {
     OnTestToggLoad();
      Debug.Log("TestQuestion"+testQuestion);
    }

    public void OnClickTestTog(){ 
      if (test5Toggle.isOn == true){
            testQuestion = 5;
             SoundManager.instance.PlaySEButton();
            ES3.Save<int>("testQuestion", testQuestion);
             Debug.Log("Test5Save");
            
            } 
        if(test7Toggle.isOn ==true){
            testQuestion = 7;
             SoundManager.instance.PlaySEButton();
            ES3.Save<int>("testQuestion", testQuestion);
       Debug.Log("Test7Save");
        }
        if(test9Toggle.isOn ==true){
          testQuestion = 9;
           SoundManager.instance.PlaySEButton();
          ES3.Save<int>("testQuestion", testQuestion);
          Debug.Log("Test9Save");
    
        }
   }   
   

    public void OnTestToggLoad(){
      testQuestion = ES3.Load<int>("testQuestion", 9);
      Debug.Log("SaveTest");
     if (testQuestion == 9){
            test5Toggle.isOn = false;
            test7Toggle.isOn = false;
            test9Toggle.isOn = true;
            Debug.Log("Load9");
            }
        if(testQuestion == 7){
            test5Toggle.isOn = false;
            test7Toggle.isOn = true;
            test9Toggle.isOn = false;
              Debug.Log("Load7");
            }
       if(testQuestion == 5){
            test5Toggle.isOn = true;
            test7Toggle.isOn = false;
            test9Toggle.isOn = false;
              Debug.Log("Load5");
            }
    }
   
   
}
