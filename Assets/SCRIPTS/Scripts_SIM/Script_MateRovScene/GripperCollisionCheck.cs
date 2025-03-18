using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripperCollisionCheck : MonoBehaviour
{

    public GameObject leftGripHand;
    public GameObject rightGripHand;
    public GameObject midPoint;
    public Transform GripperBase;
    public Vector3 hitPoint;
    public Vector3 finalHitPoint;
    public GameObject tempPivotPoint;
    public bool hitPointBool = false;
    public GameObject interactableWithCollider;



    // Start is called before the first frame update
    void Start()
    {
    


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        GameObject[] interactables = GameObject.FindGameObjectsWithTag("interactable");

        foreach (GameObject interactable in interactables)
        {

            if (leftGripHand.GetComponent<Collider>().bounds.Intersects(interactable.GetComponent<Collider>().bounds) &&
                rightGripHand.GetComponent<Collider>().bounds.Intersects(interactable.GetComponent<Collider>().bounds))
            {
                Rigidbody otherRigidbody = interactable.gameObject.GetComponent<Rigidbody>();

                Vector3 raycastTargetPoint = -transform.right;
                Ray ray = new Ray(leftGripHand.transform.position, raycastTargetPoint);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 2))
                {
                    Debug.DrawRay(leftGripHand.transform.position, raycastTargetPoint * 2, Color.red);
                    hitPoint = hit.point;
                    hitPointBool = true;
                }

                 
                Debug.Log("gripper is colliding with an interactable!");

            }
            else
            {
                finalHitPoint = Vector3.zero;
                Rigidbody otherRigidbody = interactable.gameObject.GetComponent<Rigidbody>();
                
                interactable.transform.SetParent(null);
                otherRigidbody.useGravity = true;
                otherRigidbody.isKinematic = false;
                Destroy(interactableWithCollider);
                interactableWithCollider = null;
            }
        }
    }


}
