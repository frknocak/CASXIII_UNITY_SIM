//using System;
//using System.Globalization;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;
//using System.Collections;

//public class Udp_LocSender : MonoBehaviour
//{
//    public GameObject startingBuoy;
//    public GameObject targetBuoy;
//    private UdpClient udpClient;
//    private IPEndPoint targetEndPoint;
//    public GameObject rov;
//    public float sendInterval = 0.1f; // Veri g�nderme aral���

//    private void Start()
//    {
//        udpClient = new UdpClient();
//        targetEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 54321); // Python'a g�nderece�imiz IP ve port
//        StartCoroutine(SendTargetPositionCoroutine());
//    }

//    private IEnumerator SendTargetPositionCoroutine()
//    {
//        while (true) // S�rekli �al��acak bir d�ng�
//        {
//            SendTargetPosition();
//            yield return new WaitForSeconds(sendInterval); // Belirtilen aral�kta bekle
//        }
//    }

//    private void SendTargetPosition()
//    {
//        float[] targetPos = LocSender();

//        if (targetPos.Length < 2)
//        {
//            Debug.LogWarning("Warning: targetPos array has missing values, skipping send...");
//            return;
//        }

//        // K�LT�R AYARINI ZORUNLU NOKTA (.) OLACAK �EK�LDE BEL�RLE
//        string message = string.Format(CultureInfo.InvariantCulture, "{0},{1}",
//            targetPos[0], targetPos[1]);
//        byte[] data = Encoding.UTF8.GetBytes(message);

//        udpClient.Send(data, data.Length, targetEndPoint);
//        Debug.Log($"Sent targetPos: {message}");
//    }

//    private float[] LocSender()
//    {
//        float x_val = targetBuoy.transform.position.x - startingBuoy.transform.position.x;
//        float y_val = targetBuoy.transform.position.z - startingBuoy.transform.position.z; // Y ekseni yerine Z kullan�l�yor

//        Debug.Log($"G�nderilen pozisyon: {{{x_val}, {y_val}}}");

//        return new float[] { x_val, y_val };
//    }

//    private void OnApplicationQuit()
//    {
//        udpClient.Close(); // Uygulama kapan�nca ba�lant�y� kapat
//        StopCoroutine(SendTargetPositionCoroutine()); // Coroutine'i durdur
//    }
//}


using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections;

public class Udp_LocSender : MonoBehaviour
{
    public GameObject rov;
    public GameObject targetBuoy;
    private UdpClient udpClient;
    private IPEndPoint targetEndPoint;
    private rotateCompass compassRotation;
    public float sendInterval = 0.1f; // Veri g�nderme aral���
    
    private float x_val;
    private float y_val;
    private bool isPositionSet = false; // Pozisyonun sadece bir kez atanmas�n� kontrol eder
    private void Start()
    {
        udpClient = new UdpClient();
        targetEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 54321); // Python'a g�nderece�imiz IP ve port
        SetInitialPosition(); // x_val ve y_val'� bir kez hesaplay�p kaydet
        StartCoroutine(SendTargetPositionCoroutine());
        compassRotation = GetComponent<rotateCompass>();
    }
    private void SetInitialPosition()
    {
        if (!isPositionSet)
        {
            Vector3 origin = rov.transform.position;

            x_val = targetBuoy.transform.position.x - origin.x;
            y_val = targetBuoy.transform.position.z - origin.z; // Y ekseni yerine Z kullan�l�yor

            isPositionSet = true; // Art�k tekrar hesaplanmayacak
            Debug.Log($"Sabitlenen Pozisyon: x = {x_val}, y = {y_val}");
        }
    }

    private IEnumerator SendTargetPositionCoroutine()
    {
        while (true) // S�rekli �al��acak bir d�ng�
        {
            SendTargetPosition();
            yield return new WaitForSeconds(sendInterval); // Belirtilen aral�kta bekle
        }
    }
    private void SendTargetPosition()
    {
        float rov_rotation_y = compassRotation.RotateHeadAboutDegree(); // Do�ru y ekseni d�n�� a��s�n� almak i�in eulerAngles kullan

        string message = string.Format(CultureInfo.InvariantCulture, "{0},{1},{2}",
            x_val, y_val, rov_rotation_y);
        byte[] data = Encoding.UTF8.GetBytes(message);

        udpClient.Send(data, data.Length, targetEndPoint);
        Debug.Log($"G�nderilen Pozisyon: x = {x_val}, y = {y_val}, rotation_y = {rov_rotation_y}");
    }

    private void OnApplicationQuit()
    {
        udpClient.Close(); // Uygulama kapan�nca ba�lant�y� kapat
        StopCoroutine(SendTargetPositionCoroutine()); // Coroutine'i durdur
    }
}

