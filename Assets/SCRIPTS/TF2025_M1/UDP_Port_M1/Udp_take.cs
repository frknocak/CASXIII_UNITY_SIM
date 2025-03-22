using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using UnityEngine;

public class Udp_take : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isReceiving = true;
    private short[] receivedShorts = new short[6];  // 6 elemanl� short array
    public Udp_port_m1 m1;

    void Start()
    {
        StartUDPListener(12345);  // UDP portunu ba�lat

    }

    void Update()
    {
        // Al�nan verileri ekrana yazd�r
        if (receivedShorts != null)
        {
            Debug.Log($"Al�nan Short De�erler: {string.Join(", ", receivedShorts)}");
            m1.x = receivedShorts[0];
            m1.y = receivedShorts[1];
            m1.z = receivedShorts[2];
            m1.h = receivedShorts[3];
            m1.r = receivedShorts[4];
            m1.k = receivedShorts[5];
        }
    }

    private void StartUDPListener(int port)
    {
        udpClient = new UdpClient(port);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;  // Thread'in arka planda �al��mas�n� sa�lar
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

                if (receivedBytes.Length == 12) // 6 * 2 byte (short)
                {
                    short[] tempArray = new short[6];
                    for (int i = 0; i < 6; i++)
                    {
                        tempArray[i] = BitConverter.ToInt16(receivedBytes, i * 2);
                    }

                    // Unity thread'inde de�i�keni g�ncelle
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
            catch (Exception e)
            {
                Debug.LogError("UDP Al�m Hatas�: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        isReceiving = false;
        receiveThread?.Abort();
        udpClient?.Close();
    }
}
