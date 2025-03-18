using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_starting : MonoBehaviour
{
    public GameObject myObject;
    public GameObject underWaterObj;
    public GameObject[] positions;
    public float sapma = -3.5f;

    void Awake()
    {
        int randomIndex = Random.Range(0, positions.Length);

        myObject.transform.position = positions[randomIndex].transform.position;
        underWaterObj.transform.position = myObject.transform.position + new Vector3(0, sapma, 0);

    }
}
