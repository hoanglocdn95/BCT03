using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : Spawner
{
    private static FxManager instance;
    public static FxManager Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (FxManager.instance == null)
        {
            FxManager.instance = this;
        }
    }
}
