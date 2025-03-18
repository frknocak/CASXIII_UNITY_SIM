using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject ROV;
    public Terrain terrain;
    public float minPositionX = 53f;
    public float maxPositionX = 220f;
    public float minPositionZ = 114f;
    public float maxPositionZ = 283f;

    void Awake()
    {
        PlaceObjectOnTerrain();
    }

    void PlaceObjectOnTerrain()
    {
        Vector3 terrainSize = terrain.terrainData.size;

        float randomX = Random.Range(minPositionX, maxPositionX);
        float randomZ = Random.Range(minPositionZ, maxPositionZ);


        float y = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.GetPosition().y + 6f; // Arac�n sahnenin alt�nda kalmamas� ve bug olmamas� ad�na 6 de�eri eklendi


        ROV.transform.position = new Vector3(randomX, y, randomZ);

    }
}
