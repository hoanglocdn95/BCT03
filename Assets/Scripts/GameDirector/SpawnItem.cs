using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : SpawnPoint
{
    [SerializeField] private List<Transform> itemPrefabs;
    [SerializeField] private Transform Holder;
    public List<Transform> ItemPrefabs { get => itemPrefabs; }
    public List<Transform> ListSpawnPoint { get => this.listSpawnPoint; }
    [SerializeField] private int itemAmountMax = 5;
    [SerializeField] private int itemCount = 0;

    private void Awake()
    {
        LoadSpawnItem();
    }

    private void LoadSpawnItem()
    {
        Transform TopLeft = transform.Find("TopLeft");
        foreach (Transform spawnItem in TopLeft)
        {
            this.listSpawnPoint.Add(spawnItem);
        }

        Transform TopRight = transform.Find("TopRight");
        foreach (Transform spawnItem in TopRight)
        {
            this.listSpawnPoint.Add(spawnItem);
        }

        Transform BottomLeft = transform.Find("BottomLeft");
        foreach (Transform spawnItem in BottomLeft)
        {
            this.listSpawnPoint.Add(spawnItem);
        }

        Transform BottomRight = transform.Find("BottomRight");
        foreach (Transform spawnItem in BottomRight)
        {
            this.listSpawnPoint.Add(spawnItem);
        }
    }

    public void SpawnRandomItemAt(Vector3 point)
    {
        Transform item = itemPrefabs[this.RandomIndex(itemPrefabs.Count)];

        Transform tf = Instantiate(item);
        tf.gameObject.SetActive(false);
        tf.position = point;
        tf.gameObject.SetActive(true);
        tf.SetParent(Holder);

        itemCount++;
    }

    public void DecreaseEnemyCount()
    {
        itemCount--;
    }

    public bool AllowSpawnItem()
    {
        return itemCount <= itemAmountMax;
    }
}
