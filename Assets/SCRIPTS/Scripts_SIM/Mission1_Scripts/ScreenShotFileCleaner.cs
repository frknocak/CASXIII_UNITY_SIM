using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ScreenShotFileCleaner : MonoBehaviour
{
    [SerializeField]
    private string filePath; // Path to the JPEG file you want to clear
    public int yoloProcedureActivatedInt = 0;
    private int updatedIndex;

    void Awake()
    {
    filePath = Application.dataPath + "/screenshots/";    
    }

    void Update()
    {
        yoloProcedureActivatedInt = PlayerPrefs.GetInt("yoloProcedureActivated");
        if(yoloProcedureActivatedInt == 0){
            CleanFolder();
        }

        updatedIndex = PlayerPrefs.GetInt("UpdatedIndexforEverySS"); // bunlar iltekgameden kalma sileceğim
        
        
        if(updatedIndex >= 60){
            CleanFolder();
        }
        
    }
    public void CleanFolder()
    {
        
        DirectoryInfo directory = new DirectoryInfo(filePath);

        // Delete all files inside the folder
        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }

        // Delete all subdirectories and their files
        foreach (DirectoryInfo subDirectory in directory.GetDirectories())
        {
            subDirectory.Delete(true);
        }
    }
    /*public void Auto_Cleaner(){

        int fileCount = 0;
        DirectoryInfo directory = new DirectoryInfo(filePath);

        foreach (FileInfo file in directory.GetFiles())
        {
            fileCount ++;
        }

        if(fileCount >= 30){
        
        }
    }*/
    
}
