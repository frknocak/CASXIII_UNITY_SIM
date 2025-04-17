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
    private float head_val; // Yeni eklenen de�er
    private float current_depth;
    private float calculated_depth;


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
        head_val = HeadValueForPyhtonCoordinates(); // Head de�eri i�in hesaplama fonksiyonunu �a��r

        current_depth = rov.transform.position.y;
        calculated_depth = Map(current_depth, 0f, 20f, -28f, -0.6f);

        string message = string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}",
            x_val, y_val, head_val, calculated_depth);
        byte[] data = Encoding.UTF8.GetBytes(message);

        udpClient.Send(data, data.Length, targetEndPoint);
        Debug.Log($"G�nderilen Pozisyon: x = {x_val}, y = {y_val}, head = {head_val} , h= {calculated_depth}");
    }

    private float HeadValueForPyhtonCoordinates()
    {
        float simAngle = rov.transform.rotation.eulerAngles.y;
        float pyAngle = (360 - simAngle + 90) % 360;
        return pyAngle;
    }

    private float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (20-(value - outputMin) * (inputMax / 27.4f));
    }

    private void OnApplicationQuit()
    {
        udpClient.Close(); // Uygulama kapan�nca ba�lant�y� kapat
        StopCoroutine(SendTargetPositionCoroutine()); // Coroutine'i durdur
    }
}
