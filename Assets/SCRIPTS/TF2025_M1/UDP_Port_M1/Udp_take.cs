//using System.Net.Sockets;
//using System.Net;
//using System.Threading;
//using System;
//using UnityEngine;

//public class Udp_take : MonoBehaviour
//{
//    private UdpClient udpClient;
//    private Thread receiveThread;
//    private bool isReceiving = true;
//    private short[] receivedShorts = new short[6];  // 6 elemanl� short array

//    public GameObject ROV;
//    public float movementSpeed;
//    private Vector3 translationAmount;
//    private float rotationAmount = 1f; // Her bir veride ka� derece d�necek

//    void Start()
//    {
//        StartUDPListener(12345);  // UDP portunu ba�lat
//        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); // Hareket miktar�
//    }

//    void Update()
//    {
//        // Al�nan verileri ekrana yazd�r
//        if (receivedShorts != null)
//        {
//            Debug.Log($"Al�nan Short De�erler: {string.Join(", ", receivedShorts)}");
//            HandleMovement();
//        }
//    }

//    private void StartUDPListener(int port)
//    {
//        udpClient = new UdpClient(port);
//        receiveThread = new Thread(ReceiveData);
//        receiveThread.IsBackground = true;  // Thread'in arka planda �al��mas�n� sa�lar
//        receiveThread.Start();
//    }

//    private void ReceiveData()
//    {
//        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 12345);

//        while (isReceiving)
//        {
//            try
//            {
//                byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
//                Debug.Log($"UDP Veri Al�nd�, {receivedBytes.Length} byte");

//                if (receivedBytes.Length == 12) // 6 * 2 byte (short)
//                {
//                    short[] tempArray = new short[6];
//                    for (int i = 0; i < 6; i++)
//                    {
//                        tempArray[i] = BitConverter.ToInt16(receivedBytes, i * 2);
//                    }

//                    lock (receivedShorts)
//                    {
//                        receivedShorts = tempArray;
//                    }
//                }
//                else
//                {
//                    Debug.LogWarning("Hatal� veri boyutu!");
//                }
//            }
//            catch (Exception e)
//            {
//                Debug.LogError("UDP Al�m Hatas�: " + e.Message);
//            }
//        }
//    }

//    private void HandleMovement()
//    {
//        float x = receivedShorts[0];
//        float y = receivedShorts[1];
//        float z = receivedShorts[2];
//        float h = receivedShorts[3];
//        float r = receivedShorts[4];
//        float k = receivedShorts[5];

//        ROV.transform.Translate(Vector3.forward * Time.deltaTime * y / 9.5f);
//        ROV.transform.Translate(Vector3.right * Time.deltaTime * x / 20);
//        rotationAmount = h / 50;
//        ROV.transform.Rotate(Vector3.up, rotationAmount);

//        if (r == 1)
//        {
//            float target_depth = k / 10;
//            float tolerance = 0.05f;
//            float simDepth = Map(target_depth, 0f, 20f, -24f, -0.6f);

//            if (Mathf.Abs(ROV.transform.position.y - simDepth) > tolerance)
//            {
//                if (ROV.transform.position.y > simDepth && z > 0f)
//                {
//                    translationAmount = new Vector3(0.0f, (-1 * z) / 2000, 0.0f);
//                    ROV.transform.Translate(translationAmount);
//                }
//                else if (ROV.transform.position.y > simDepth && z <= 0f)
//                {
//                    translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
//                    ROV.transform.Translate(translationAmount);
//                }
//                else if (ROV.transform.position.y < simDepth && z < 0f)
//                {
//                    translationAmount = new Vector3(0.0f, (-1 * z) / 2000, 0.0f);
//                    ROV.transform.Translate(translationAmount);
//                }
//                else if (ROV.transform.position.y < simDepth && z >= 0f)
//                {
//                    translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
//                    ROV.transform.Translate(translationAmount);
//                }
//            }
//            else
//            {
//                ROV.transform.position = new Vector3(
//                    ROV.transform.position.x,
//                    simDepth,
//                    ROV.transform.position.z
//                );
//            }
//        }
//        else
//        {
//            translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
//            ROV.transform.Translate(translationAmount);
//        }
//    }

//    private float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
//    {
//        return (inputMax - value) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
//    }


//    void OnApplicationQuit()
//    {
//        isReceiving = false;
//        receiveThread?.Abort();
//        udpClient?.Close();
//    }
//}


using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class Udp_take : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isReceiving = false;
    private short[] receivedShorts = new short[8]; // 8 elemanl� short array

    public GameObject ROV;
    public float movementSpeed;
    private Vector3 translationAmount;
    private float rotationAmount = 1f; // Her bir veride ka� derece d�necek

    public float rotationSpeed = 45.0f;                                             /*This part for the rotation controller function*/
    public float rotation_angle = 0f; // We will get this variable from Python side             
    private float target_rotation = 0f;
    private bool rotating = false;
    void Start()
    {
        StartUDPListener(12345); // UDP portunu ba�lat
        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); // Hareket miktar�
    }

    void FixedUpdate()
    {
        // Al�nan verileri ekrana yazd�r
        if (receivedShorts != null)
        {
            Debug.Log($"Al�nan Short De�erler: {string.Join(", ", receivedShorts)}");
            HandleMovement();
            
        }

    }


    private void StartUDPListener(int port)
    {
        if (udpClient != null)
        {
            CloseUDPListener(); // E�er eski ba�lant� a��ksa kapat
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
                Debug.Log($"UDP Veri Al�nd�, {receivedBytes.Length} byte");

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
                    }
                }
                else
                {
                    Debug.LogWarning("Hatal� veri boyutu!");
                }
            }
            catch (SocketException e)
            {
                Debug.LogError("UDP Al�m Hatas�: " + e.Message);
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
                    ROV.transform.position.z);
            }
        }
        else
        {
            translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
            ROV.transform.Translate(translationAmount);
        }

        if (rc == 1)
        {
            float currentY = ROV.transform.eulerAngles.y;
            if (!rotating && tr != 0f)
            {
                target_rotation = (currentY + tr) % 360;
                rotation_angle = tr;
                rotating = true;
            }
            if (rotating == true)
            {
                float shortestAngle = Mathf.DeltaAngle(currentY, target_rotation);
                float rotation_step = rotationSpeed * Time.deltaTime;

                float newY;

                if (Mathf.Abs(shortestAngle) < rotation_step)
                {
                    newY = target_rotation;
                    rotating = false;
                    rotation_angle = 0f;
                    ROV.transform.rotation = Quaternion.Euler(0, newY, 0);
                }
                else
                {
                    newY = currentY + Mathf.Sign(shortestAngle) * rotation_step;
                    ROV.transform.rotation = Quaternion.Euler(0, newY, 0);
                }
            }
        }
        else
        {
            Debug.Log($" Rotation Controller de�eri: {rc} ");
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

