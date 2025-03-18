using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPipe : MonoBehaviour
{
    public GameObject myObject;

    public GameObject[] positions;

    void Awake()
    {
        int randomIndex = Random.Range(0, positions.Length);

        myObject.transform.position = positions[randomIndex].transform.position;

    }
}