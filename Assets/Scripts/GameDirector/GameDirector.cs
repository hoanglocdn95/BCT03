using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private static GameDirector instance;
    public static GameDirector Instance { get => instance; }

    public SpawnPlayer spawnPlayer;
    public SpawnEnemy spawnEnemy;
    public SpawnItem spawnItem;

    private List<Vector3> pointSpawnedList = new List<Vector3>();

    private enum spawnTypeEnum
    {
        enemy,
        item
    }

    private Camera mainCamera;

    private void Awake()
    {
        if (GameDirector.instance == null)
        {
            GameDirector.instance = this;
        }
    }

    private void Start()
    {
        InitComponent();
    }

    private void Update()
    {
        if (spawnEnemy.AllowSpawnEnemy())
        {
            DetectSpawnPointInCamera((int)spawnTypeEnum.enemy);
        }

        if (spawnItem.AllowSpawnItem())
        {
            DetectSpawnPointInCamera((int)spawnTypeEnum.item);
        }
    }

    private void InitComponent()
    {
        spawnPlayer = GetComponentInChildren<SpawnPlayer>();
        spawnEnemy = GetComponentInChildren<SpawnEnemy>();
        spawnItem = GetComponentInChildren<SpawnItem>();
        mainCamera = Camera.main;
    }

    private void DetectSpawnPointInCamera(int spawnTypeIndex)
    {
        if (spawnTypeIndex == (int)spawnTypeEnum.enemy)
        {
            foreach (Transform point in spawnEnemy.ListSpawnPoint)
            {
                if (IsPointInCamera(point))
                {
                    if (IsPointAvailable(point.position))
                    {
                        pointSpawnedList.Add(point.position);
                        spawnEnemy.SpawnRandomEnemyAt(point.position);
                    }
                }
            }
        }

        if (spawnTypeIndex == (int)spawnTypeEnum.item)
        {
            foreach (Transform point in spawnItem.ListSpawnPoint)
            {
                if (IsPointInCamera(point))
                {
                    if (IsPointAvailable(point.position))
                    {
                        pointSpawnedList.Add(point.position);
                        spawnItem.SpawnRandomItemAt(point.position);
                    }
                }
            }
        }
    }

    private bool IsPointInCamera(Transform point)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(point.position);

        if (viewportPoint.x >= 0f && viewportPoint.x <= 1f && viewportPoint.y >= 0f && viewportPoint.y <= 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsPointAvailable(Vector3 pointCheck)
    {
        if (pointSpawnedList.Count == 0) return true;

        foreach (Vector3 point in pointSpawnedList)
        {
            if (pointCheck == point)
            {
                return false;
            }
        }

        return true;
    }
}
