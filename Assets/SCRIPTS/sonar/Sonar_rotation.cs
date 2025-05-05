//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Sonar_rotation : MonoBehaviour
//{
//    //public float speed = 50f;
//    public Color rayColor = Color.red; // I��n�n �arpt���nda kullan�lacak renk
//    private float startTime;
//    private float rotationInterval = 0.0875F; // *** birim ba�� d�nme h�z� (35/400) *** 
//    private float rotation = 0f;
//    private float rotationAmount = 0.9f; //360/400. bluerobotics sonar� 400 derecede tarama yap�yor.

//    private void Start()
//    {
//        startTime = Time.time;
//    }
//    void Update()
//    {
//        move();
//        isin();
//    }

//    private void move()
//    {
//        if (Time.time - startTime >= rotationInterval)
//        {
//            startTime = Time.time;
//            rotation += rotationAmount;
//            transform.Rotate(Vector3.up, rotationAmount);
//        }

//    }

//    private void isin()
//    {
//        // Ana ���n�n ba�lang�� noktas� ve y�n�
//        Vector3 rayOrigin = transform.position;
//        Vector3 rayDirection = transform.forward;

//        // Ana ���n �izdirme
//        DrawRay(rayOrigin, rayDirection, rayColor);

//        float angleStep = 1f;    // Her ad�mda 1 derece art��

//        // 24 adet ek ���n� �iz
//        for (int i = 0; i < 13; i++)
//        {
//            // D�n�� matrisi ile ���n y�n�n� g�ncelle
//            Quaternion rotation = Quaternion.AngleAxis(angleStep * i, transform.right);
//            Vector3 rotatedDirection = rotation * rayDirection;

//            // �izgiyi �iz
//            DrawRay(rayOrigin, rotatedDirection, rayColor);
//        }
//        for (int i = 0; i < 13; i++)
//        {
//            // D�n�� matrisi ile ���n y�n�n� g�ncelle
//            Quaternion rotation = Quaternion.AngleAxis(angleStep * i, (-1)*transform.right);
//            Vector3 rotatedDirection = rotation * rayDirection;

//            // �izgiyi �iz
//            DrawRay(rayOrigin, rotatedDirection, rayColor);
//        }
//    }

//    private void DrawRay(Vector3 origin, Vector3 direction, Color color)
//    {
//        // Raycast
//        RaycastHit[] hits = Physics.RaycastAll(origin, direction, 50);

//        bool hitSomething = false; // I��n�n bir �eye �arp�p �arpmad���n� kontrol etmek i�in bir de�i�ken

//        // Her �arp��ma noktas� i�in �izgi �iz
//        foreach (RaycastHit hit in hits)
//        {
//            Debug.DrawLine(origin, hit.point, color); // �arpt���nda rayColor rengini kullan
//            hitSomething = true; // I��n�n bir �eye �arpt���n� belirt
//            Debug.Log("Hitted: " + hit.collider.transform.position);
//            Debug.Log("Hit distance: " + hit.distance);
//        }

//        // E�er bir �eye �arpt�ysa ���n� belirtilen renge �evir
//        if (hitSomething)
//        {
//            Debug.DrawRay(origin, direction * 50, color);
//        }
//        else
//        {
//            // Rayin 50 birim mesafede �izgi �iz
//            Debug.DrawRay(origin, direction * 50, Color.green);
//        }
//    }
//}

//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;

//public class Sonar_rotation : MonoBehaviour
//{
//    public Color rayColor = Color.red;

//    private float startTime;
//    private float rotationInterval = 0.0875f;
//    private float rotation = 0f;
//    private float rotationAmount = 0.9f;
//    private int stepCounter = 0;
//    private int totalSteps = 400;

//    public List<Vector3>[] rayHitPositions = new List<Vector3>[25];

//    private void Start()
//    {
//        startTime = Time.time;

//        for (int i = 0; i < rayHitPositions.Length; i++)
//        {
//            rayHitPositions[i] = new List<Vector3>();
//        }
//    }

//    void Update()
//    {
//        if (stepCounter >= totalSteps)
//            return;

//        move();
//        castRays();
//        stepCounter++;

//        if (stepCounter == totalSteps)
//        {
//            PrintRayPositionData();
//            ExportToTxt();
//        }
//    }

//    private void move()
//    {
//        if (Time.time - startTime >= rotationInterval)
//        {
//            startTime = Time.time;
//            rotation += rotationAmount;
//            transform.Rotate(Vector3.up, rotationAmount);
//        }
//    }

//    private void castRays()
//    {
//        Vector3 origin = transform.position;
//        Vector3 direction = transform.forward;

//        int index = 0;

//        CastAndStore(origin, direction, rayColor, index);
//        index++;

//        float angleStep = 1f;

//        for (int i = 1; i <= 12; i++)
//        {
//            Quaternion upRotation = Quaternion.AngleAxis(angleStep * i, transform.right);
//            Vector3 upDir = upRotation * direction;
//            CastAndStore(origin, upDir, rayColor, index);
//            index++;
//        }

