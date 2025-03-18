using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurfaceCheck : MonoBehaviour
{
    public float surfaceLevel = -1.2f; // Su yüzeyi seviyesi
    public SurfaceExitChecker exitChecker;
    public TextMeshProUGUI messageText;


    void Start()
    {
        exitChecker = GetComponent<SurfaceExitChecker>();
    }

    void Update()
    {

        if (transform.position.y >= surfaceLevel)
        {
            Debug.Log("Aracýn Y Pozisyonu: " + transform.position.y);

            if (exitChecker != null && !exitChecker.isInAllowedZone)
            {
                Debug.Log("Yarýþmayý Kaybettin! Yanlýþ yerde su yüzeyine çýktýn.");
                // Burada yarýþmayý kaybettirme iþlemini yapabilirsin.
                messageText.text = "Yarýþmayý Kaybettin.";
                StartCoroutine(ClearMessageAfterDelay(1));
            }
            if (exitChecker != null && exitChecker.large_area_control)
            {
                Debug.Log("Büyük Alandan çýktýn.");
                // Burada yarýþmayý kaybettirme iþlemini yapabilirsin.
                messageText.text = "Büyük Alandan çýktýn, tebrikler!";
                StartCoroutine(ClearMessageAfterDelay(2));
            }
            if (exitChecker != null && exitChecker.medium_area_control)
            {
                Debug.Log("Orta Alandan çýktýn.");
                // Burada yarýþmayý kaybettirme iþlemini yapabilirsin.
                messageText.text = "Orta Alandan çýktýn, tebrikler!";
                StartCoroutine(ClearMessageAfterDelay(2));
            }
            if (exitChecker != null && exitChecker.small_area_control)
            {
                Debug.Log("Küçük Alandan çýktýn.");
                // Burada yarýþmayý kaybettirme iþlemini yapabilirsin.
                messageText.text = "Küçük Alandan çýktýn, tebrikler!";
                StartCoroutine(ClearMessageAfterDelay(2));
            }
        }

    }
    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen süreyi bekle
        messageText.text = ""; // Mesajý temizle
    }
}
