using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class randomAruco : MonoBehaviour
{
    private Texture2D[] Arucos;
    private int randomNumber;

    void Start()
    {
        LoadTextures();
        Texture2D selectedTexture = arucodeneme();
        GetComponent<Renderer>().material.mainTexture = selectedTexture;
        Debug.Log("Selected Random Number: " + (randomNumber+1)); // Rastgele seçilen numarayý konsola yazdýr
        Debug.Log("Selected Texture Name: " + selectedTexture.name); // Seçilen dokunun adýný konsola yazdýr
    }

    void LoadTextures()
    {
        // Resources klasöründen tüm Texture2D varlýklarýný yükle
        Object[] loadedObjects = Resources.LoadAll("Arucos", typeof(Texture2D));

        // Texture2D türünde olanlarý ayýr
        Arucos = loadedObjects.OfType<Texture2D>().ToArray();

        // Texture2D dizisini dosya adlarýna göre sýralamak için
        Arucos = Arucos.OrderBy(tex => int.Parse(tex.name)).ToArray();
    }

    Texture2D arucodeneme()
    {
        randomNumber = Random.Range(1, Arucos.Length);
        return Arucos[randomNumber];
    }
}
