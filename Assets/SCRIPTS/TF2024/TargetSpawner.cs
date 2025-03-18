using UnityEngine;
using UnityEngine.UI;


public class TargetSpawner : MonoBehaviour
{
    public GameObject shootingTargetPrefab1;
    public GameObject shootingTargetPrefab2;
    public GameObject shootingTargetPrefab3;

    public GameObject shootingTargetInst_1;
    public GameObject shootingTargetInst_2;
    public GameObject shootingTargetInst_3;


    public Transform targetPos1;
    public Transform targetPos2;
    public Transform targetPos3;

    public Vector3 offset1;
    public Vector3 offset2;




    public int received_toggle;
    public int poolSizeBigOrLarge=1;

    void Start()
    {
        offset1 = new Vector3 (7,0,4);
        offset2 = new Vector3 (-7,0,-2);

        if(poolSizeBigOrLarge == 0){
        Vector3 randomPos1 = new Vector3(Random.Range(-20,15),(-24.9f),Random.Range(-40,50));
        Vector3 randomPos2 = new Vector3(Random.Range(-20,15),(-24.9f),Random.Range(-40,50));
        Vector3 randomPos3 = new Vector3(Random.Range(-20,15),(-24.9f),Random.Range(-40,50));
        }else
        if(poolSizeBigOrLarge == 1){
            int random = Random.Range(0,3);

            if(random == 0 ){
            shootingTargetPrefab1.transform.position = targetPos1.position;
            shootingTargetPrefab1.SetActive(true);

            shootingTargetPrefab2.transform.position = targetPos1.position + offset1;
            shootingTargetPrefab2.SetActive(true);

            shootingTargetPrefab3.transform.position = targetPos1.position + offset2;
            shootingTargetPrefab3.SetActive(true);

            }
            if(random == 1){
            shootingTargetPrefab1.transform.position = targetPos2.position + offset1;
            shootingTargetPrefab1.SetActive(true);

            shootingTargetPrefab2.transform.position = targetPos2.position;
            shootingTargetPrefab2.SetActive(true);

            shootingTargetPrefab3.transform.position = targetPos2.position + offset2;
            shootingTargetPrefab3.SetActive(true);

            }

            if(random == 2){
            shootingTargetPrefab1.transform.position = targetPos3.position;
            shootingTargetPrefab1.SetActive(true);

            shootingTargetPrefab2.transform.position = targetPos3.position + offset2;
            shootingTargetPrefab2.SetActive(true);

            shootingTargetPrefab3.transform.position = targetPos3.position + offset1;
            shootingTargetPrefab3.SetActive(true);

            }
        
    
        }
        
       
    }
    void Update(){
        poolSizeBigOrLarge = PlayerPrefs.GetInt("poolSizePref");
    }

    /*public void PoolSizePrefChangedMidGame(){
        received_toggle = PlayerPrefs.GetInt("randomizer");
        if(poolSizeBigOrLarge == 1){
        shootingTargetInst_.transform.position = new Vector3(shootingTargetInst_.transform.position.x, -29.9f, shootingTargetInst_.transform.position.z);

        }else if(poolSizeBigOrLarge == 0){
        shootingTargetInst_.transform.position = new Vector3(shootingTargetInst_.transform.position.x, -9.9f, shootingTargetInst_.transform.position.z);
        }
    }*/
    

}

