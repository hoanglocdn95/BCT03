using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject closeAttack;
    private bool isAttack = false;
    private float attackTime = 0f;
    private float attackInterval = 0.25f;

    void Start()
    {
        closeAttack = transform.GetComponentInChildren<CloseAttack>().gameObject;
        closeAttack.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isAttack)
        {
            Attack();
        }

        if (isAttack)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= attackInterval)
            {
                attackTime = 0;
                isAttack = false;
                if (closeAttack)
                {
                    closeAttack.SetActive(isAttack);
                }
            }
        }
    }

    private void Attack()
    {
        isAttack = true;
        if (closeAttack)
        {
            closeAttack.SetActive(true);
        }
    }
}
