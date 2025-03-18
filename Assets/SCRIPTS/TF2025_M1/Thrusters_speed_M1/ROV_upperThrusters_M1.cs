using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROV_upperThrusters_M1 : MonoBehaviour
{
    public Color workingColor;
    public Color notWorkingColor;
    public float movementSpeed;

    void Start()
    {
        workingColor = new Color(174, 150, 1, 1);
        notWorkingColor = new Color(0, 0, 0, 1);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            GetComponent<Renderer>().material.color = workingColor;
            if (Input.GetKey(KeyCode.Q))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, movementSpeed, 0), ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.E))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, -movementSpeed, 0), ForceMode.Force);
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = notWorkingColor;
        }



    }
}
