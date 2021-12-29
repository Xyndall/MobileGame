using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] _objects;

    private void Start()
    {
        int rand = Random.Range(0, _objects.Length);
        GameObject instance = Instantiate(_objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }




}
