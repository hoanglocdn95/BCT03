using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject closeAttack;
    private GameObject superCloseAttack;
    private GameObject attackObject;
    private bool isAttack = false;
    private float attackTime = 0f;
    private float attackInterval = 0.25f;

    void Start()
    {
        closeAttack = transform.GetComponentInChildren<CloseAttack>().gameObject;
        superCloseAttack = transform.GetComponentInChildren<SuperCloseAttack>().gameObject;
        closeAttack.SetActive(false);
        superCloseAttack.SetActive(false);
        GetAttackObject("CloseAttack");
    }

    public void GetAttackObject(string nameObject)
    {
        switch(nameObject)
        {
            case "CloseAttack":
                attackObject = closeAttack;
                break;
            case "SuperCloseAttack":
                attackObject = superCloseAttack;
                break;
            default:
                attackObject = closeAttack;
                break;
        }
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
                if (attackObject)
                {
                    attackObject.SetActive(isAttack);
                }
            }
        }
    }

    private void Attack()
    {
        isAttack = true;
        if (attackObject)
        {
            AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.playerAttack);
            attackObject.SetActive(true);
        }
    }
}
