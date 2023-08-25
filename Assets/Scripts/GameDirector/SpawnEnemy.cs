using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : SpawnPoint
{
    [SerializeField] private List<Transform> enemyPrefabs;
    [SerializeField] private Transform Holder;
    public List<Transform> EnemyPrefabs { get => enemyPrefabs;  }
    public List<Transform> ListSpawnPoint { get => this.listSpawnPoint; }
    [SerializeField] private int enemyAmountMax = 5;
    [SerializeField] private int enemyCount = 0;

    private void Awake()
    {
        LoadSpawnEnemy();
    }

    private void LoadSpawnEnemy()
    {
        Transform TopLeft = transform.Find("TopLeft");
        foreach (Transform spawnEnemy in TopLeft)
        {
            this.listSpawnPoint.Add(spawnEnemy);
        }

        Transform TopRight = transform.Find("TopRight");
        foreach (Transform spawnEnemy in TopRight)
        {
            this.listSpawnPoint.Add(spawnEnemy);
        }

        Transform BottomLeft = transform.Find("BottomLeft");
        foreach (Transform spawnEnemy in BottomLeft)
        {
            this.listSpawnPoint.Add(spawnEnemy);
        }

        Transform BottomRight = transform.Find("BottomRight");
        foreach (Transform spawnEnemy in BottomRight)
        {
            this.listSpawnPoint.Add(spawnEnemy);
        }
    }

    public void SpawnRandomEnemyAt(Vector3 point)
    {
        //Transform enemy = enemyPrefabs[1];
        Transform enemy = enemyPrefabs[this.RandomIndex(enemyPrefabs.Count)];

        Transform tf = Instantiate(enemy);

        tf.gameObject.SetActive(false);
        tf.position = point;
        tf.gameObject.SetActive(true);
        tf.SetParent(Holder);
        enemyCount++;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }

    public bool AllowSpawnEnemy()
    {
        return enemyCount <= enemyAmountMax;
    }
}
