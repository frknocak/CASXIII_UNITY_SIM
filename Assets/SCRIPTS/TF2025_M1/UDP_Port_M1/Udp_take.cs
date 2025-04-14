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
//    private short[] receivedShorts = new short[6];  // 6 elemanlý short array

//    public GameObject ROV;
//    public float movementSpeed;
//    private Vector3 translationAmount;
//    private float rotationAmount = 1f; // Her bir veride kaç derece dönecek

//    void Start()
//    {
//        StartUDPListener(12345);  // UDP portunu baþlat
//        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); // Hareket miktarý
//    }

//    void Update()
//    {
//        // Alýnan verileri ekrana yazdýr
//        if (receivedShorts != null)
//        {
//            Debug.Log($"Alýnan Short Deðerler: {string.Join(", ", receivedShorts)}");
//            HandleMovement();
//        }
//    }

//    private void StartUDPListener(int port)
//    {
//        udpClient = new UdpClient(port);
//        receiveThread = new Thread(ReceiveData);
//        receiveThread.IsBackground = true;  // Thread'in arka planda çalýþmasýný saðlar
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
//                Debug.Log($"UDP Veri Alýndý, {receivedBytes.Length} byte");

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
//                    Debug.LogWarning("Hatalý veri boyutu!");
//                }
//            }
//            catch (Exception e)
//            {
//                Debug.LogError("UDP Alým Hatasý: " + e.Message);
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

public class Udp_take : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isReceiving = false;
    private short[] receivedShorts = new short[8]; // 8 elemanlý short array

    public GameObject ROV;
    public float movementSpeed;
    private Vector3 translationAmount;
    private float rotationAmount = 1f; // Her bir veride kaç derece dönecek

    void Start()
    {
        StartUDPListener(12345); // UDP portunu baþlat
        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); // Hareket miktarý
    }

    void Update()
    {
        // Alýnan verileri ekrana yazdýr
        if (receivedShorts != null)
        {
            Debug.Log($"Alýnan Short Deðerler: {string.Join(", ", receivedShorts)}");
            HandleMovement();
        }
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

                if (receivedBytes.Length == 16) // 8 * 2 byte (short)
                {
                    short[] tempArray = new short[7];
                    for (int i = 0; i < 8; i++)
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
                    ROV.transform.position.z
                );
            }
        }
        else
        {
            translationAmount = new Vector3(0.0f, z / 2000, 0.0f);
            ROV.transform.Translate(translationAmount);
        }

        //if (rc == 1)
        //{
        //    float target_rotation = rc;
        //    float tolerance = 0.05f;


        //}
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
