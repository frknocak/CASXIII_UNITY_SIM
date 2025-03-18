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
        Debug.Log("Selected Random Number: " + (randomNumber+1)); // Rastgele se�ilen numaray� konsola yazd�r
        Debug.Log("Selected Texture Name: " + selectedTexture.name); // Se�ilen dokunun ad�n� konsola yazd�r
    }

    void LoadTextures()
    {
        // Resources klas�r�nden t�m Texture2D varl�klar�n� y�kle
        Object[] loadedObjects = Resources.LoadAll("Arucos", typeof(Texture2D));

        // Texture2D t�r�nde olanlar� ay�r
        Arucos = loadedObjects.OfType<Texture2D>().ToArray();

        // Texture2D dizisini dosya adlar�na g�re s�ralamak i�in
        Arucos = Arucos.OrderBy(tex => int.Parse(tex.name)).ToArray();
    }

    Texture2D arucodeneme()
    {
        randomNumber = Random.Range(1, Arucos.Length);
        return Arucos[randomNumber];
    }
}
