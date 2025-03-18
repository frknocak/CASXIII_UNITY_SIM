using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishScreenLoader : MonoBehaviour
{
    public bool finishBool = false;
    public int finishInt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        finishInt = PlayerPrefs.GetInt("isFinished");
        if(finishInt == 1)
        {
            finishBool = true;
        }

    
    }
    public void OpenScene()
    {
        if(finishBool == true)
        {
        SceneManager.LoadScene("SaveScoreScene");
        }
    }
}
