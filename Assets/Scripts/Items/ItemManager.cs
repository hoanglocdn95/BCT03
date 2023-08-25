using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private string objectCollisionTag;
    [SerializeField] private float healHP;
    [SerializeField] private ItemData itemData;
    public ItemData ItemData => itemData;

    private void Awake()
    {
        objectCollisionTag = TagCollection.Player;
        healHP = ItemData.amountHeal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(objectCollisionTag))
        {
            AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.itemPickup, 1f);

            PlayerSkill.Instance.SetItem(ItemData.indexItem);

            PlayerHP.Instance.IncreaseHP(healHP);
            Destroy(transform.gameObject);
        }
    }
}
