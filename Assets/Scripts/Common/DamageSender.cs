using UnityEngine;

public class DamageSender : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(TagCollection.DamageReceiver))
        {
            var dr = collision.GetComponentInChildren<DamageReceiver>();
            dr.ReceiveDamage(damage);

            ShootingController.Instance.bulletCount--;
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(TagCollection.Player))
        {
            PlayerHP playerHP = collision.transform.GetComponentInChildren<PlayerHP>();
            if (playerHP)
            {
                playerHP.DecreaseHP(damage);
            }
        }
    }
}
