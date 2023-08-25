using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyFly : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool isChasing = false;

    private float moveSpeed = 0f;
    [SerializeField] private float moveSpeed_1 = 2.5f;
    [SerializeField] private float moveSpeed_2 = 5f;
    public bool isFacingRight = true;

    private Rigidbody2D rb;

    [SerializeField] public bool isKnockbacked = false;
    [SerializeField] private float knockbackTime = 0.5f;
    [SerializeField] private float knockbackForce = 2.5f;

    private Color originalColor;
    [SerializeField] private SpriteRenderer srBody;
    [SerializeField] private SpriteRenderer leftEye;
    [SerializeField] private SpriteRenderer rightEye;
    [SerializeField] private SpriteShapeRenderer mouth;

    private bool allowAttack = false;
    public bool AllowAttack { get => allowAttack; }

    private EnemyTrigger enemyTrigger;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.mass = 0f;
        enemyTrigger = GetComponentInChildren<EnemyTrigger>();
    }

    private void Update()
    {
        if (target == null) return;

        if (allowAttack)
        {
            StopChasing();
            return;
        };

        if (isChasing)
        {
            Chasing();
            TriggerMode(true);
        }
        else
        {
            StopChasing();
            TriggerMode(false);
        }

        if (enemyTrigger.isTriggered)
        {
            SetMovementSpeed(moveSpeed_2);
        }
        else
        {
            SetMovementSpeed(moveSpeed_1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(TagCollection.Player))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(TagCollection.Player))
        {
            isChasing = false;
        }
    }

    private void TriggerMode(bool isTrigger)
    {
        if (leftEye && rightEye && mouth)
        {
            leftEye.color = isTrigger ? Color.red : Color.white;
            rightEye.color = isTrigger ? Color.red : Color.white;
            mouth.color = isTrigger ? Color.red : Color.white;
        }
    }

    private void Chasing()
    {

        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (transform.position.x > target.position.x && isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x < target.position.x && !isFacingRight)
        {
            Flip();
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

    public void Knockback()
    {
        isKnockbacked = true;

        originalColor = srBody.color;
        srBody.color = Color.white;

        StartCoroutine(Unknockback());
    }

    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
        srBody.color = originalColor;
    }

    public void SetAllowAttack(bool isAllow)
    {
        allowAttack = isAllow;
    }

    public void SetMovementSpeed(float value)
    {
        moveSpeed = value;
    }
}
