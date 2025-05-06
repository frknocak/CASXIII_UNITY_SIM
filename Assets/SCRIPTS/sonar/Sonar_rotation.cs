using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Sonar_rotation : MonoBehaviour
{
    public Color rayColor = Color.red;

    private float rotationAmount = 0.9f; // 400 * 0.9 = 360 derece
    private int stepCounter = 0;
    private int totalSteps = 400;

    public class RayHitData
    {
        public Vector3 position;
        public float angle;
        public float distance;

        public RayHitData(Vector3 pos, float ang, float dist)
        {
            position = pos;
            angle = ang;
            distance = dist;
        }
    }

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
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, 150f);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance)); // En yakýndan en uzaða sýrala

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                Debug.DrawLine(origin, hit.point, color);

                float angle = Vector3.Angle(direction, hit.normal);
                float distance = hit.distance;

                rayHitData[index].Add(new RayHitData(hit.transform.position, angle, distance));

                // PoolWalls'a çarptýysa, ýþýný burada sonlandýr
                if (hit.collider.CompareTag("PoolWall"))
                    break;

                // Iþýný bir sonraki objeye yönlendirmek için yeni origin
                origin = hit.point;
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * 50, Color.green);
            rayHitData[index].Add(new RayHitData(Vector3.zero, 0f, 0f));
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
                Debug.Log($"[{data.position.x}, {data.position.y}, {data.position.z}] - Açý: {data.angle} - Mesafe: {data.distance}");
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
            // Baþlangýç pozisyonunu yaz
            Vector3 startPos = transform.position;
            writer.WriteLine($"Sensor Position: [{startPos.x}, {startPos.y}, {startPos.z}]");
            writer.WriteLine();

            for (int i = 0; i < rayHitData.Length; i++)
            {
                writer.WriteLine($"--- Ray {i} ---");
                foreach (RayHitData data in rayHitData[i])
                {
                    writer.WriteLine($"{data.position.x}, {data.position.y}, {data.position.z}, {data.angle}, {data.distance}");
                }
                writer.WriteLine();
            }
        }

        Debug.Log($"Veriler þu dosyaya yazýldý: {filePath}");
    }
}
