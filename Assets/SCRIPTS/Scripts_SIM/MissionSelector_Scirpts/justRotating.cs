using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justRotating : MonoBehaviour
{   
    public float rotationSpeed= 10;
   
    void Update()
    {
        transform.Rotate(new Vector3(0,rotationSpeed,0)*Time.deltaTime,Space.World);
    }
}
