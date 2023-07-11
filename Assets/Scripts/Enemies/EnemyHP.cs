using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : HPController
{
    private void Reset()
    {
        this.thresholdHP = 40f;
        this.currentHP = 40f;
    }
}
