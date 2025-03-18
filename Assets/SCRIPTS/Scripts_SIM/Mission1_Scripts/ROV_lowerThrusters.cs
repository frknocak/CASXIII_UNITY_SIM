using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROV_lowerThrusters : MonoBehaviour
{
    public GameObject thruster1;
    public GameObject thruster2;
    public GameObject thruster3;
    public GameObject thruster4;
    public GameObject rov;
    public Color workingColor = new Color(174, 150, 1, 1);
    public Color notWorkingColor = new Color(0, 0, 0, 1);

    public float movementSpeed;
    /*float headData = 127f;
    float depthData = 127f;
    float xAxisData = 127f;
    float yAxisData = 127f;
    float[] values = new float[4];*/ // axis verilerini taklit etmeye çalıştım ama başarılı bir array üretme sıklığı üretmek uzun sürecek
    void Start()
    {


        workingColor = new Color(174, 150, 1, 1);
        notWorkingColor = new Color(0, 0, 0, 1);
    }

    void FixedUpdate()
    {
        // ArrayCreator();

        RightAndLeft();
        BackAndForward();

        HeadRotation();
        TiltingUpAndDown();
    }

    void BackAndForward()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {


            thruster2.GetComponent<Renderer>().material.color = workingColor;
            thruster4.GetComponent<Renderer>().material.color = workingColor;
            thruster1.GetComponent<Renderer>().material.color = workingColor;
            thruster3.GetComponent<Renderer>().material.color = workingColor;
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody>().AddForce(-transform.forward * movementSpeed, ForceMode.Force);
            }
        }

    }
    void RightAndLeft()
    {
        if (Input.GetKey(KeyCode.D))
        {

            thruster2.GetComponent<Renderer>().material.color = workingColor;
            thruster4.GetComponent<Renderer>().material.color = workingColor;

            thruster2.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed, ForceMode.Force);
            thruster4.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed, ForceMode.Force);

        }
        else
        {
            thruster2.GetComponent<Renderer>().material.color = notWorkingColor;
            thruster4.GetComponent<Renderer>().material.color = notWorkingColor;
        }


        if (Input.GetKey(KeyCode.A))
        {


            thruster1.GetComponent<Renderer>().material.color = workingColor;
            thruster3.GetComponent<Renderer>().material.color = workingColor;

            thruster1.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed, ForceMode.Force);
            thruster3.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed, ForceMode.Force);
        }
        else
        {
            thruster1.GetComponent<Renderer>().material.color = notWorkingColor;
            thruster3.GetComponent<Renderer>().material.color = notWorkingColor;
        }

    }

    void HeadRotation()
    {
        if (Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.C))
        {


            thruster2.GetComponent<Renderer>().material.color = workingColor;
            thruster4.GetComponent<Renderer>().material.color = workingColor;
            thruster1.GetComponent<Renderer>().material.color = workingColor;
            thruster3.GetComponent<Renderer>().material.color = workingColor;
            if (Input.GetKey(KeyCode.C))
            {
                thruster1.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed / 10, ForceMode.Force);
                thruster2.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed / 10, ForceMode.Force);
                thruster3.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed / 10, ForceMode.Force);
                thruster4.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed / 10, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.V))
            {
                thruster1.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed / 10, ForceMode.Force);
                thruster2.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed / 10, ForceMode.Force);
                thruster3.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed / 10, ForceMode.Force);
                thruster4.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed / 10, ForceMode.Force);
            }
        }




    }
    //aslında aşağı yukarı bakma üst thruster'ıun işi ancak simülasyonda inan fark etmiyor.
    void TiltingUpAndDown()
    {
        Rigidbody rovRb = rov.GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X))
        {

            rovRb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
            thruster2.GetComponent<Renderer>().material.color = workingColor;
            thruster4.GetComponent<Renderer>().material.color = workingColor;
            thruster1.GetComponent<Renderer>().material.color = workingColor;
            thruster3.GetComponent<Renderer>().material.color = workingColor;
            if (Input.GetKey(KeyCode.Z))
            {
                thruster1.GetComponent<Rigidbody>().AddForce(-transform.up * movementSpeed / 10, ForceMode.Force);
                thruster2.GetComponent<Rigidbody>().AddForce(-transform.up * movementSpeed / 10, ForceMode.Force);
                thruster3.GetComponent<Rigidbody>().AddForce(transform.up * movementSpeed / 10, ForceMode.Force);
                thruster4.GetComponent<Rigidbody>().AddForce(transform.up * movementSpeed / 10, ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.X))
            {
                thruster1.GetComponent<Rigidbody>().AddForce(transform.up * movementSpeed / 10, ForceMode.Force);
                thruster2.GetComponent<Rigidbody>().AddForce(transform.up * movementSpeed / 10, ForceMode.Force);
                thruster3.GetComponent<Rigidbody>().AddForce(-transform.up * movementSpeed / 10, ForceMode.Force);
                thruster4.GetComponent<Rigidbody>().AddForce(-transform.up * movementSpeed / 10, ForceMode.Force);
            }
        }
        else
        {
            rovRb.constraints |= RigidbodyConstraints.FreezeRotationX;
        }
    }
    /* public void ArrayCreator(){
         float convertingSpeed = 1000f;
         if (Input.GetKey(KeyCode.A))
         {
             // When "C" key is pressed, increase the value towards 255
             xAxisData = Mathf.MoveTowards(xAxisData, 255, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             xAxisData = 127f;

         }
          if (Input.GetKey(KeyCode.D))
         {
             // When "C" key is pressed, increase the value towards 255
             xAxisData = Mathf.MoveTowards(xAxisData, 0, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             xAxisData = 127f;

         }

         if (Input.GetKey(KeyCode.W))
         {
             // When "C" key is pressed, increase the value towards 255
             yAxisData = Mathf.MoveTowards(yAxisData, 255, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             yAxisData = 127f;

         }
          if (Input.GetKey(KeyCode.S))
         {
             // When "C" key is pressed, increase the value towards 255
             yAxisData = Mathf.MoveTowards(yAxisData, 0, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             yAxisData = 127f;

         }

          if (Input.GetKey(KeyCode.C))
         {
             // When "C" key is pressed, increase the value towards 255
             headData = Mathf.MoveTowards(headData, 255, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             headData = 127f;


         }
          if (Input.GetKey(KeyCode.V))
         {
             // When "C" key is pressed, increase the value towards 255
             headData = Mathf.MoveTowards(headData, 0, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             headData = 127f;


         }


         if (Input.GetKey(KeyCode.Q))
         {
             // When "C" key is pressed, increase the value towards 255
             depthData = Mathf.MoveTowards(depthData, 255, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             depthData = 127f;

         }
          if (Input.GetKey(KeyCode.E))
         {
             // When "C" key is pressed, increase the value towards 255
             depthData = Mathf.MoveTowards(depthData, 0, Time.deltaTime * convertingSpeed); // Adjust the speed as needed
         }else{
             depthData = 127f;

         }

         values[0] = xAxisData;
         values[1] = yAxisData;
         values[2] = headData;
         values[3] = depthData; 
         string arrayString = "[" + string.Join(", ", values) + "]";
         Debug.Log("values: " + arrayString);


     }*/
}
