using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private float healHP = 50f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            PlayerHP.Instance.IncreaseHP(healHP);
            Destroy(transform.gameObject);
        }
    }
}
