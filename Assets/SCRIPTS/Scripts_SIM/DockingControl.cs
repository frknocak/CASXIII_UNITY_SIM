using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DockingControl : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("underWaterObj")) // Oyuncu etiketli nesneye dokunma kontrol�
        {
            messageText.text = "Ara� dokundu";
            StartCoroutine(ClearMessageAfterDelay(3));
        }
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen s�reyi bekle
        messageText.text = ""; // Mesaj� temizle
    }
}
