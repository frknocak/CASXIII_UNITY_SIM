using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper : MonoBehaviour
{
    public GameObject leftGripper;
    public GameObject rightGripper;
    public GameObject gripperRotator;
    public float rotationSpeed = 30f;
    public int gripperCollCheckBool;
    public bool gripperCheck = true;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gripperCollCheckBool = PlayerPrefs.GetInt("gripperCollCheckBool");
        //Debug.Log(gripperCollCheckBool);
    }
    private void FixedUpdate()
    {
        if (gripperCollCheckBool == 1 )
        {
            gripperCheck = false;

        }
        else
        {
            gripperCheck = true;
        }

        if (gripperCheck == true)
        {

            if (Input.GetKey(KeyCode.J))
            {
                leftGripper.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
                rightGripper.transform.Rotate(new Vector3(0, -rotationSpeed, 0) * Time.deltaTime);

            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            leftGripper.transform.Rotate(new Vector3(0, -rotationSpeed, 0) * Time.deltaTime);
            rightGripper.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
            

        }
        if (Input.GetKey(KeyCode.L))
        {
            gripperRotator.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            gripperRotator.transform.Rotate(new Vector3(0, 0, -rotationSpeed) * Time.deltaTime);
        }

    }

}
