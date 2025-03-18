using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLocationManager : MonoBehaviour
{
    private int location_;
    public GameObject underWaterObj;
    public GameObject lookAtObject;

    public GameObject[] spawnPoints;

    // public GameObject spawnPoint1;
    // public GameObject spawnPoint2;

    // public GameObject spawnPoint3;

    // public GameObject spawnPoint4;


    // public GameObject spawnPoint5;

    // public GameObject spawnPoint6;

    void Start(){
        location_  = PlayerPrefs.GetInt("location");
        
        underWaterObj.transform.position = spawnPoints[location_-1].transform.position;
        underWaterObj.transform.LookAt(lookAtObject.transform.position);
        
    }





    public void Button1(){
        PlayerPrefs.SetInt("location",1);
    }
    public void Button2(){
        PlayerPrefs.SetInt("location",2);
    }
    public void Button3(){
        PlayerPrefs.SetInt("location",3);
    }
    public void Button4(){
        PlayerPrefs.SetInt("location",4);
    }
    public void Button5(){
        PlayerPrefs.SetInt("location",5);
    }
    public void Button6(){
        PlayerPrefs.SetInt("location",6);
    }
}
