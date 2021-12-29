using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject[] TileGen;

    public int iterations = 20;
    float offset = 10.5f;

    void Generate()
    {

        for(int i = iterations; i >0; i--)
        {
            int selection = Random.Range(0, TileGen.Length);
            Vector3 spawnPos = new Vector3(i * offset, 0, 0);
            Instantiate(TileGen[selection], transform.position + spawnPos, Quaternion.identity);
        }

    }

    void Start()
    {
        Generate();
    }

    
}
