using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSpawner : MonoBehaviour
{
    
    public GameObject frogPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<9; i++)
        {
        float random_x = Random.Range(-11,-14);
        float random_z = Random.Range(-19,-33);
        Vector3 randomPos = new Vector3(random_x, -10, random_z);
        Instantiate(frogPrefab, randomPos, Quaternion.identity);
        }
    
    }

   
}
