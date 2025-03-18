using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSourceMovement : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Speed of the rotation
    private Vector3 initialRotation = new Vector3(60f, 0f, 0f); // Initial rotation offset

    void Start()
    {
        // Apply the initial rotation offset
        transform.rotation = Quaternion.Euler(initialRotation);
    }

    void Update()
    {
        // Calculate the rotation angles
        float rotationAngleX = Mathf.Sin(Time.time) * rotationSpeed;
        float rotationAngleY = Mathf.Cos(Time.time) * rotationSpeed;

        // Create a new rotation quaternion based on the calculated angles
        Quaternion newRotation = Quaternion.Euler(initialRotation.x + rotationAngleX, initialRotation.y + rotationAngleY, initialRotation.z);

        // Apply the new rotation to the object
        transform.rotation = newRotation;
    }
}
