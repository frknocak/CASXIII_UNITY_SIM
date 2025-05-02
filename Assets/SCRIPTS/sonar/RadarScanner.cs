using UnityEngine;

public class RadarScanner : MonoBehaviour
{
    public float scanRange = 10f;
    public int numberOfRays = 60;
    public float rotationSpeed = 30f;
    [HideInInspector] public float starting_rotation;

    public RadarUI radarUI; // RadarUI scriptine baðlan

    private void Start()
    {
        starting_rotation = transform.eulerAngles.y;
    }

    void Update()
    {
        transform.Rotate((-1)*Vector3.up * rotationSpeed * Time.deltaTime);

        float angleStep = 360f / numberOfRays;

        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * angleStep;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;

            Ray ray = new Ray(transform.position, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, scanRange))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                    radarUI.ShowPing(hit.point); // Noktayý göster
                }
            }
            else
            {
                Debug.DrawRay(transform.position, dir * scanRange, Color.red);
            }
        }
    }
}
