using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class SpawnPoint : MonoBehaviour
{
    [SerializeField] protected List<Transform> listSpawnPoint;

    protected Vector3 PointSpawn()
    {
        int indexPoint = RandomIndex(listSpawnPoint.Count - 1);
        return listSpawnPoint[indexPoint].position;
    }

    protected int RandomIndex(int limit)
    {
        return UnityEngine.Random.Range(0, limit);
    }
}
