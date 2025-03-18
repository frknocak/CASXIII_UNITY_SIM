using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Img_to_Byte_topCam_M1 : MonoBehaviour
{
    public bool StartRecording = false;
    public int resWidth = 640;
    public int resHeight = 640;
    private RenderTexture rt;
    private Texture2D screenShot;
    private int screenshotIndex = 0;

    void Start()
    {
        // Render Texture ve Texture2D'yi baþlat
        rt = new RenderTexture(resWidth, resHeight, 24);
        screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

        if (StartRecording)
        {
            StartCoroutine(TakeScreenshotsAsync());
        }
    }

    void OnDestroy()
    {
        // Kaynaklarý serbest býrak
        if (rt != null)
        {
            rt.Release();
            Destroy(rt);
        }

        if (screenShot != null)
        {
            Destroy(screenShot);
        }

        Resources.UnloadUnusedAssets();
    }

    IEnumerator TakeScreenshotsAsync()
    {
        float frameRate = 30f; // Saniyede 30 kare
        float delay = 1f / frameRate; // Çaðrý aralýðý

        while (true)
        {
            yield return new WaitForSeconds(delay);

            // Render Texture'a kareyi render et
            GetComponent<Camera>().targetTexture = rt;
            GetComponent<Camera>().Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null;

            // Kareyi PNG formatýnda kaydet
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
        }
    }

    public static string ScreenShotName(int resWidth, int resHeight)
    {
        string folderPath = @"C:\Screenshots";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        return Path.Combine(folderPath, $"img_t.png");
    }
}
