using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HPController
{
    private static PlayerHP instance;
    public static PlayerHP Instance { get => instance; }

    private void Awake()
    {
        if (PlayerHP.instance == null)
        {
            PlayerHP.instance = this;
        }
    }

    private void Reset()
    {
        this.thresholdHP = 100f;
        this.currentHP = 100f;
    }

    protected override float GetDirection()
    {
        return PlayerMovement.Instance.isFacingRight ? 1f : -1f;
    }
}