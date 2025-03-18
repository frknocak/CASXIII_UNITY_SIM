using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurfaceCheck : MonoBehaviour
{
    public float surfaceLevel = -1.2f; // Su y�zeyi seviyesi
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
            Debug.Log("Arac�n Y Pozisyonu: " + transform.position.y);

            if (exitChecker != null && !exitChecker.isInAllowedZone)
            {
                Debug.Log("Yar��may� Kaybettin! Yanl�� yerde su y�zeyine ��kt�n.");
                // Burada yar��may� kaybettirme i�lemini yapabilirsin.
                messageText.text = "Yar��may� Kaybettin.";
                StartCoroutine(ClearMessageAfterDelay(1));
            }
            if (exitChecker != null && exitChecker.large_area_control)
            {
                Debug.Log("B�y�k Alandan ��kt�n.");
                // Burada yar��may� kaybettirme i�lemini yapabilirsin.
                messageText.text = "B�y�k Alandan ��kt�n, tebrikler!";
                StartCoroutine(ClearMessageAfterDelay(2));
            }
            if (exitChecker != null && exitChecker.medium_area_control)
            {
                Debug.Log("Orta Alandan ��kt�n.");
                // Burada yar��may� kaybettirme i�lemini yapabilirsin.
                messageText.text = "Orta Alandan ��kt�n, tebrikler!";
                StartCoroutine(ClearMessageAfterDelay(2));
            }
            if (exitChecker != null && exitChecker.small_area_control)
            {
                Debug.Log("K���k Alandan ��kt�n.");
                // Burada yar��may� kaybettirme i�lemini yapabilirsin.
                messageText.text = "K���k Alandan ��kt�n, tebrikler!";
                StartCoroutine(ClearMessageAfterDelay(2));
            }
        }

    }
    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen s�reyi bekle
        messageText.text = ""; // Mesaj� temizle
    }
}
