using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float counterDelay = 0;
    private float counterAttackTime = 0f;

    // Close attack
    private CloseAttack closeAttack;

    private bool isAttack = false;
    private bool startAttack = false;
    [SerializeField] private float delayAttackTime = 1f;
    [SerializeField] private float attackInterval = 1f;

    // Trigger Attack
    private TriggerAttack triggerAttack;
    private bool isTriggerAttack = false;
    private bool startTriggerAttack = false;
    [SerializeField] private float delayTriggerAttackTime = 1f;
    [SerializeField] private float triggerAttackInterval = 2f;

    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyTrigger enemyTrigger;

    private void Awake()
    {
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
        enemyTrigger = transform.parent.GetComponentInChildren<EnemyTrigger>();

        closeAttack = transform.GetComponentInChildren<CloseAttack>();
        closeAttack.gameObject.SetActive(false);

        triggerAttack = transform.GetComponentInChildren<TriggerAttack>();
        triggerAttack.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (enemyTrigger.isTriggered && isAttack)
        {
            isAttack = false;
            isTriggerAttack = true;
        }

        if (!enemyTrigger.isTriggered && isTriggerAttack)
        {
            isAttack = true;
            isTriggerAttack = false;
        }
    }

    private void FixedUpdate()
    {
        if (startAttack)
        {
            counterAttackTime += Time.fixedDeltaTime;
            if (counterAttackTime >= attackInterval)
            {
                counterAttackTime = 0;
                startAttack = false;
                closeAttack.gameObject.SetActive(false);
            }
            return;
        }

        if (startTriggerAttack)
        {
            counterAttackTime += Time.fixedDeltaTime;
            if (counterAttackTime >= triggerAttackInterval)
            {
                counterAttackTime = 0;
                startTriggerAttack = false;
                triggerAttack.gameObject.SetActive(false);
            }
            return;
        }

        if (isTriggerAttack)
        {
            counterDelay += Time.fixedDeltaTime;
            if (counterDelay >= delayTriggerAttackTime)
            {
                counterDelay = 0;
                TriggerAttack();
            }
        }

        if (isAttack)
        {
            counterDelay += Time.fixedDeltaTime;
            if (counterDelay >= delayAttackTime)
            {
                counterDelay = 0;
                CloseAttack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagCollection.Player))
        {
            if (enemyTrigger.isTriggered)
            {
                isTriggerAttack = true;
            }
            else
            {
                isAttack = true;
            }
            enemyMovement.SetAllowAttack(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagCollection.Player))
        {
            isAttack = false;
            isTriggerAttack = false;
            counterDelay = 0;
            counterAttackTime = 0;
            enemyMovement.SetAllowAttack(false);
        }
    }

    private void CloseAttack()
    {
        startAttack = true;
        if (closeAttack)
        {
            AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.playerAttack);
            closeAttack.gameObject.SetActive(true);
        }
    }

    private void TriggerAttack()
    {
        startTriggerAttack = true;
        if (triggerAttack)
        {
            AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.playerAttack);
            triggerAttack.gameObject.SetActive(true);
        }
    }
}
