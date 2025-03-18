using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleButtons3 : MonoBehaviour
{
    public Toggle _toggle3;
    // Start is called before the first frame update
    void Start()
    {
        _toggle3.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
     Value_change3(_toggle3.isOn);   
    }

    public void Value_change3(bool val3) {

    if(val3 == true)
    {
        PlayerPrefs.SetInt("randomizer3",1);
    } 
    else
    {
        PlayerPrefs.SetInt("randomizer3",0);
    }
   }
}
