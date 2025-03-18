using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;


public class InGameSettingsMenu : MonoBehaviour
{
    public bool img_toByte_control = false;

    public PostProcessProfile ppp;
    public PostProcessProfile undercam_ppp;
    public TMP_Dropdown frontcamdropdown;
    public TMP_Dropdown undercamdropdown;

    public GameObject take_screenshot;
    public GameObject causticsController;
    public GameObject biggerPool;
    public GameObject smallerPool;
    [SerializeField] public int PPPOnOff = 1;
    [SerializeField] public int causticsOnOff = 1;
    [SerializeField] public int poolSizeBigOrLarge = 1;
    [SerializeField] public int TSCoff = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PPPOnOff", 1);
        PlayerPrefs.SetInt("causticsPref", 1);
        PlayerPrefs.SetInt("TSCoff",0);
        PlayerPrefs.SetInt("poolSizePref", 1);
        ActivatePostProcessing(true);
    }

    // Update is called once per frame
    void Update()
    {
// GETTING THE PREFS
        PPPOnOff = PlayerPrefs.GetInt("PPPOnOff");
        causticsOnOff = PlayerPrefs.GetInt("causticsPref");
        TSCoff = PlayerPrefs.GetInt("TSCoff");
        poolSizeBigOrLarge = PlayerPrefs.GetInt("poolSizePref");

// POST-PROCESS PROFILE SETTINGS        
        if(PPPOnOff == 0){
            ActivatePostProcessing(false);
        }
        else if(PPPOnOff==1){
            ActivatePostProcessing(true);
        }
//CAUSTICS SETTINGS
        if(causticsOnOff == 0){
            causticsController.SetActive(false);
        }
        else if(causticsOnOff==1){
            causticsController.SetActive(true);
        }
//TSC SETTINGS
        if (TSCoff == 0)
        {
            if (take_screenshot != null)
            {
                imgToByte script = take_screenshot.GetComponent<imgToByte>();
                if (script != null)
                {
                    img_toByte_control = false;
                    script.enabled = false;
                }
            }
        }
        else if (TSCoff == 1)
        {
            if (take_screenshot != null)
            {
                imgToByte script = take_screenshot.GetComponent<imgToByte> ();
                if (script != null)
                {
                    img_toByte_control = true;
                    script.enabled = true;
                }
            }
        }

        // POOL SIZE SETTINGS
        if (poolSizeBigOrLarge == 0){
            smallerPool.SetActive(false);
            biggerPool.SetActive(true);
        }
        else if(poolSizeBigOrLarge==1){
            biggerPool.SetActive(false);
            smallerPool.SetActive(true);
        }


        // cam res setts
        PlayerPrefs.SetInt("frontcampref", frontcamdropdown.value);
        PlayerPrefs.SetInt("undercampref", undercamdropdown.value);
        Debug.Log("fcp" + frontcamdropdown.value.ToString());



    }

   /* public void ApplyButton(){
        // GETTING THE PREFS
        PPPOnOff = PlayerPrefs.GetInt("PPPOnOff");
        causticsOnOff = PlayerPrefs.GetInt("causticsPref");
        poolSizeBigOrLarge = PlayerPrefs.GetInt("poolSizePref");

// POST-PROCESS PROFILE SETTINGS        
        if(PPPOnOff == 0){
            ActivatePostProcessing(false);
        }
        else if(PPPOnOff==1){
            ActivatePostProcessing(true);
        }
//CAUSTICS SETTINGS
        if(causticsOnOff == 0){
            causticsController.SetActive(false);
        }
        else if(causticsOnOff==1){
            causticsController.SetActive(true);
        }

// POOL SIZE SETTINGS
        if(poolSizeBigOrLarge == 0){
            biggerPool.SetActive(true);
            smallerPool.SetActive(false);
        }
        else if(poolSizeBigOrLarge==1){
            biggerPool.SetActive(false);
            smallerPool.SetActive(true);
        }
    }
*/
    void ActivatePostProcessing(bool activate)
    {
        ColorGrading colorGrading;
            if (ppp.TryGetSettings(out colorGrading))
            {
                colorGrading.enabled.value = activate;
            }
        DepthOfField depthOfField;
            if (ppp.TryGetSettings(out depthOfField))
            {
                depthOfField.enabled.value = activate;
            }
        ColorGrading colorGrading2;
            
            if (undercam_ppp.TryGetSettings(out colorGrading2))
            {
                colorGrading.enabled.value = activate;
            }
        
    }

// BUTTON FUNCTIONS
    public void PPP_PreferencesOn(){
        PlayerPrefs.SetInt("PPPOnOff", 1);
    }
    public void PPP_PreferencesOff(){
        PlayerPrefs.SetInt("PPPOnOff", 0);
    }

    public void Caustics_On_Button(){
        PlayerPrefs.SetInt("causticsPref", 1);
    }
    public void Caustics_Off_Button(){
        PlayerPrefs.SetInt("causticsPref", 0);
    }
    public void TSC_On_Button()
    {
        PlayerPrefs.SetInt("TSCoff", 1);
    }
    public void TSC_Off_Button()
    {
        PlayerPrefs.SetInt("TSCoff", 0);
    }
    public void PoolSizeSmallButton(){
        PlayerPrefs.SetInt("poolSizePref", 1);
    }
    public void PoolSizeLargeButton(){
        PlayerPrefs.SetInt("poolSizePref", 0);
    }
}
