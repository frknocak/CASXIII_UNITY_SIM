using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{
    public GameObject circle2Prefab;
    public GameObject recteanglePrefab;
    public GameObject circlePrefab;
    public int received_toggle2;

    void Start()
    {
        //setup 1
        Vector3 positionSetUp1 = new Vector3(10, -1, 5);
        Vector3 positionSetUp2 = new Vector3(-5, -8, 5);
        Vector3 positionSetUp3 = new Vector3(-19, -1, 5);
        //setup 2
        Vector3 positionSetUp4 = new Vector3(-18, -0.75f, -7);
        Vector3 positionSetUp5 = new Vector3(13, -7, 5);
        Vector3 positionSetUp6 = new Vector3(-7, 0, 0);

        //setup 3
        Vector3 positionSetUp7 = new Vector3(-18f, -0.75f, -7.2f);
        Vector3 positionSetUp8 = new Vector3(2, -7f, 5);
        Vector3 positionSetUp9 = new Vector3(12, -0.75f, 0);

        received_toggle2 = PlayerPrefs.GetInt("randomizer2");
        if (received_toggle2 == 1)
        {
            int index = Random.Range(1, 4);
            if (index == 1)
            {
                Debug.Log(index);
                Debug.Log("Setup1 kuruldu");
                Instantiate(circle2Prefab, positionSetUp1, new Quaternion(1f, 0f, 0f, 1));
                Instantiate(recteanglePrefab, positionSetUp2, Quaternion.identity);
                Instantiate(circlePrefab, positionSetUp3, new Quaternion(1f, 0f, 0f, 1));
            }

            if (index == 2)
            {
                Debug.Log(index);
                Debug.Log("Setup2 kuruldu");
                Instantiate(circle2Prefab, positionSetUp4, new Quaternion(1f, 0f, 0f, 1));
                Instantiate(recteanglePrefab, positionSetUp5, Quaternion.identity);
                Instantiate(circlePrefab, positionSetUp6, new Quaternion(1f, 0f, 0f, 1));
            }
            if (index == 3)
            {
                Debug.Log(index);
                Debug.Log("Setup3 kuruldu");
                Instantiate(circle2Prefab, positionSetUp7, new Quaternion(1f, 0f, 0f, 1));
                Instantiate(recteanglePrefab, positionSetUp8, Quaternion.identity);
                Instantiate(circlePrefab, positionSetUp9, new Quaternion(1f, 0f, 0f, 1));
            }
        }
        else
        {
            Instantiate(circle2Prefab, new Vector3(10, -1, 5), new Quaternion(1f, 0f, 0f, 1));
            Instantiate(recteanglePrefab, new Vector3(-5, -8, 5), Quaternion.identity);
            Instantiate(circlePrefab, new Vector3(-19, -1, 5), new Quaternion(1f, 0f, 0f, 1));
        }

    }



}
