using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class missionSelector : MonoBehaviour
{
    public int mission;
    public Slider rotationYSlider;
    public GameObject rovpng;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        // Get rotation value from the Y-axis slider
        float rotationY = rotationYSlider.value;

        // Apply rotation to the object only around the Y-axis
        rovpng.transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
    

    public void OpenScene()
    {
        SceneManager.LoadScene("Mission" + mission.ToString());
    }
    public void MateROVSceneCall(){
        SceneManager.LoadScene("MateROVScene");
    }
    public void QuitButton(){
        Application.Quit();
    }
}
