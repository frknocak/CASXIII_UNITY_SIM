using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationForM1 : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float target_rotation = 0f;
    public GameObject ROV;


    private void FixedUpdate()
    {
    Quaternion mevcutRotasyon = ROV.transform.rotation;

    Quaternion targetRotateEuler = Quaternion.Euler(0, target_rotation, 0);

    ROV.transform.rotation = Quaternion.Lerp(mevcutRotasyon, targetRotateEuler, Time.deltaTime * rotationSpeed);

    }
}

