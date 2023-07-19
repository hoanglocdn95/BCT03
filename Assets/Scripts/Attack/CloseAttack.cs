using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : MonoBehaviour
{
    [SerializeField] private float damage = 40f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(TagCollection.DamageReceiver))
        {
            var dr = collision.GetComponentInChildren<DamageReceiver>();
            dr.ReceiveDamage(damage);
        }
    }
}
