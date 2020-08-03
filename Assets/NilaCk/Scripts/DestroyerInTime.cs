using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerInTime : MonoBehaviour
{
    public int destroyedAfter;

    void Start()
    {
        StartCoroutine(Destroyer());
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(destroyedAfter);
        Destroy(gameObject);
    }
}
