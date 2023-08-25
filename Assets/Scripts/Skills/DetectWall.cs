using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour
{
    private BoxCollider2D bc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = false;
        transform.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!collision.transform.CompareTag(TagCollection.Player))
        //{
            var dr = collision.transform.GetComponent<DamageReceiver>();
            if(dr)
            {
                dr.ReceiveDamage(20f);
            }

            Transform bulletFx = FxManager.Instance.Spawn(transform.parent.position, new Quaternion(), 1);
            bulletFx.gameObject.SetActive(true);
            FxManager.Instance.Spawn(transform.parent.position, new Quaternion(), 0);

            SkillManager.Instance.PutObjectInPool(transform.parent);
        //}
    }
}
