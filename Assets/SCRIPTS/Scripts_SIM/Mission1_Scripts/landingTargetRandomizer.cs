using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class landingTargetRandomizer : MonoBehaviour
{
    public GameObject landingTargetPrefab;
    public GameObject landingTargetInst_;
    public int received_toggle;
    public int poolSizeBigOrLarge=1;

    void Start()
    {
        received_toggle = PlayerPrefs.GetInt("randomizer");
        if(poolSizeBigOrLarge == 0){
        if(received_toggle == 1){
        Vector3 randomPos = new Vector3(Random.Range(-20,15),(-29.9f),Random.Range(-40,50));
         landingTargetInst_ = Instantiate(landingTargetPrefab,randomPos, Quaternion.identity);
        }
        else
        {
         landingTargetInst_ = Instantiate(landingTargetPrefab,new Vector3(15,-29.9f,20), Quaternion.identity);
        }

        }else if(poolSizeBigOrLarge == 1){
        if(received_toggle == 1){
        Vector3 randomPos = new Vector3(Random.Range(-15,10),(-9.9f),Random.Range(-35,45));
         landingTargetInst_ = Instantiate(landingTargetPrefab,randomPos, Quaternion.identity);
        }
        else
        {
         landingTargetInst_ = Instantiate(landingTargetPrefab,new Vector3(15,-9.9f,20), Quaternion.identity);
        }
        }
       
    }
    void Update(){
        poolSizeBigOrLarge = PlayerPrefs.GetInt("poolSizePref");
    }

    public void PoolSizePrefChangedMidGame(){
        received_toggle = PlayerPrefs.GetInt("randomizer");
        if(poolSizeBigOrLarge == 1){
        landingTargetInst_.transform.position = new Vector3(landingTargetInst_.transform.position.x, -29.9f, landingTargetInst_.transform.position.z);

        }else if(poolSizeBigOrLarge == 0){
        landingTargetInst_.transform.position = new Vector3(landingTargetInst_.transform.position.x, -9.9f, landingTargetInst_.transform.position.z);
        }
    }
    

}

