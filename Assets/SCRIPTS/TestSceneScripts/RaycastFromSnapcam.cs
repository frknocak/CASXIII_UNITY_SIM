using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;
using UnityEngine;
public class RaycastFromSnapcam : MonoBehaviour
{

    [SerializeField] 
    public GameObject SnapCam;
    public GameObject UnderCam;

    public float raycastLength = 20;
    public MeshRenderer rend;
    public Projector projector;
    public Material ScannedAreaMat;
    public LayerMask hitLayer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(SnapCam.transform.position, transform.TransformDirection(SnapCam.transform.forward), out hit, raycastLength, hitLayer))
        {
            Debug.DrawRay(SnapCam.transform.position, transform.TransformDirection(SnapCam.transform.forward) * raycastLength, Color.green);
            Debug.Log("HIT!");
            rend = hit.transform.gameObject.GetComponent<MeshRenderer>();
            rend.material.color = new Color(255,0,0,1);

            //projector.transform.position = hit.point;
            //CreateScannedAreaMat(hit)
            //projector.transform.rotation = Quaternion.LookRotation(hit.normal);

        }
        else{
            Debug.DrawRay(SnapCam.transform.position, transform.TransformDirection(SnapCam.transform.forward) * raycastLength, Color.red);
            Debug.Log("not hit!");

        }

        if(Physics.Raycast(UnderCam.transform.position, transform.TransformDirection(UnderCam.transform.forward), out hit, raycastLength, hitLayer))
        {
            Debug.DrawRay(UnderCam.transform.position, transform.TransformDirection(UnderCam.transform.forward) * raycastLength, Color.green);
            Debug.Log("HIT!");
            rend = hit.transform.gameObject.GetComponent<MeshRenderer>();
            rend.material.color = new Color(255,0,0,1);

            //projector.transform.position = hit.point;
            //CreateScannedAreaMat(hit)
            //projector.transform.rotation = Quaternion.LookRotation(hit.normal);

        }
        else{
            Debug.DrawRay(UnderCam.transform.position, transform.TransformDirection(UnderCam.transform.forward) * raycastLength, Color.red);
            Debug.Log("not hit!");

        }
    }
    void CreateScannedAreaMat(RaycastHit hit ){

    }




    }
