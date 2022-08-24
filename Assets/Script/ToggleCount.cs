using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleCount : MonoBehaviour
{
    public int T1Count;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ToggleOnClick(){
        Debug.Log("T1Click");
        T1Count++;
        Debug.Log("T1"+T1Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}