using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeIsUp : MonoBehaviour
{
    public int timeIsUpInt = 0;
    public bool timeIsUpBool = false;
    public bool finishBool = false;
    public int finishInt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        finishInt = PlayerPrefs.GetInt("isFinished");
        if(finishInt == 1)
        {
            finishBool = true;
        }

        timeIsUpInt = PlayerPrefs.GetInt("timeIsUp");
        if(timeIsUpInt == 1)
        {
            timeIsUpBool = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsUpBool == true && finishBool == false)
        {

            Debug.Log("Time is Up. Start Again");
        }
    }
}
