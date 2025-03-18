using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROV_TPSCamera : MonoBehaviour
{
    public Transform underWaterObj;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = underWaterObj.position + offset;
    }
}
