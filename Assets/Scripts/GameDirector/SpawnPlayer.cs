using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : SpawnPoint
{
    [SerializeField] private Transform Player;

    private void Awake()
    {
        LoadSpawnPlayer();
    }

    private void LoadSpawnPlayer()
    {
        foreach (Transform spawnPlayer in transform)
        {
            this.listSpawnPoint.Add(spawnPlayer);
        }
    }

    public void SpawnPlayerRandom()
    {
        Player.gameObject.SetActive(false);
        Player.position = PointSpawn();
        Player.gameObject.SetActive(true);
    }
}
