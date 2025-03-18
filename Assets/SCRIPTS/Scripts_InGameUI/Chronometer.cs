using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{

    public Text ChronometerText;
    public float timeValue = 900f;

    public bool chronometerIsActive = false;
    public bool chronometerReset = false;
    public int seconds;
    public int minutes;

    // Start is called before the first frame update
    void Start()
    {

            timeValue = 900;
            seconds = Mathf.FloorToInt(timeValue % 60);
            minutes = Mathf.FloorToInt(timeValue / 60);
            ChronometerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {

        if (chronometerIsActive == true)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;

            }
            else
            {
                timeValue = 0;
            }
            seconds = Mathf.FloorToInt(timeValue % 60);
            minutes = Mathf.FloorToInt(timeValue / 60);
            ChronometerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        if (chronometerIsActive == false)
        {
            ChronometerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if (chronometerReset == true)
        {
            timeValue = 900f;
            seconds = Mathf.FloorToInt(timeValue % 60);
            minutes = Mathf.FloorToInt(timeValue / 60);
            ChronometerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            chronometerReset = false;
            chronometerIsActive = false;
            
        }
        else if(chronometerIsActive == true && chronometerReset == false && timeValue == 0)
        {
            PlayerPrefs.SetInt("timeIsUp", 1);
        }


    }


    public void StartChronometer()
    {
        chronometerIsActive = true;

    }
    public void StopChronometer() // PAUSE 
    {
        chronometerIsActive = false;
    }
    public void ResetChronometer()
    {
        chronometerReset = true;

    }
}
