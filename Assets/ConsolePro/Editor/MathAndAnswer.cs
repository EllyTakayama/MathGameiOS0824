
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathAndAnswer : MonoBehaviour
{
      //we make this script instance
    


    //its an enum which we help use to identify the current mode of game 
    public enum MathsType
    {
        addition,
        subtraction,
        multiplication,
        division,
        mix
    }

    //we make a variable of MathsType
    public MathsType mathsType;

    //2 private floats this are the question values a and b
    private float a, b ;
    //the variable for answer value
    [HideInInspector] public float answer;
    //varible whihc will assign ans to any one of the 4 answer button
    private float locationOfAnswer;
    //ref to the button
    public GameObject[] ansButtons;
    //get the tag of button 
    public string tagOfButton;
    //ref to text in scene where we will assign a and b values of question
    public Text valueA , valueB;

    
     void Awake()
    {
     
    }



    // Start is called before the first frame update
    void Start()
    {
        //we put the location value in tag of button variable
        tagOfButton = locationOfAnswer.ToString();
        //we call the methods
       
    }

    



    // Update is called once per frame
    void Update()
    {
         tagOfButton = locationOfAnswer.ToString();

    }
    //Multiplication
    //this methode perform Multiplication process
    void MultiplicationMethod()
    {
        //similar to the addition method only we do multiplication here
        a = 1;
        b = 9;


        locationOfAnswer = Random.Range(0, ansButtons.Length);

        answer = a * b;

        valueA.text = "" + a;
        valueB.text = "" + b;

          for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i == locationOfAnswer)
            {

                ansButtons[i].GetComponentInChildren<Text>().text = "" + answer;

            }
        }


    }
}
