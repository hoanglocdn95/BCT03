using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skills : MonoBehaviour
{
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected Transform player;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(TagCollection.DamageReceiver))
        {
            var dr = collision.GetComponentInChildren<DamageReceiver>();
            dr.ReceiveDamage(this.damage);

            Transform bulletFx = FxManager.Instance.Spawn(transform.position, new Quaternion(), 1);
            bulletFx.gameObject.SetActive(true);
        }
    }

}
