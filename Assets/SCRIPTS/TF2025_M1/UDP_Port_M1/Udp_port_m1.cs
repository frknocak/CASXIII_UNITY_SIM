using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

public class Udp_port_m1 : MonoBehaviour
{
    public ROV_lowerThrusters_M1 rov_LowerThrusters;
    public float movementSpeed;
    public GameObject thruster1;
    public GameObject thruster2;
    public GameObject thruster3;
    public GameObject thruster4;
    public GameObject ROV;

    public Vector3 translationAmount;
    private float rotationAmount = 1f; //Her bir veride kaç derece dönecek
    private UdpClient udpListener;
    private IPEndPoint udpEndPoint;
    // yorum satýrlarý gerçekçi hareket algýsý üzerine hazýrlandýðý için UDP kontrolde PID gerektiriyor.
    private void Start()
    {
        translationAmount = new Vector3(0.0f, 0.015f, 0.0f); //Her bir veride ne kadar yukarý çýkacak
        int udpPort = 12345;
        udpListener = new UdpClient(udpPort); // Port to listen on
        udpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        PlayerPrefs.SetInt("udpPort", udpPort);

        Debug.Log("UDP server is listening...");
    }

    // kod buðranýn bu fonksiyonu neden yazdý hiçbir fikrim yok.
    /*    public static int[] ConvertByteArrayToIntArray(byte[] byteArray)
        {
            int[] intArray = new int[byteArray.Length / 4]; // 4 byte'lýk integer deðerler olduðunu varsayýyoruz.

            for (int i = 0; i < byteArray.Length; i += 4)
            {
                int intValue = BitConverter.ToInt32(byteArray, i);
                intArray[i / 4] = intValue;
            }

            return intArray;
        }
    */

    float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
    }

    private void FixedUpdate()
    {
        movementSpeed = rov_LowerThrusters.movementSpeed;
        if (udpListener.Available > 0)
        {
            byte[] data = udpListener.Receive(ref udpEndPoint);
            HandleMessage(data);
        }
    }

    private void HandleMessage(byte[] ints)
    {
        float x = (float)(sbyte)ints[0];
        float y = (float)(sbyte)ints[1];
        float z = (float)(sbyte)ints[2];
        float h = (float)(sbyte)ints[3];
        float r = (float)(sbyte)ints[4];
        float k = (float)(sbyte)ints[5];
        //float l = (float)Convert.ToInt32(ints[4]);
        //float m = (float)Convert.ToInt32(ints[5]);
        //float n = (float)Convert.ToInt32(ints[6]);
        //x = x - 127;
        //y = y - 127;
        //z = z - 127;
        //h = h - 127;
        //p = p - 127;
        //r = r - 127;
        //k = k - 127;
        Debug.Log(string.Format("int: {0} {1} {2} {3} \n", x, y, z, h));
        Debug.Log(string.Format("byte: {0} {1} {2} {3} \n", ints[0], ints[1], ints[2], ints[3]));
        //Debug.Log(string.Format("int: {0} {1} {2} {3} {4} {5} {6} \n", x, y, z, h,p,r,k));
        //Debug.Log(string.Format("byte: {0} {1} {2} {3} {4} {5} {6} \n", ints[0], ints[1], ints[2], ints[3], ints[4], ints[5], ints[6]));
        ROV.transform.Translate(Vector3.forward * Time.deltaTime * y / 9.5f);
        ROV.transform.Translate(Vector3.right * Time.deltaTime * x / 20);
        //translationAmount = new Vector3(0.0f, z / 3900, 0.0f);
        //ROV.transform.Translate(translationAmount);
        rotationAmount = h / 50;
        ROV.transform.Rotate(Vector3.up, rotationAmount);

        if (r == 1)
        {
            float target_depth = k / 100;
            float tolerance = 0.05f; // Tolerans deðeri
            float simDepth = Map(target_depth, 0f, 2f, -9f, -0.6f);

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

    private void OnDestroy()
    {
        udpListener.Close();
    }
}
