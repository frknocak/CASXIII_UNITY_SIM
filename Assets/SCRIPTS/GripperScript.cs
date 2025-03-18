using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripperScript : MonoBehaviour
{
    [SerializeField] private  GameObject gripperMainServo;
    [SerializeField] private  GameObject gripperRightHandServo;
    [SerializeField] private  GameObject gripperLeftHandServo;
    [SerializeField] private float rotationSpeed = 30; 

   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L)){
            gripperMainServo.transform.Rotate(new Vector3(0f,0f,rotationSpeed)* Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.P)){
            gripperMainServo.transform.Rotate(new Vector3(0f,0f,-rotationSpeed)* Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.J)){
            gripperRightHandServo.transform.Rotate(new Vector3(0f,-rotationSpeed,0f)  * Time.deltaTime);
            gripperLeftHandServo.transform.Rotate(new Vector3(0f,-rotationSpeed,0f)  * Time.deltaTime);

        }
        if(Input.GetKey(KeyCode.K)){
            gripperRightHandServo.transform.Rotate(new Vector3(0f,rotationSpeed,0f)  * Time.deltaTime);
            gripperLeftHandServo.transform.Rotate(new Vector3(0f,rotationSpeed,0f)  * Time.deltaTime);
        }
    }
}
