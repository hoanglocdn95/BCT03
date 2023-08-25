using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public void ReceiveDamage(float amount)
    {
        var enemyHP = transform.parent.GetComponentInChildren<EnemyHP>();
        var enemyMovement = transform.parent.GetComponent<EnemyMovement>();
        var enemyFly = transform.parent.GetComponent<EnemyFly>();

        if (enemyMovement)
        {
            if (enemyHP && !enemyMovement.isKnockbacked)
            {
                enemyMovement.Knockback();
                enemyHP.DecreaseHP(amount);
                AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.enemyHit);
                GameDirector.Instance.spawnEnemy.DecreaseEnemyCount();
                return;
            }
        }

        if (enemyFly)
        {
            if (enemyHP)
            {
                enemyFly.Knockback();
                enemyHP.DecreaseHP(amount);
                AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.enemyHit);
                GameDirector.Instance.spawnEnemy.DecreaseEnemyCount();
                return;
            }
        }
    }
}
