using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject door_grayBluePrefab;
    [SerializeField] private GameObject door_greenRedPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offset2;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;



// gri ve mavinin sağda mı yoksa solda mı olduğunun kesinleşmesi lazım. model yanlış eğitilebilir.

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-6,0,0);
        offset2 = new Vector3(0, -1, 0);
        int i = Random.Range(1,4);
        if(i == 1){
        Instantiate(door_grayBluePrefab, pos1.position + offset2, new Quaternion (0,0,1,1));
        Instantiate(door_greenRedPrefab, pos2.position + offset + offset2, new Quaternion (0,0,1,1));
        }
        if(i == 2){
        Instantiate(door_grayBluePrefab, pos2.position + offset2, new Quaternion (0,0,1,1));
        Instantiate(door_greenRedPrefab, pos1.position + offset + offset2, new Quaternion (0,0,1,1));
        }
        if(i == 3){
        Instantiate(door_grayBluePrefab, pos3.position + offset2, new Quaternion (0,0,1,1));
        Instantiate(door_greenRedPrefab, pos2.position + offset + offset2, new Quaternion (0,0,1,1));
        }
    }


}
