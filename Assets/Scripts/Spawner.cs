using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] gameObjects;
    public float spawnEverySecond = 0.02f;
    float nextSpawn = 0;

    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnEverySecond;
            var randomObject = Random.Range(0, gameObjects.Length);
            Instantiate(gameObjects[randomObject], transform.position, Quaternion.identity);
        }
    }
}
