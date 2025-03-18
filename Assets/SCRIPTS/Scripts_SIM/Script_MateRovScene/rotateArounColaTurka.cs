using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateArounColaTurka : MonoBehaviour
{
    public GameObject pivotPoint;
    public float rotationSpeed = 10f;
    public float startTime = 0;
    public float ascendingSpeed = 0.5f;
    public bool ascendingBool = true;


    // Start is called before the first frame update
    void Start()
    {
     startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotPoint.transform.position, new Vector3(0,1,0), rotationSpeed * Time.deltaTime);
        if(Time.time - startTime > 37 && ascendingBool)
         {
            transform.Translate(Vector3.up * 2, Space.World);
            transform.Translate(0, 0, -3, Space.Self);
            //transform.Rotate(30.0f, 0.0f, 0.0f, Space.Self);
            ascendingBool = false;

         }
        
        

    }

    
}
