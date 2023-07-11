using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{

    public void ReceiveDamage(float amount)
    {
        var enemyHP = transform.parent.GetComponentInChildren<EnemyHP>();

        if (enemyHP)
        {
            enemyHP.DecreaseHP(amount);
            return;
        }

        var playerHP = transform.parent.GetComponentInChildren<PlayerHP>();

        if (playerHP)
        {
            enemyHP.DecreaseHP(amount);
            return;
        }
    }
}
