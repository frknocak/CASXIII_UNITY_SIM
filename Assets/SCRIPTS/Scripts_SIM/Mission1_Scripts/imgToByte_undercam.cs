using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class imgToByte_undercam : MonoBehaviour
{
   public bool StartRecording = false;
   public int resWidth;
   public int resHeight;

     
 
     public static string ScreenShotName(int resWidth, int resHeight) {
        /* return string.Format("{0}/screenshots/UNDERCAM_screen_{1}x{2}_{3}.png", 
                              Application.dataPath, 
                              resWidth, resHeight, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));*/

   //    string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "Screenshots");

   //    if (!Directory.Exists(folderPath))
   //      {
   //          Directory.CreateDirectory(folderPath);

   //      }
   //       return string.Format(System.IO.Path.Combine(
   //  System.Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop ), "Screenshots",
   //      "img_undercam.png"));

   string folderPath = @"C:\Screenshots";

   if (!Directory.Exists(folderPath))
      {
         Directory.CreateDirectory(folderPath);
      }

      return Path.Combine(folderPath, "img_undercam.png");

}
//---------BU KISIM BYTELARI YAZDIRMAK İÇİN--------
/*public static string ByteTextName(int resWidth, int resHeight) {
     return string.Format("{0}/txtFiles/txtFile_{1}x{2}_{3}.txt", 
                          Application.dataPath, 
                          resWidth, resHeight, 
                          System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));*/


void Start()
{
   if (StartRecording)
   {
      InvokeRepeating("TakeScreenshots", 1f, 2f);
   }
}

void Update(){
   int underamPref = PlayerPrefs.GetInt("undercampref");
   if(underamPref == 0){
      resWidth = 640;
      resHeight = 640;

   }
   if(underamPref == 1){
      resWidth = 1280;
      resHeight = 720;

   }

   if(underamPref == 2){
      resWidth = 1920;
      resHeight = 1080;
   }

}

void TakeScreenshots()
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
