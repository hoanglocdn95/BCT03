using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private static GameDirector instance;
    public static GameDirector Instance { get => instance; }

    public SpawnPlayer spawnPlayer;

    private void Awake()
    {
        if (GameDirector.instance == null)
        {
            GameDirector.instance = this;
        }

        spawnPlayer = GetComponentInChildren<SpawnPlayer>();
        Debug.Log($"spawnPlayer: {spawnPlayer}");
    }
}
