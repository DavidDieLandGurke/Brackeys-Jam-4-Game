using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] gameObjects;
    public int count;
    private int _countLeft;
    public int id;

    void Start()
    {
        _countLeft = count;
        GameEvents.onSpawn += Spawn;
        Spawn(id);
    }
    private void OnDestroy()
    {
        GameEvents.onSpawn -= Spawn;
    }

    void Spawn(int id)
    {
        if (id == this.id)
        {
            if(_countLeft > 0)
            {
                var randomObject = UnityEngine.Random.Range(0, gameObjects.Length);
                var theObject = gameObjects[randomObject];
                var newEnemy = Instantiate(theObject, transform.position, Quaternion.identity);
                if (newEnemy.GetComponentInChildren<Enemy>() != null)
                {
                    newEnemy.GetComponentInChildren<Enemy>().id = id;
                }
                else
                {
                    newEnemy.GetComponentInChildren<Enemies>().id = id;
                }
                _countLeft--;
            }
        }
    }

    //void Update()
    //{
    //    if(Time.time > nextSpawn)
    //    {
    //        nextSpawn = Time.time + spawnEverySecond;
    //        var randomObject = Random.Range(0, gameObjects.Length);
    //        Instantiate(gameObjects[randomObject], transform.position, Quaternion.identity);
    //    }
    //}
}
