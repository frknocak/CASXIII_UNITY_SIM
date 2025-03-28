using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCompass : MonoBehaviour
{
    public GameObject ROV;
    private float simAngle;
    private float triAngle;
    private float tolerans;
    private float outputAngle;
    void FixedUpdate()
    {
        outputAngle = RotateHeadAboutDegree();
        Debug.Log($"Angle is: {outputAngle}");
    }

    public float RotateHeadAboutDegree()
    {
        // Quaternion yerine Euler açýlarýný kullan
        simAngle = ROV.transform.rotation.eulerAngles.y;

        triAngle = 90 - simAngle + 360;

        if(triAngle > 360)
        {
            tolerans = triAngle - 360;
            triAngle = tolerans;
        }
     return triAngle;
    }
}
