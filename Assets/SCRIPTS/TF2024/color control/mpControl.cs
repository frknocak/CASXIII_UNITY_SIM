using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mpControl : MonoBehaviour
{
    int blue_number;
    public TextMeshProUGUI resultText;
    
    private void Start()
    {
        blue_number = 0;
    }

    // on trigger enter kullanılacak
    private void OnTriggerEnter(Collider other)
    {
        // Torpidonun "Missile" tagine sahip olduğunu varsayıyoruz
        if (other.CompareTag("TorpedoTwo"))
        {
            blue_number++;
            Debug.Log("Kırmızıdan geçti " + blue_number);
            // Sahneye yazı yazdırma işlemini buradan yapabilirsin
            // Örneğin, bir UI Text nesnesinin text özelliğini değiştirebilirsin
            // GameObject.Find("ResultText").GetComponent<UnityEngine.UI.Text>().text = "Maviden geçti " + blue_number;
            resultText.text = "Kırmızıdan Geçti: " + blue_number;
        }
    }




}
