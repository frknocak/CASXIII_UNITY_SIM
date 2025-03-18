using UnityEngine;

public class interactableObjects : MonoBehaviour
{
    public GameObject interactable;
    private bool boxColAdded = false;
    private BoxCollider existedColl;
    public GameObject midPoint;
    public GameObject constantPoint;

    void Start()
    {
        
    }


    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "LeftGripHand" && boxColAdded == false)
        {
            ContactPoint[] contacts = new ContactPoint[10];
            int numContacts = other.GetContacts(contacts);


            existedColl = transform.GetComponent<BoxCollider>();


            //BoxCollider collider = interactable.AddComponent<BoxCollider>();
            existedColl.size = new Vector3(1f, 1f, 0.1f);
            existedColl.center = midPoint.transform.position;

            boxColAdded = true;

        }
    }
    void Update()
    {
        //constantPoint = constantPoint + new Vector3(0, 0, 1);
    }
}
