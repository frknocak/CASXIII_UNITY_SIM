using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEditor;
using UnityEngine;

public class TorpedoController : MonoBehaviour
{
    //public GameObject TorpedoOne;
    public GameObject TorpedoTwo;
    public float torpedo_speed;
    //Vector3 ImpulseVector;
    Vector3 offset;
    //int torpedoCount;
    public UDP_portTF UDP_portTF;
    bool canFire = true; // Tek atış için kontrol değişkeni
    private bool isWaiting = false;

    public GameObject torpedoExitPoint;
    public GameObject LookAtObject;

    

    void Start()
    {
        //GetComponent<Rigidbody>().useGravity = false;
        offset = new Vector3(0,-0.75f, 0);
        //torpedoCount = 0;

    }

    private void FixedUpdate()
    {
        HandleTorpedoLaunch();
    }

    public void HandleTorpedoLaunch()
    {
        TorpedoTwo.transform.position = this.transform.position;
        Vector3 forwardDirection = transform.forward; // Aracın ileri yönde vektörü
        Quaternion vehicleRotation = transform.rotation;
        Vector3 ImpulseVector = forwardDirection * torpedo_speed;

        

        //ImpulseVector = new Vector3(0, 0, -torpedo_speed);
        if (!isWaiting && UDP_portTF.shouldFire)
        {
            GameObject Inst_torpedo;
            Inst_torpedo = Instantiate(TorpedoTwo, torpedoExitPoint.transform.position, Quaternion.identity);
            Inst_torpedo.transform.LookAt(LookAtObject.transform.position ,Vector3.forward);


            Inst_torpedo.GetComponent<Rigidbody>().AddForce(ImpulseVector, ForceMode.Impulse);
            //torpedoCount++;
            UDP_portTF.shouldFire = false; // atış yapıldıktan sonra shouldFire değerini sıfırlayabilirsiniz
            StartCoroutine(WaitingFire());
        }
        if (Input.GetKeyDown(KeyCode.R) && canFire)
        {
            GameObject Inst_torpedo;
            Inst_torpedo = Instantiate(TorpedoTwo, torpedoExitPoint.transform.position, Quaternion.identity);

            Inst_torpedo.transform.LookAt(LookAtObject.transform.position ,Vector3.forward);


            Inst_torpedo.GetComponent<Rigidbody>().AddForce(ImpulseVector, ForceMode.Impulse);

            canFire = false; // Tekrar atış yapılabilmesi için tekrar true yapılmalıdır.
            StartCoroutine(ResetFireCooldown());
        }
    }
    IEnumerator ResetFireCooldown()
    {
        yield return new WaitForSeconds(0.2f); // Atışlar arasındaki minimum süre (istediğiniz süreyi ayarlayabilirsiniz)
        canFire = true; // Tekrar atış yapılabilmesi için tekrar true yapılmalıdır.
    }

    IEnumerator WaitingFire()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        isWaiting = false;
    }
}
