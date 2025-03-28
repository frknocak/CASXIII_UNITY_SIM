//using System;
//using System.Globalization;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;
//using System.Collections;

//public class Udp_LocSender : MonoBehaviour
//{
//    public GameObject rov;
//    public GameObject targetBuoy;
//    private UdpClient udpClient;
//    private IPEndPoint targetEndPoint;
//    public float sendInterval = 0.1f; // Veri g�nderme aral���

//    private float x_val;
//    private float y_val;
//    private bool isPositionSet = false; // Pozisyonun sadece bir kez atanmas�n� kontrol eder
//    private void Start()
//    {
//        udpClient = new UdpClient();
//        targetEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 54321); // Python'a g�nderece�imiz IP ve port
//        SetInitialPosition(); // x_val ve y_val'� bir kez hesaplay�p kaydet
//        StartCoroutine(SendTargetPositionCoroutine());
//    }
//    private void SetInitialPosition()
//    {
//        if (!isPositionSet)
//        {
//            Vector3 origin = rov.transform.position;

//            x_val = targetBuoy.transform.position.x - origin.x;
//            y_val = targetBuoy.transform.position.z - origin.z; // Y ekseni yerine Z kullan�l�yor

//            isPositionSet = true; // Art�k tekrar hesaplanmayacak
//            Debug.Log($"Sabitlenen Pozisyon: x = {x_val}, y = {y_val}");
//        }
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

//        string message = string.Format(CultureInfo.InvariantCulture, "{0},{1}",
//            x_val, y_val);
//        byte[] data = Encoding.UTF8.GetBytes(message);

//        udpClient.Send(data, data.Length, targetEndPoint);
//        Debug.Log($"G�nderilen Pozisyon: x = {x_val}, y = {y_val}");
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
    public float sendInterval = 0.1f; // Veri g�nderme aral���

    private float x_val;
    private float y_val;
    private float z_val; // Yeni eklenen de�er

    private bool isPositionSet = false; // Pozisyonun sadece bir kez atanmas�n� kontrol eder

    private void Start()
    {
        udpClient = new UdpClient();
        targetEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 54321); // Python'a g�nderece�imiz IP ve port
        SetInitialPosition(); // x_val ve y_val'� bir kez hesaplay�p kaydet
        StartCoroutine(SendTargetPositionCoroutine());
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
        z_val = RotateHeadAboutDegree(); // Z ekseni i�in hesaplama fonksiyonunu �a��r

        string message = string.Format(CultureInfo.InvariantCulture, "{0},{1},{2}",
            x_val, y_val, z_val);
        byte[] data = Encoding.UTF8.GetBytes(message);

        udpClient.Send(data, data.Length, targetEndPoint);
        Debug.Log($"G�nderilen Pozisyon: x = {x_val}, y = {y_val}, z = {z_val}");
    }

    private float RotateHeadAboutDegree()
    {
        float simAngle = rov.transform.rotation.eulerAngles.y;
        float triAngle = 90 - simAngle + 360;

        if (triAngle > 360)
        {
            float tolerans = triAngle - 360;
            triAngle = tolerans;
        }
        return triAngle;
    }

    private void OnApplicationQuit()
    {
        udpClient.Close(); // Uygulama kapan�nca ba�lant�y� kapat
        StopCoroutine(SendTargetPositionCoroutine()); // Coroutine'i durdur
    }
}
