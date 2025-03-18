//using System;
//using System.Globalization;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;
//public class Udp_LocSender : MonoBehaviour
//{
//    public GameObject startingBuoy;
//    public GameObject targetBuoy;
//    private UdpClient udpClient;
//    private IPEndPoint targetEndPoint;

//    private void Start()
//    {
//        udpClient = new UdpClient();
//        targetEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 54321); // Python'a göndereceðimiz IP ve port
//        InvokeRepeating("SendTargetPosition", 0f, 0.1f); // 0.1 saniyede bir veri yolla
//    }

//    private void SendTargetPosition()
//    {
//        float[] targetPos = LocSender();

//        if (targetPos.Length < 2)
//        {
//            Debug.LogWarning("Warning: targetPos array has missing values, skipping send...");
//            return;
//        }

//        // KÜLTÜR AYARINI ZORUNLU NOKTA (.) OLACAK ÞEKÝLDE BELÝRLE
//        string message = string.Format(CultureInfo.InvariantCulture, "{0},{1}", targetPos[0], targetPos[1]);
//        byte[] data = Encoding.UTF8.GetBytes(message);

//        udpClient.Send(data, data.Length, targetEndPoint);
//        Debug.Log($"Sent targetPos: {message}");
//    }

//    private float[] LocSender()
//    {
//        float x_val = targetBuoy.transform.position.x - startingBuoy.transform.position.x;
//        float y_val = targetBuoy.transform.position.z - startingBuoy.transform.position.z; // Y ekseni yerine Z kullanýlýyor
//        Debug.Log($"Gönderilen pozisyon: {{{x_val}, {y_val}}}");

//        return new float[] { x_val, y_val };
//    }

//    private void OnApplicationQuit()
//    {
//        udpClient.Close(); // Uygulama kapanýnca baðlantýyý kapat
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
    public GameObject startingBuoy;
    public GameObject targetBuoy;
    private UdpClient udpClient;
    private IPEndPoint targetEndPoint;
    public GameObject rov;
    public float sendInterval = 0.1f; // Veri gönderme aralýðý

    private void Start()
    {
        udpClient = new UdpClient();
        targetEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 54321); // Python'a göndereceðimiz IP ve port
        StartCoroutine(SendTargetPositionCoroutine());
    }

    private IEnumerator SendTargetPositionCoroutine()
    {
        while (true) // Sürekli çalýþacak bir döngü
        {
            SendTargetPosition();
            yield return new WaitForSeconds(sendInterval); // Belirtilen aralýkta bekle
        }
    }

    private void SendTargetPosition()
    {
        float[] targetPos = LocSender();

        if (targetPos.Length < 2)
        {
            Debug.LogWarning("Warning: targetPos array has missing values, skipping send...");
            return;
        }

        // KÜLTÜR AYARINI ZORUNLU NOKTA (.) OLACAK ÞEKÝLDE BELÝRLE
        string message = string.Format(CultureInfo.InvariantCulture, "{0},{1}",
            targetPos[0], targetPos[1]);
        byte[] data = Encoding.UTF8.GetBytes(message);

        udpClient.Send(data, data.Length, targetEndPoint);
        Debug.Log($"Sent targetPos: {message}");
    }

    private float[] LocSender()
    {
        float x_val = targetBuoy.transform.position.x - startingBuoy.transform.position.x;
        float y_val = targetBuoy.transform.position.z - startingBuoy.transform.position.z; // Y ekseni yerine Z kullanýlýyor

        Debug.Log($"Gönderilen pozisyon: {{{x_val}, {y_val}}}");

        return new float[] { x_val, y_val };
    }

    private void OnApplicationQuit()
    {
        udpClient.Close(); // Uygulama kapanýnca baðlantýyý kapat
        StopCoroutine(SendTargetPositionCoroutine()); // Coroutine'i durdur
    }
}
