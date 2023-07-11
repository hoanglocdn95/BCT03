using UnityEngine;
using System.Collections;

public class BulletDespawn : MonoBehaviour
{
    private static BulletDespawn instance;
    public static BulletDespawn Instance { get => instance; }

    private void Awake()
    {
        if (BulletDespawn.instance == null)
        {
            BulletDespawn.instance = this;
        }
    }
}
