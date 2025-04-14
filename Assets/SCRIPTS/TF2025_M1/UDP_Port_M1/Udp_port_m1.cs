using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System;

public class Udp_port_m1 : MonoBehaviour
{
    public ROV_lowerThrusters_M1 rov_LowerThrusters;
    public float movementSpeed;
    public GameObject thruster1;
    public GameObject thruster2;
    public GameObject thruster3;
    public GameObject thruster4;
    public GameObject ROV;

    [SerializeField] public short x;
    [SerializeField] public short y;
    [SerializeField] public short z;
    [SerializeField] public short h;
    [SerializeField] public short r;
    [SerializeField] public short k;
    [SerializeField] public short rc; //rotate sender control
    [SerializeField] public short tr; //target rotation

    public Vector3 translationAmount;
    private float rotationAmount = 1f; //Her bir veride kaç derece dönecek
    private UdpClient udpListener;
    private IPEndPoint udpEndPoint;
    // yorum satýrlarý gerçekçi hareket algýsý üzerine hazýrlandýðý için UDP kontrolde PID gerektiriyor.
    private void Start()
    {
        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); //Her bir veride ne kadar yukarý çýkacak
        int udpPort = 12345;
        udpListener = new UdpClient(udpPort); // Port to listen on
        udpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        PlayerPrefs.SetInt("udpPort", udpPort);

        Debug.Log("UDP server is listening...");
    }

    float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
    }

    private void FixedUpdate()
    {
        movementSpeed = rov_LowerThrusters.movementSpeed;
        if (udpListener.Available > 0)
        {
            byte[] data = udpListener.Receive(ref udpEndPoint);
            HandleMessage(data);
        }
    }

    private void HandleMessage(byte[] ints)
    {

        Debug.Log(string.Format("byte: {0} {1} {2} {3} {4} {5} {6} {7} \n", ints[0], ints[1], ints[2], ints[3], ints[4], ints[5], ints[6], ints[7]));

        ROV.transform.Translate(Vector3.forward * Time.deltaTime * y / 9.5f);
        ROV.transform.Translate(Vector3.right * Time.deltaTime * x / 20);
        rotationAmount = h / 50;
        ROV.transform.Rotate(Vector3.up, rotationAmount);

        if (r == 1)
        {
            float target_depth = k / 100;
            float tolerance = 0.05f;
            float simDepth = Map(target_depth, 0f, 20f, -26f, -0.6f);

            if (Mathf.Abs(ROV.transform.position.y - simDepth) > tolerance)
            {
                if (ROV.transform.position.y > simDepth && z > 0f)
                {
                    translationAmount = new Vector3(0.0f, (-1 * z) / 2000, 0.0f);
                    ROV.transform.Translate(translationAmount);
                }
                else if (ROV.transform.position.y > simDepth && z <= 0f)
                {
                    translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
                    ROV.transform.Translate(translationAmount);
                }
                else if (ROV.transform.position.y < simDepth && z < 0f)
                {
                    translationAmount = new Vector3(0.0f, (-1 * z) / 2000, 0.0f);
                    ROV.transform.Translate(translationAmount);
                }
                else if (ROV.transform.position.y < simDepth && z >= 0f)
                {
                    translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
                    ROV.transform.Translate(translationAmount);
                }
            }
            else
            {
                ROV.transform.position = new Vector3(
                    ROV.transform.position.x,
                    simDepth,
                    ROV.transform.position.z
                );
            }
        }
        else
        {
            translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
            ROV.transform.Translate(translationAmount);
        }
    }
    private void OnDestroy()
    {
        udpListener.Close();
    }
 }
