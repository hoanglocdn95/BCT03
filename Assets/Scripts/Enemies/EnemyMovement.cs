using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float agro = 8f;

    [SerializeField] private float moveSpeed = 4f;
    public bool isFacingRight = true;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target == null) return;

        float heightDistance = Mathf.Abs(transform.position.y - target.position.y);
        if (transform.position.x == target.position.x || heightDistance > 2f)
        {
            StopChasing();
            return;
        }

        if (GetDistanceToTarget() <= agro && heightDistance <= 2f)
        {
            Chasing();
        }
    }

    private float GetDistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    private void Chasing()
    {
        if (transform.position.x > target.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);

            if (isFacingRight)
            {
                Flip();
            }
        }
        else if (transform.position.x < target.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            if (!isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void StopChasing()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
