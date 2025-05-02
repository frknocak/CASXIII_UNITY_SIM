using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep_rotation : MonoBehaviour
{
    public RadarScanner rs;

    void Update()
    {
        // RadarScanner'ýn Y dönüþünü al ve sweep çubuðuna uygula
        Vector3 rotation = transform.eulerAngles;
        rotation.z = rs.transform.eulerAngles.y;  // Z ekseni UI için döner
        transform.eulerAngles = rotation;
    }
}
