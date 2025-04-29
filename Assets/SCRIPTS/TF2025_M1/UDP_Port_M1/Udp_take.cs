using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using UnityEngine;

public class Udp_take : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isReceiving = false;
    private short[] receivedShorts = new short[8]; // 8 elemanlı short array
    private DateTime lastReceivedTime;
    private float timeoutSeconds = 0.8f;

    public GameObject ROV;
    public float movementSpeed;
    private Vector3 translationAmount;
    private float rotationAmount = 1f; // Her bir veride kaç derece dönecek

    public float rotationSpeed = 45.0f;                                             /*This part for the rotation controller function*/
    public float rotation_angle = 0f; // We will get this variable from Python side             
    private float target_rotation = 0f;
    private bool rotating = false;
    private float prevRc = 0f;


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
            prevRc = 0f;
            return;
        }
        // Alınan verileri ekrana yazdır
        if (receivedShorts != null)
        {
            Debug.Log($"Alınan Short Değerler: {string.Join(", ", receivedShorts)}");
            HandleMovement();

        }

    }


    private void StartUDPListener(int port)
    {
        if (udpClient != null)
        {
            CloseUDPListener(); // Eğer eski bağlantı açıksa kapat
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
                Debug.Log($"UDP Veri Alındı, {receivedBytes.Length} byte");

                if (receivedBytes.Length == 16) // 8 * 2 byte (short)
                {
                    short[] tempArray = new short[8];
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
                    Debug.LogWarning("Hatalı veri boyutu!");
                }
            }
            catch (SocketException e)
            {
                Debug.LogError("UDP Alım Hatası: " + e.Message);
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
        float rc = receivedShorts[6]; // for rotation controller
        float tr = receivedShorts[7]; // for target rotation

        ROV.transform.Translate(Vector3.forward * Time.deltaTime * y / 9.5f);
        ROV.transform.Translate(Vector3.right * Time.deltaTime * x / 20);
        rotationAmount = h / 50;
        ROV.transform.Rotate(Vector3.up, rotationAmount);


        if (r == 1)
        {
            float target_depth = k / 10;
            float tolerance = 0.05f;
            float simDepth = Map(target_depth, 0f, 20f, -28f, -0.6f);

            if (Mathf.Abs(ROV.transform.position.y - simDepth) > tolerance)
            {
                // Derinlik farkı toleransın üzerindeyse hareket et
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
                // Derinlik farkı tolerans içindeyse sadece Y eksenini sabitle
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

        if (rc == 1 && prevRc != 1)
        {
            float currentY = ROV.transform.eulerAngles.y;

            if (!rotating && tr != 0f)
            {
                rotation_angle = -tr;
                target_rotation = (currentY + rotation_angle) % 360;
                if (target_rotation < 0f)
                {
                    target_rotation += 360f;
                }
                rotating = true;
                prevRc = rc;
            }
        }
        if (rotating == true)
        {
            float currentY = ROV.transform.eulerAngles.y;
            float shortestAngle = Mathf.DeltaAngle(currentY, target_rotation);
            float rotation_step = rotationSpeed * Time.deltaTime;


            float newY;

            if (Mathf.Abs(shortestAngle) <= rotation_step || Mathf.Abs(shortestAngle) <= 1f)
            {
                newY = target_rotation;
                ROV.transform.rotation = Quaternion.Euler(0, newY, 0);
                rotating = false;
                rotation_angle = 0f;

                prevRc = rc;

            }
            else
            {
                newY = currentY + Mathf.Sign(shortestAngle) * rotation_step;
                ROV.transform.rotation = Quaternion.Euler(0, newY, 0);

            }
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

