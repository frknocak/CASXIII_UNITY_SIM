using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarScanner3D : MonoBehaviour
{
    [SerializeField] private Transform pfRadarPing; // Ping prefabý
    [SerializeField] private Transform sonarHead;   // Dönen kafa
    [SerializeField] private LayerMask sonarLayerMask; // Taranacak layerlar
    [SerializeField] private float sonarRange = 50f;  // Maksimum sonar mesafesi

    private float rotationSpeed = 30f; // Sweep dönüþ hýzý (derece/sn)
    private int rayCount = 25;          // Ayný anda gönderilecek ýþýn sayýsý
    private List<Collider> detectedObjects = new List<Collider>();

    private void Update()
    {
        // Sonar kafasýný döndür
        sonarHead.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Her frame 25 ýþýn gönder
        for (int i = 0; i < rayCount; i++)
        {
            float verticalOffset = Mathf.Lerp(-10f, 10f, (float)i / (rayCount - 1));
            // 25 ýþýný yukarý-aþaðý hafif farklý yönlerde daðýtýyoruz
            Vector3 direction = Quaternion.Euler(verticalOffset, sonarHead.eulerAngles.y, 0) * Vector3.forward;

            Ray ray = new Ray(sonarHead.position, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, sonarRange, sonarLayerMask))
            {
                if (!detectedObjects.Contains(hit.collider))
                {
                    detectedObjects.Add(hit.collider);

                    // Ping oluþtur
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

        // Sweep tam döndüðünde list temizle
        if (sonarHead.eulerAngles.y >= 359f || sonarHead.eulerAngles.y <= 1f)
        {
            detectedObjects.Clear();
        }
    }
}
