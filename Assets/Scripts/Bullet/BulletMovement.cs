using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float distanceDespawn = 10f;
    private Vector3 direction = Vector3.right;
    private Vector3 initPosition;

    private void Awake()
    {
        initPosition = transform.parent.position;
        direction = PlayerMovement.Instance.isFacingRight ? Vector3.right : Vector3.left;
    }

    private void Start()
    {
        Rigidbody2D rbParent = transform.parent.GetComponent<Rigidbody2D>();
        if (rbParent)
        {
            rbParent.velocity = direction * bulletSpeed;
        }
        else
        {
            Debug.Log($"BulletMovement---Error: Rigidbody2D not found");
        }
    }

    void FixedUpdate()
    {
        DespawnByDistance();
    }

    public void DespawnByDistance()
    {
        float distance = Vector3.Distance(initPosition, transform.parent.position);
        if (distance > distanceDespawn)
        {
            ShootingController.Instance.bulletCount--;
            Destroy(transform.parent.gameObject);
        }
    }
}
