using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarScanner3D : MonoBehaviour
{
    [SerializeField] private Transform pfRadarPing; // Ping prefab�
    [SerializeField] private Transform sonarHead;   // D�nen kafa
    [SerializeField] private LayerMask sonarLayerMask; // Taranacak layerlar
    [SerializeField] private float sonarRange = 50f;  // Maksimum sonar mesafesi

    private float rotationSpeed = 30f; // Sweep d�n�� h�z� (derece/sn)
    private int rayCount = 25;          // Ayn� anda g�nderilecek ���n say�s�
    private List<Collider> detectedObjects = new List<Collider>();

    private void Update()
    {
        // Sonar kafas�n� d�nd�r
        sonarHead.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Her frame 25 ���n g�nder
        for (int i = 0; i < rayCount; i++)
        {
            float verticalOffset = Mathf.Lerp(-10f, 10f, (float)i / (rayCount - 1));
            // 25 ���n� yukar�-a�a�� hafif farkl� y�nlerde da��t�yoruz
            Vector3 direction = Quaternion.Euler(verticalOffset, sonarHead.eulerAngles.y, 0) * Vector3.forward;

            Ray ray = new Ray(sonarHead.position, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, sonarRange, sonarLayerMask))
            {
                if (!detectedObjects.Contains(hit.collider))
                {
                    detectedObjects.Add(hit.collider);

                    // Ping olu�tur
                    Transform ping = Instantiate(pfRadarPing, hit.point, Quaternion.identity);
                    RadarPing pingScript = ping.GetComponent<RadarPing>();
                    pingScript.SetColor(Color.green);
                    pingScript.SetDisappearTimer(2f);
                }
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 0.1f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * sonarRange, Color.green, 0.1f);
            }
        }

        // Sweep tam d�nd���nde list temizle
        if (sonarHead.eulerAngles.y >= 359f || sonarHead.eulerAngles.y <= 1f)
        {
            detectedObjects.Clear();
        }
    }
}
