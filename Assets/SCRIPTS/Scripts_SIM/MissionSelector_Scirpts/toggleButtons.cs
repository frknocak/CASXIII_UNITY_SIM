using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleButtons : MonoBehaviour
{
   public Toggle _toggle;    
   
   void Start()
   {
    _toggle.GetComponent<Toggle>();

   }
   void Update(){
    Value_change(_toggle.isOn);
   }
   public void Value_change(bool val) {

    if(val == true)
    {
        PlayerPrefs.SetInt("randomizer",1);
    } 
    else
    {
        PlayerPrefs.SetInt("randomizer",0);
    }
   }

}
