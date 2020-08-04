using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event Action<int> onSpawn;
    public static void NotNull(int id)
    {
        if (onSpawn != null)
        {
            onSpawn.Invoke(id);
        }
    }
}
