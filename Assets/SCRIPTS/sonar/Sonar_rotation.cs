//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;

//public class Sonar_rotation : MonoBehaviour
//{
//    public Color rayColor = Color.red;

//    private float rotationAmount = 0.9f; // 400 * 0.9 = 360 derece
//    private int stepCounter = 0;
//    private int totalSteps = 400;

//    public List<Vector3>[] rayHitPositions = new List<Vector3>[25];

//    private void Start()
//    {
//        for (int i = 0; i < rayHitPositions.Length; i++)
//        {
//            rayHitPositions[i] = new List<Vector3>();
//        }
//    }

//    void Update()
//    {
//        if (stepCounter >= totalSteps)
//            return;

//        castRays();                         // Rayler her adýmda atýlýyor
//        transform.Rotate(Vector3.up, rotationAmount); // Sonra döndürülüyor
//        stepCounter++;

//        if (stepCounter == totalSteps)
//        {
//            PrintRayPositionData();
//            ExportToTxt();
//        }
//    }

//    private void castRays()
//    {
//        Vector3 origin = transform.position;
//        Vector3 direction = transform.forward;

//        int index = 0;
//        float angleStep = 1f;

//        CastAllAndStore(origin, direction, rayColor, index);
//        index++;

//        for (int i = 1; i <= 12; i++)
//        {
//            Quaternion upRotation = Quaternion.AngleAxis(angleStep * i, transform.right);
//            Vector3 upDir = upRotation * direction;
//            CastAllAndStore(origin, upDir, rayColor, index);
//            index++;
//        }

//        for (int i = 1; i <= 12; i++)
//        {
//            Quaternion downRotation = Quaternion.AngleAxis(angleStep * i, -transform.right);
//            Vector3 downDir = downRotation * direction;
//            CastAllAndStore(origin, downDir, rayColor, index);
//            index++;
//        }
//    }

//    private void CastAllAndStore(Vector3 origin, Vector3 direction, Color color, int index)
//    {
//        RaycastHit[] hits = Physics.RaycastAll(origin, direction, 50f);

//        if (hits.Length > 0)
//        {
//            // Ýlk nesnenin pozisyonunu al (alternatif olarak tümünü kaydedebilirsin)
//            Debug.DrawLine(origin, hits[0].point, color);
//            rayHitPositions[index].Add(hits[0].transform.position);
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

//        Debug.Log($"Veriler þu dosyaya yazýldý: {filePath}");
//    }
//}

using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Yeni sýnýf: Ray çarpma verilerini tutar (nokta + açý)
public class RayHitData
{
    public Vector3 point;
    public float angle;

    public RayHitData(Vector3 point, float angle)
    {
        this.point = point;
        this.angle = angle;
    }
}

public class Sonar_rotation : MonoBehaviour
{
    public Color rayColor = Color.red;

    private float rotationAmount = 0.9f; // 400 * 0.9 = 360 derece
    private int stepCounter = 0;
    private int totalSteps = 400;

    public List<RayHitData>[] rayHitData = new List<RayHitData>[25];

    private void Start()
    {
        for (int i = 0; i < rayHitData.Length; i++)
        {
            rayHitData[i] = new List<RayHitData>();
        }
    }

    void Update()
    {
        if (stepCounter >= totalSteps)
            return;

        castRays();                         // Rayler her adýmda atýlýyor
        transform.Rotate(Vector3.up, rotationAmount); // Sonra döndürülüyor
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
            Debug.DrawLine(origin, hits[0].point, color);
            float angle = Vector3.Angle(direction, hits[0].normal); // Açýyý hesapla
            rayHitData[index].Add(new RayHitData(hits[0].transform.position, angle));
        }
        else
        {
            Debug.DrawRay(origin, direction * 50, Color.green);
            rayHitData[index].Add(new RayHitData(Vector3.zero, 0)); // -1: çarpma yok
        }
    }

    private void PrintRayPositionData()
    {
        Debug.Log("------ 400 ADIMLIK TARAMA TAMAMLANDI ------");

        for (int i = 0; i < rayHitData.Length; i++)
        {
            Debug.Log($"--- Ray {i} ---");
            foreach (RayHitData data in rayHitData[i])
            {
                Debug.Log($"[{data.point.x}, {data.point.y}, {data.point.z}, Açý: {data.angle}]");
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
            for (int i = 0; i < rayHitData.Length; i++)
            {
                writer.WriteLine($"--- Ray {i} ---");
                foreach (RayHitData data in rayHitData[i])
                {
                    writer.WriteLine($"{data.point.x}, {data.point.y}, {data.point.z}, {data.angle}");
                }
                writer.WriteLine();
            }
        }

        Debug.Log($"Veriler þu dosyaya yazýldý: {filePath}");
    }
}
