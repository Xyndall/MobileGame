using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{

    public LayerMask WhatIsRoom;
    public LevelGeneration levelGen;

    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, WhatIsRoom);
        if(roomDetection == null && levelGen._stopGenertaiion == true)
        { // spawn random room
            int rand = Random.Range(0, levelGen._rooms.Length);
            Instantiate(levelGen._rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
