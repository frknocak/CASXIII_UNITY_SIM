using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightGripperCollisionCheck : MonoBehaviour
{
    public Transform GripperBase;
    public Transform midPoint;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("rightCollCheckBool", 0);
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
        BoxCollider otherBoxCollider = other.gameObject.GetComponent<BoxCollider>();
        
        if (other.gameObject.tag == "interactable")
        {
            PlayerPrefs.SetInt("rightCollCheckBool", 1);
            other.transform.position = midPoint.position;
            other.transform.LookAt(GripperBase);
            other.transform.SetParent(GripperBase);
            otherRigidbody.useGravity = false;
            //otherBoxCollider.isTrigger = true;
            
        }
    }
    private void OnCollisionExit(Collision other)
    {
        Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
        BoxCollider otherBoxCollider = other.gameObject.GetComponent<BoxCollider>();

        if (other.gameObject.tag == "interactable")
        {
            PlayerPrefs.SetInt("rightCollCheckBool", 0);
            other.transform.SetParent(null);
            otherRigidbody.useGravity = true;
            //otherBoxCollider.isTrigger = false;

        }
    }
}
