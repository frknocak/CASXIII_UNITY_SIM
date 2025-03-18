using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Route : MonoBehaviour
{
    public List<GameObject> waypoints;
    int index = 0;
    public float speed=5;
    public bool Loop = true;
    public float yChange = -0.002f;
    public float rotationSpeed;
    private float startTime;
    

    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        for (int i =0; i<waypoints.Count; i++)
        {
            GameObject obj = waypoints[i];
            Vector3 waypointsPos = obj.transform.position;
            waypointsPos.y += (yChange* i);
            obj.transform.position = waypointsPos;
        }
        
    }

    // Update is called once per frame
    async void Update()
    {
        rotationSpeed = speed/5f;



        if(Time.time - startTime >4f)
        {

        Vector3 targetLocation = waypoints[index].transform.position;


        Quaternion targetRotation = Quaternion.LookRotation(targetLocation - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 pos = Vector3.MoveTowards(transform.position, targetLocation, speed*Time.deltaTime);
        
        transform.position = pos;

        

        Vector3 defaultPoint = new Vector3(1,3,-30); 

        float distance = Vector3.Distance(transform.position, targetLocation);

        if(distance <= 0.05)
        {
           
            if(index < waypoints.Count-1)
            {
            index++;

            }else
            {
                if(Loop)
                {
                index = 0;
                await Task.Delay(1);
                }else
                {
                    
                    transform.position = defaultPoint;
                    transform.rotation = Quaternion.identity;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    await Task.Delay(1);
                    enabled = false;
                }
            }
        }
        }


    }
}
