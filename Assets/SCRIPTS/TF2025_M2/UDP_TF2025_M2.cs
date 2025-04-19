using System.Net.Sockets;
using System.Threading;
using System.Net;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class UDP_TF2025_M2 : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isReceiving = false;
    private short[] receivedShorts = new short[6]; //6 elemanlı short array
    private DateTime lastReceivedTime;
    private float timeoutSeconds = 0.8f;

    public ROV_lowerThrusters rov_LowerThrusters;
    public GameObject ROV;
    public GameObject thruster1;
    public GameObject thruster2;
    public GameObject thruster3;
    public GameObject thruster4;
    public float movementSpeed;

    private Vector3 translationAmount;
    private float rotationAmount = 1f; //Her bir veride kaç derece dönecek

    void Start()
    {
        StartUDPListener(12345); // UDP portunu başlat
        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); // Hareket miktarı
    }

    void FixedUpdate()
    {
        if ((DateTime.Now - lastReceivedTime).TotalSeconds > timeoutSeconds)
        {
            // Zaman aşımı oldu, hareketi durdur
            Debug.LogWarning("UDP veri zaman aşımına uğradı. ROV hareketi durduruluyor.");
            return;
        }
        // Alinan verileri ekrana yazdýr
        if (receivedShorts != null)
        {
            Debug.Log($"Alinan Short Değerler: {string.Join(", ", receivedShorts)}");
            HandleMovement();
        }

    }

    private void StartUDPListener(int port)
    {
        if (udpClient != null)
        {
            CloseUDPListener(); // Eğer eski bağlantıyı açıksa kapat
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
                Debug.Log($"UDP Veri Alindi, {receivedBytes.Length} byte");

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
                        lastReceivedTime = DateTime.Now;
                    }
                }
                else
                {
                    Debug.LogWarning("Hatali veri boyutu!");
                }
            }
            catch (SocketException e)
            {
                Debug.LogError("UDP Alim Hatasi: " + e.Message);
                isReceiving = false;
            }
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


        ROV.transform.Translate(Vector3.forward * Time.deltaTime * y / 9.5f);
        ROV.transform.Translate(Vector3.right * Time.deltaTime * x / 20);

        rotationAmount = h / 50;
        ROV.transform.Rotate(Vector3.up, rotationAmount);

        if (r == 1)
        {
            float target_depth = k / 10;

            float tolerance = 0.05f; // Tolerans deðeri
            float simDepth = Map(target_depth, 0f, 20f, -37f, -0.6f);


            if (Mathf.Abs(ROV.transform.position.y - simDepth) > tolerance)
            {
                if (ROV.transform.position.y > simDepth && z > 0f)
                {
                    translationAmount = new Vector3(0.0f, (-1 * z) / 2000, 0.0f);
                    ROV.transform.Translate(translationAmount);
                }
                else if (ROV.transform.position.y < simDepth && z < 0f)
                {
                    translationAmount = new Vector3(0.0f, (-1 * z) / 2000, 0.0f);
                    ROV.transform.Translate(translationAmount);
                }
                else
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
    private float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (inputMax - value) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
    }
}

