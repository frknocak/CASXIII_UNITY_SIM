using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("isFinished", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "gripper")
        {
            //PlayerPrefs.SetInt("isFinished", 1);
            SceneManager.LoadScene("SaveScoreScene");
        
        }
        
    }
    
        
    
}
