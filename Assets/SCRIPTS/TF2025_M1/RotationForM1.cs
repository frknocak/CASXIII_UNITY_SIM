using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationForM1 : MonoBehaviour
{
    public float rotationSpeed = 45.0f;
    public float rotation_angle = 0f; // We will get this variable from Python side
    public GameObject ROV;

    private float target_rotation = 0f;
    private bool rotating = false;

    private void FixedUpdate()
    {
        RotationforPython();
    }
    private void RotationforPython()
    {
        float currentY = ROV.transform.eulerAngles.y;

        if (!rotating && rotation_angle != 0f)
        {
            target_rotation = (currentY + rotation_angle) % 360;
            rotating = true;
        }

        if (rotating == true)
        {
            float shortestAngle = Mathf.DeltaAngle(currentY, target_rotation);
            float rotation_step = rotationSpeed * Time.deltaTime;

            float newY;

            if (Mathf.Abs(shortestAngle) < rotation_step)
            {
                newY = target_rotation;
                rotating = false;
                rotation_angle = 0f;
            }
            else
            {
                newY = currentY + Mathf.Sign(shortestAngle) * rotation_step;
            }

            ROV.transform.rotation = Quaternion.Euler(0, newY, 0);
        }
    }
    float NormalizeAngle(float angle)
    {
        return ((360 - angle + 90) % 360);
    }
}
