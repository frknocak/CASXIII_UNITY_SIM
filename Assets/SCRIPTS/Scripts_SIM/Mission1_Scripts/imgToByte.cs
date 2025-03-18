using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEditor;
public class imgToByte : MonoBehaviour {
     public bool StartRecording = false; 
     public int resWidth; 
     public int resHeight;
    private int frameNumarasi = 0; // Frame numarasını takip etmek için bir sayaç
    public InGameSettingsMenu menu;
    public bool img_control = false;
    

    public string ScreenShotName(int resWidth, int resHeight) {
         /*return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
                              Application.dataPath, 
                              resWidth, resHeight, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));*/
        // string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "Screenshots");

      string folderPath = @"C:\Screenshots";

        //    if (!Directory.Exists(folderPath))
        //      {
        //          Directory.CreateDirectory(folderPath);

        //      }
        //       return string.Format(System.IO.Path.Combine(
        //  System.Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop ), "Screenshots",
        //      "img.png"));
        //  if (!menu.img_toByte_control)
        //    {
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }
        //        img_control = true;
        //        return Path.Combine(folderPath, "img.png");


        //    }
        //  else
        //    {
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }


        //        // Geçerli tarih ve saat damgasını al
        //        string zamanDamgasi = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //        // Frame numarasını artır
        //        frameNumarasi++;
        //        // Dosya yolunu tarih, saat ve frame numarası ile birleştir
        //        return Path.Combine(folderPath, $"img_{zamanDamgasi}_frame{frameNumarasi}.png");
        //        //return Path.Combine(folderPath, "img.png");
        //    }
        //    //---------BU KISIM BYTELARI YAZDIRMAK İÇİN--------
        //    /*public static string ByteTextName(int resWidth, int resHeight) {
        //    return string.Format("{0}/txtFiles/txtFile_{1}x{2}_{3}.txt", 
        //                         Application.dataPath, 
        //                         resWidth, resHeight, 
        //                         System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));*/
        //}
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }


        // Geçerli tarih ve saat damgasını al
        string zamanDamgasi = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        // Frame numarasını artır
        frameNumarasi++;
        // Dosya yolunu tarih, saat ve frame numarası ile birleştir
        return Path.Combine(folderPath, $"img_{zamanDamgasi}_frame{frameNumarasi}.png");
        //return Path.Combine(folderPath, "img.png");
    }
    //---------BU KISIM BYTELARI YAZDIRMAK İÇİN--------
    /*public static string ByteTextName(int resWidth, int resHeight) {
    return string.Format("{0}/txtFiles/txtFile_{1}x{2}_{3}.txt", 
                         Application.dataPath, 
                         resWidth, resHeight, 
                         System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));*/


    void Start() {
      if(StartRecording){
            InvokeRepeating("TakeScreenshots", 1f, 0.1f);
            
      }
     }

void Update(){
    int frontCamPref = PlayerPrefs.GetInt("frontcampref");
    if(frontCamPref == 0){
        resWidth = 640;
        resHeight = 640;

    }
    if(frontCamPref == 1){
        resWidth = 1280;
        resHeight = 720;

    }

    if(frontCamPref == 2){
        resWidth = 1920;
        resHeight = 1080;

    }

}



void TakeScreenshots(){
        if (menu.img_toByte_control)
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            GetComponent<Camera>().Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();

             string filename = ScreenShotName(resWidth, resHeight);
                //---------BU KISIM BYTELARI YAZDIRMAK İÇİN--------
             /*string txtFilename = ByteTextName(resWidth, resHeight);
             string bytesTEXT = string.Join(",", bytes);*/

             System.IO.File.WriteAllBytes(filename, bytes);
             Debug.Log(string.Format("Took screenshot to: {0}", filename));
                //---------BU KISIM BYTELARI YAZDIRMAK İÇİN--------
             //System.IO.File.WriteAllText(txtFilename, bytesTEXT);
             //Debug.Log(string.Format("Took byteText to: {0}", txtFilename));
        }


    }
}