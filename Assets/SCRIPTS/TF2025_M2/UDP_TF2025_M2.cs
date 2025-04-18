using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;


using System.Net;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class UDP_TF2025_M2 : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isReceiving = false;
    private short[] receivedShorts = new short[6];

    public ROV_lowerThrusters rov_LowerThrusters;
    public GameObject ROV;
    public GameObject thruster1;
    public GameObject thruster2;
    public GameObject thruster3;
    public GameObject thruster4;
    public float movementSpeed;

    public Vector3 translationAmount;
    private float rotationAmount = 1f; //Her bir veride kaç derece dönecek

    void Start()
    {
        StartUDPListener(12345);
        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); //Her bir veride ne kadar yukarý çýkacak

    }

    private void StartUDPListener(int port)
    {
        if (udpClient != null)
        {
            CloseUDPListener(); // Eðer eski baðlantý açýksa kapat
        }

        udpClient = new UdpClient(port);
        isReceiving = true;

        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 12345);

        while (isReceiving)
        {
            try
            {
                byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
                Debug.Log($"UDP Veri Alýndý, {receivedBytes.Length} byte");

                if (receivedBytes.Length == 12) // 6 * 2 byte (short)
                {
                    short[] tempArray = new short[6];
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        tempArray[i] = BitConverter.ToInt16(receivedBytes, i * 2);
                    }

                    lock (receivedShorts)
                    {
                        receivedShorts = tempArray;
                    }
                }
                else
                {
                    Debug.LogWarning("Hatalý veri boyutu!");
                }
            }
            catch (SocketException e)
            {
                Debug.LogError("UDP Alým Hatasý: " + e.Message);
                isReceiving = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // Alýnan verileri ekrana yazdýr
        if (receivedShorts != null)
        {
            Debug.Log($"Alýnan Short Deðerler: {string.Join(", ", receivedShorts)}");
            HandleMovement();

        }
    }
    private void HandleMovement()
    {
        float x = receivedShorts[0];
        float y = receivedShorts[1];
        float z = receivedShorts[2];
        float h = receivedShorts[3];
        float r = receivedShorts[4];
        float k = receivedShorts[5];


        ROV.transform.Translate(Vector3.forward * Time.deltaTime * y / 9.5f);
        ROV.transform.Translate(Vector3.right * Time.deltaTime * x / 20);

        rotationAmount = h / 50;
        ROV.transform.Rotate(Vector3.up, rotationAmount);

        if (r == 1)
        {
            float target_depth = k / 10;
            float tolerance = 0.05f; // Tolerans deðeri
            float simDepth = Map(target_depth, 0f, 20f, -28f, -0.6f);

            if (Mathf.Abs(ROV.transform.position.y - simDepth) > tolerance)
            {
                // Derinlik farký toleransýn üzerindeyse hareket et
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
                // Derinlik farký tolerans içindeyse sadece Y eksenini sabitle
                ROV.transform.position = new Vector3(
                    ROV.transform.position.x,  // X ekseninde hareket serbest
                    simDepth,                  // Y ekseni sabitleniyor
                    ROV.transform.position.z   // Z ekseninde hareket serbest
                );
            }
        }
        else
        {
            translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
            ROV.transform.Translate(translationAmount);
        }

    }

    private void CloseUDPListener()
    {
        isReceiving = false;

        if (udpClient != null)
        {
            udpClient.Close();
            udpClient = null;
        }

        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
            receiveThread = null;
        }
    }

    private void OnDestroy()
    {
        CloseUDPListener();
    }

    float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
    }
}