//        for (int i = 1; i <= 12; i++)
//        {
//            Quaternion downRotation = Quaternion.AngleAxis(angleStep * i, -transform.right);
//            Vector3 downDir = downRotation * direction;
//            CastAndStore(origin, downDir, rayColor, index);
//            index++;
//        }
//    }

//    private void CastAndStore(Vector3 origin, Vector3 direction, Color color, int index)
//    {
//        RaycastHit hit;
//        bool isHit = Physics.Raycast(origin, direction, out hit, 50f);

//        if (isHit)
//        {
//            Debug.DrawLine(origin, hit.point, color);
//            rayHitPositions[index].Add(hit.transform.position); // << BURADA G�NCELLEME YAPTIK
//        }
//        else
//        {
//            Debug.DrawRay(origin, direction * 50, Color.green);
//            rayHitPositions[index].Add(Vector3.zero);
//        }
//    }


//    private void PrintRayPositionData()
//    {
//        Debug.Log("------ 400 ADIMLIK TARAMA TAMAMLANDI ------");

//        for (int i = 0; i < rayHitPositions.Length; i++)
//        {
//            Debug.Log($"--- Ray {i} ---");
//            foreach (Vector3 pos in rayHitPositions[i])
//            {
//                Debug.Log($"[{pos.x}, {pos.y}, {pos.z}]");
//            }
//        }
//    }

//    private void ExportToTxt()
//    {
//        string folderPath = Application.dataPath + "/SonarLogs";
//        if (!Directory.Exists(folderPath))
//        {
//            Directory.CreateDirectory(folderPath);
//        }

//        string filePath = folderPath + "/sonar_output.txt";

//        using (StreamWriter writer = new StreamWriter(filePath))
//        {
//            for (int i = 0; i < rayHitPositions.Length; i++)
//            {
//                writer.WriteLine($"--- Ray {i} ---");
//                foreach (Vector3 pos in rayHitPositions[i])
//                {
//                    writer.WriteLine($"{pos.x}, {pos.y}, {pos.z}");
//                }
//                writer.WriteLine();
//            }
//        }

//        Debug.Log($"Veriler �u dosyaya yaz�ld�: {filePath}");
//    }
//}

using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Sonar_rotation : MonoBehaviour
{
    public Color rayColor = Color.red;

    private float rotationAmount = 0.9f; // 400 * 0.9 = 360 derece
    private int stepCounter = 0;
    private int totalSteps = 400;

    public List<Vector3>[] rayHitPositions = new List<Vector3>[25];

    private void Start()
    {
        for (int i = 0; i < rayHitPositions.Length; i++)
        {
            rayHitPositions[i] = new List<Vector3>();
        }
    }

    void Update()
    {
        if (stepCounter >= totalSteps)
            return;

        castRays();                         // Rayler her ad�mda at�l�yor
        transform.Rotate(Vector3.up, rotationAmount); // Sonra d�nd�r�l�yor
        stepCounter++;

        if (stepCounter == totalSteps)
        {
            PrintRayPositionData();
            ExportToTxt();
        }
    }

    private void castRays()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        int index = 0;
        float angleStep = 1f;

        CastAllAndStore(origin, direction, rayColor, index);
        index++;

        for (int i = 1; i <= 12; i++)
        {
            Quaternion upRotation = Quaternion.AngleAxis(angleStep * i, transform.right);
            Vector3 upDir = upRotation * direction;
            CastAllAndStore(origin, upDir, rayColor, index);
            index++;
        }

        for (int i = 1; i <= 12; i++)
        {
            Quaternion downRotation = Quaternion.AngleAxis(angleStep * i, -transform.right);
            Vector3 downDir = downRotation * direction;
            CastAllAndStore(origin, downDir, rayColor, index);
            index++;
        }
    }

    private void CastAllAndStore(Vector3 origin, Vector3 direction, Color color, int index)
    {
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, 50f);

        if (hits.Length > 0)
        {
            // �lk nesnenin pozisyonunu al (alternatif olarak t�m�n� kaydedebilirsin)
            Debug.DrawLine(origin, hits[0].point, color);
            rayHitPositions[index].Add(hits[0].transform.position);
        }
        else
        {
            Debug.DrawRay(origin, direction * 50, Color.green);
            rayHitPositions[index].Add(Vector3.zero);
        }
    }

    private void PrintRayPositionData()
    {
        Debug.Log("------ 400 ADIMLIK TARAMA TAMAMLANDI ------");

        for (int i = 0; i < rayHitPositions.Length; i++)
        {
            Debug.Log($"--- Ray {i} ---");
            foreach (Vector3 pos in rayHitPositions[i])
            {
                Debug.Log($"[{pos.x}, {pos.y}, {pos.z}]");
            }
        }
    }

    private void ExportToTxt()
    {
        string folderPath = Application.dataPath + "/SonarLogs";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = folderPath + "/sonar_output.txt";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < rayHitPositions.Length; i++)
            {
                writer.WriteLine($"--- Ray {i} ---");
                foreach (Vector3 pos in rayHitPositions[i])
                {
                    writer.WriteLine($"{pos.x}, {pos.y}, {pos.z}");
                }
                writer.WriteLine();
            }
        }

        Debug.Log($"Veriler �u dosyaya yaz�ld�: {filePath}");
    }
}
