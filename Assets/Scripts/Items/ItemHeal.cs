using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : MonoBehaviour
{
    [SerializeField] private string objectCollisionTag;
    [SerializeField] private float healHP = 50f;

    private void Awake()
    {
        objectCollisionTag = TagCollection.Player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(objectCollisionTag))
        {
            PlayerHP.Instance.IncreaseHP(healHP);
            Destroy(transform.gameObject);
        }
    }
}
