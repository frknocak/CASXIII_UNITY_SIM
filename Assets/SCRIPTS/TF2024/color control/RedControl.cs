using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RedControl : MonoBehaviour
{
    int red_number;
    public TextMeshProUGUI resultText;


    private void Start()
    {
        red_number = 0;
    }

    // on trigger enter kullan�lacak
    private void OnTriggerEnter(Collider other)
    {
        // Torpidonun "Missile" tagine sahip oldu�unu varsay�yoruz
        if (other.CompareTag("TorpedoTwo"))
        {
            red_number++;
            Debug.Log("Maviden geçti " + red_number);
            // Sahneye yaz� yazd�rma i�lemini buradan yapabilirsin
            // �rne�in, bir UI Text nesnesinin text �zelli�ini de�i�tirebilirsin
            // GameObject.Find("ResultText1").GetComponent<UnityEngine.UI.Text>().text = "K�rm�z�dan ge�ti " + red_number;
            resultText.text = "Maviden geçti" + red_number;

        }
    }



}
