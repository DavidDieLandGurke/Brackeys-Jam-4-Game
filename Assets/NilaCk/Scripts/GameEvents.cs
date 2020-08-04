using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event Action onSpawn;
    public static void NotNull()
    {
        if (onSpawn != null)
        {
            onSpawn.Invoke();
        }
    }
}
