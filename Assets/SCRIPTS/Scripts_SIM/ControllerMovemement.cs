using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

public class ControllerMovemement : MonoBehaviour
{
    [SerializeField]
    public JoystickController joystickController;
    public ROV_lowerThrusters rOV_LowerThrusters;
    public Vector2 _rightJoystick;
    public Vector2 _leftJoystick;

    public GameObject thruster1;
    public GameObject thruster2;
    public GameObject thruster3;
    public GameObject thruster4;

    public float movementSpeed;


    void Awake()
    {
        movementSpeed = rOV_LowerThrusters.movementSpeed * 2;

        joystickController = new JoystickController();

        joystickController.ZimaControls.RightJoystick.performed += ctx => _rightJoystick = ctx.ReadValue<Vector2>();
        joystickController.ZimaControls.RightJoystick.canceled += ctx => _rightJoystick = Vector2.zero;
        joystickController.ZimaControls.LeftJoystick.performed += ctx => _leftJoystick = ctx.ReadValue<Vector2>();
        joystickController.ZimaControls.LeftJoystick.canceled += ctx => _leftJoystick = Vector2.zero;


    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LeftJoystickFunc();
        RightJoystickFunc();
    }

    void LeftJoystickFunc()
    {
        // SAĞ SOL HAREKET
        thruster1.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed * _leftJoystick.x, ForceMode.Force);
        thruster2.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed * _leftJoystick.x, ForceMode.Force);
        thruster3.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed * _leftJoystick.x, ForceMode.Force);
        thruster4.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed * _leftJoystick.x, ForceMode.Force);


        //İLERİ YÖNDE İLERLEME
        thruster1.transform.GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * _leftJoystick.y, ForceMode.Force);
        thruster2.transform.GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * _leftJoystick.y, ForceMode.Force);
        thruster3.transform.GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * _leftJoystick.y, ForceMode.Force);
        thruster4.transform.GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * _leftJoystick.y, ForceMode.Force);

    }
    void RightJoystickFunc()
    {

        // AŞAĞI YUKARI HAREKET
        thruster1.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, movementSpeed * _rightJoystick.y, 0), ForceMode.Force);
        thruster2.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, movementSpeed * _rightJoystick.y, 0), ForceMode.Force);
        thruster3.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, movementSpeed * _rightJoystick.y, 0), ForceMode.Force);
        thruster4.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, movementSpeed * _rightJoystick.y, 0), ForceMode.Force);
        // HEAD HAREKETİ

        if (_rightJoystick.x != 0)
        {
            if (_rightJoystick.x > 0)
            {
                thruster1.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed/10, ForceMode.Force);
                thruster2.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed/10, ForceMode.Force);
                thruster3.transform.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed/10, ForceMode.Force);
                thruster4.transform.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed/10, ForceMode.Force);
            }
            else if (_rightJoystick.x < 0)
            {
                thruster1.transform.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed/10, ForceMode.Force);
                thruster2.transform.GetComponent<Rigidbody>().AddForce(-transform.right * movementSpeed/10, ForceMode.Force);
                thruster3.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed/10, ForceMode.Force);
                thruster4.transform.GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed/10, ForceMode.Force);

            }

        }





    }
    void OnEnable()
    {
        joystickController.ZimaControls.Enable();
    }
}
