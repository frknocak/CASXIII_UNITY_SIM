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
        float currentY = ROV.transform.eulerAngles.y;

        float shortestAngle = Mathf.DeltaAngle(currentY, target_rotation);

        float newY;

        if (Mathf.Abs(shortestAngle) < (rotationSpeed*Time.deltaTime))
        {
            newY = target_rotation; // hedefe ulaþtý
        }
        else
        {
            newY = currentY + Mathf.Sign(shortestAngle) * (rotationSpeed * Time.deltaTime); // saða mý sola mý?
        }

        ROV.transform.rotation = Quaternion.Euler(0, newY, 0);
    }
}




