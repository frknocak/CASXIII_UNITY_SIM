using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleButtons2 : MonoBehaviour
{
    public Toggle _toggle2;    
   
   void Start()
   {
    _toggle2.GetComponent<Toggle>();
   }
   void Update(){
    Value_change2(_toggle2.isOn);
   }
   public void Value_change2(bool val2) {

    if(val2 == true)
    {
        PlayerPrefs.SetInt("randomizer2",1);
    } 
    else
    {
        PlayerPrefs.SetInt("randomizer2",0);
    }
   }

}
