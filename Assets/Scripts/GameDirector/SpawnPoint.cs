using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> listSpawnPoint;

    //private static SpawnPoint instance;
    //public static SpawnPoint Instance { get => instance; }

    //private void Awake()
    //{
    //    if (SpawnPoint.instance == null)
    //    {
    //        SpawnPoint.instance = this;
    //    }
    //}

    protected Vector3 PointSpawn()
    {
        int indexPoint = UnityEngine.Random.Range(0, listSpawnPoint.Count - 1);
        Debug.Log($"indexPoint: {indexPoint}");
        Debug.Log($"listSpawnPoint[indexPoint].position: {listSpawnPoint[indexPoint].position}");
        return listSpawnPoint[indexPoint].position;
    }
}
