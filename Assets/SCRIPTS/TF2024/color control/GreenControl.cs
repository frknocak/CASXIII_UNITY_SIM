using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GreenControl : MonoBehaviour
{
    int green_number;
    public TextMeshProUGUI resultText;


    private void Start()
    {
        green_number = 0;
    }

    // on trigger enter kullan�lacak
    private void OnTriggerEnter(Collider other)
    {
        // Torpidonun "Missile" tagine sahip oldu�unu varsay�yoruz
        if (other.CompareTag("TorpedoTwo"))
        {
            green_number++;
            Debug.Log("Ye�ilden ge�ti " + green_number);
            // Sahneye yaz� yazd�rma i�lemini buradan yapabilirsin
            // �rne�in, bir UI Text nesnesinin text �zelli�ini de�i�tirebilirsin
            // GameObject.Find("ResultText2").GetComponent<UnityEngine.UI.Text>().text = "Ye�ilden ge�ti " + green_number;
            resultText.text = "Yeşilden geçti"  + green_number;
        }
    }



}
