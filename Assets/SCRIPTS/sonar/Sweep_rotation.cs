using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep_rotation : MonoBehaviour
{
    public RadarScanner rs;

    void Update()
    {
        // RadarScanner'�n Y d�n���n� al ve sweep �ubu�una uygula
        Vector3 rotation = transform.eulerAngles;
        rotation.z = rs.transform.eulerAngles.y;  // Z ekseni UI i�in d�ner
        transform.eulerAngles = rotation;
    }
}
