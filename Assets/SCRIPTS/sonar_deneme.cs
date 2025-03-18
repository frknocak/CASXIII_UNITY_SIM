using System;
using UnityEngine;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class sona_deneme : MonoBehaviour
{


    public Vector3 targetDirection;
    public Transform sonar_head_transform;

    List<List<float>> placeHolderLists = new List<List<float>>();
    List<List<float>> raycastPerDegreeList = new List<List<float>>();
    RaycastHit hit;
    public float ROVsBlindSpot = 0.001f; //sonar_head'i çok hızlı bir şekilde (realistik değil) sallayınca head ve body birbirine çarpıyor
    // bu da 0.0017 gibi yanlış değerler üretiyor.
    private float rotation = 0f;
    private float rotationAmount = 0.9f; //360/400. bluerobotics sonarı 400 derecede tarama yapıyor.
    private float rotationInterval = 0.0875F; // *** birim başı dönme hızı (35/400) *** 
    private float startTime;
    public float facingAngle;
    string folderPath;


    
    string filePath;
    int a;


    private void Start()
    {
        CheckForFile();
        startTime = Time.time;


        // ****  25 tane liste oluşturmak için döngüler ****
        for (int i = 0; i < 25; i++)
        {
            placeHolderLists.Add(new List<float>());
            for (int j = 0; j < 400; j++)
            {
                placeHolderLists[i].Add(0);
            }
        }


         for (int i = 0; i < 400; i++)
        {
            raycastPerDegreeList.Add(new List<float>());

            for (int j = 0; j < 25; j++)
            {
                raycastPerDegreeList[i].Add(0);

            }
            // Debug.Log(string.Format("The RAYCASTPERDEGREE {0}th list is: {1}", i + 1, string.Join(", ", raycastPerDegreeList[i])));
        }
    }





    private void FixedUpdate()
    {
        // **** ADIM ADIM DÖNME KODU ****

        if (Time.time - startTime >= rotationInterval)
        {
            startTime = Time.time;
            rotation += rotationAmount;
            transform.Rotate(Vector3.up, rotationAmount);
        }

        if (rotation >= 360f)
        {
            //rotation = 0f; // *** 360 derecede bir liste sıfırlamk için kapattım.
        }

        // RAYCAST ARRAYİ TANIMLAMA
        Ray[] raycasts = new Ray[25];


        facingAngle = sonar_head_transform.rotation.eulerAngles.y;
        int intFacingAngle = Convert.ToInt32(facingAngle);


        for (int a = 0; a < 25; a++)
        {
            targetDirection = sonar_head_transform.rotation * Quaternion.Euler(a, 0, 0) * Vector3.forward;

            raycasts[a] = new Ray(sonar_head_transform.transform.position, targetDirection);

            Debug.DrawRay(sonar_head_transform.transform.position, targetDirection * 50, Color.green);

            if (Physics.Raycast(raycasts[a], out hit, 50))
            {

                Debug.DrawRay(sonar_head_transform.transform.position, targetDirection * 50, Color.red);
                Debug.DrawRay(hit.point, Vector3.up, Color.red, 0.01f);

                //Debug.Log(string.Format("{0}. ray için en kisa mesafe:{1}", a+1, hit.distance)); 



                placeHolderLists[a][intFacingAngle] = hit.distance;   //a. listenin b. elemanını hit.distance ile değiştirmek



            }
            else
            {
                print("engel yok");
            }

            if (rotation % 360f == 0)
            {
                //placeHolderLists[a] = placeHolderLists[a].OrderBy(dist => dist).ToList(); // küçükten büyüğe sıralamak için aç

                Debug.Log(string.Format("The {0}th list is: {1}", a + 1, string.Join(", ", placeHolderLists[a])));
                if (rotation != 0)
                {
                    WriteArraysToTextFile(placeHolderLists);


                    placeHolderLists[a].Clear();



                    for (int j = 0; j < 400; j++)
                    {
                        placeHolderLists[a].Add(0);
                    }

                    print("LIST IS CLEARED");
                }

            }
        }


    }



    void TransposeData()
    {
        // Transpose data from placeHolderLists to raycastPerDegreeLists
        for (int i = 0; i < 25; i++) // Iterate over arrays in placeHolderLists
        {
            for (int j = 0; j < 400; j++) // Iterate over degrees
            {
                raycastPerDegreeList[j][i] = placeHolderLists[i][j];
            }
        }
    }

    void CheckForFile()
    {
        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sim Sonar Data");
        string filePath = folderPath + "/ArrayData.txt";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);

        }
        File.WriteAllText(filePath, string.Empty);

    }

    void WriteArraysToTextFile(List<List<float>> lists)
    {
        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sim Sonar Data");
        string filePath = folderPath + "/ArrayData.txt";
        using (StreamWriter writer = File.AppendText(filePath))
        {
            for (int i = 0; i < lists.Count; i++)
            {
                // Write each element of the list to the file
                writer.WriteLine($"The {i + 1}th list is: {string.Join(", ", lists[i])}");
            }
        }
    }
}