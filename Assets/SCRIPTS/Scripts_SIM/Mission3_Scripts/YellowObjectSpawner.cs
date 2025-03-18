using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject yellowTargetPrefab;
    [SerializeField] private GameObject yellowTargetInst_;
    [SerializeField] public int received_toggle3; 
    public int poolSizeBigOrLarge=1;

    // Start is called before the first frame update
    void Start()
    {

        received_toggle3 = PlayerPrefs.GetInt("randomizer3");
        

        if(poolSizeBigOrLarge == 0){
        if(received_toggle3 == 1){
        Vector3 randomPos = new Vector3(Random.Range(-20,15),-25f,Random.Range(-40,50));
        yellowTargetInst_ = Instantiate(yellowTargetPrefab, randomPos, Quaternion.identity);
        }
        else
        {
        yellowTargetInst_ = Instantiate(yellowTargetPrefab, new Vector3(0,-25f,20), Quaternion.identity);
        }

        }else if(poolSizeBigOrLarge == 1){
        if(received_toggle3 == 1){
        Vector3 randomPos = new Vector3(Random.Range(-15,10),-5f,Random.Range(-35,45));
            yellowTargetInst_ = Instantiate(yellowTargetPrefab, randomPos, Quaternion.identity);
        }
        else
        {
        yellowTargetInst_ = Instantiate(yellowTargetPrefab, new Vector3(0,-5f,20), Quaternion.identity);

        }
        }


    }

    void Update(){
        poolSizeBigOrLarge = PlayerPrefs.GetInt("poolSizePref");
    }


     public void PoolSizePrefChangedMidGame(){
        received_toggle3 = PlayerPrefs.GetInt("randomizer");
        if(poolSizeBigOrLarge == 1){
        yellowTargetInst_.transform.position = new Vector3(yellowTargetInst_.transform.position.x, -25f, yellowTargetInst_.transform.position.z);

        }else if(poolSizeBigOrLarge == 0){
        yellowTargetInst_.transform.position = new Vector3(yellowTargetInst_.transform.position.x, -5f, yellowTargetInst_.transform.position.z);
        }
    }

}
