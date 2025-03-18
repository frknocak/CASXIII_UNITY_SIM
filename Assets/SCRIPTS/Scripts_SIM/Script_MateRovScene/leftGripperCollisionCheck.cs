using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftGripperCollisionCheck : MonoBehaviour
{
    public Transform GripperBase;
    public Transform midPoint;

    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("leftCollCheckBool", 0);
        

    }

        // BU ESKİ KOD SAP SOL GRİPPERLARI TEK OLARAK "gripperCollisionCheck" kodunda birleştirdim
// 
//
//  BAKMAK İSTERSEN DİYE SİLMEDİM
//
//
//
//
    private void OnCollisionEnter(Collision other)
    {
        Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "interactable")
        {
            PlayerPrefs.SetInt("leftCollCheckBool", 1);
            other.transform.position = midPoint.position;
            other.transform.LookAt(GripperBase);
            other.transform.SetParent(GripperBase);
            otherRigidbody.useGravity = false;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "interactable")
        {
            PlayerPrefs.SetInt("leftCollCheckBool", 0);
            other.transform.SetParent(null);
            otherRigidbody.useGravity = true;
        }
    }
}
