using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData => playerData;

    private float horizontal;
    private float speed;
    private float jumpingPower;
    private float extraJumpingPower;
    public bool isFacingRight = true;

    private bool doubleJump = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower;
    private float dashingTime;
    private float dashingCooldown;
    [SerializeField] private TrailRenderer tr;

    private bool isWallSliding;
    private float wallSlidingSpeed;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime;
    private float wallJumpingCounter;
    private float wallJumpingDuration;
    [SerializeField] private Vector2 wallJumpingPower;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private static PlayerMovement instance;
    public static PlayerMovement Instance { get => instance; }

    [SerializeField] public bool isKnockbacked = false;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackForce;
    [SerializeField] private SpriteRenderer srBody;
    private Color originalColor;

    private void Awake()
    {
        if (PlayerMovement.instance == null)
        {
            PlayerMovement.instance = this;
        }

        GetPlayerData();
    }

    private void GetPlayerData()
    {
        if (PlayerData)
        {
            speed = PlayerData.movementSpeed;
            jumpingPower = PlayerData.jumpingPower;
            extraJumpingPower = PlayerData.extraJumpingPower;

            dashingPower = PlayerData.dashingPower;
            dashingTime = PlayerData.dashingTime;
            dashingCooldown = PlayerData.dashingCooldown;

            wallSlidingSpeed = PlayerData.wallSlidingSpeed;

            wallJumpingTime = PlayerData.wallJumpingTime;
            wallJumpingDuration = PlayerData.wallJumpingDuration;
            wallJumpingPower = PlayerData.wallJumpingPower;

            knockbackTime = PlayerData.knockbackTime;
            knockbackForce = PlayerData.knockbackForce;
        }
    }

    public void ResetStatus()
    {
        srBody.color = originalColor;
        isKnockbacked = false;
        canDash = true;
        isDashing = false;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            StartCoroutine(Dash());
        }

        JumpAction();

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

        if (isKnockbacked)
        {
            rb.velocity = new Vector2(knockbackForce, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void JumpAction()
    {

        if (IsGrounded() && !Input.GetKey(KeyCode.X))
        {
            doubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.playerJump, 1f, true);
            if (doubleJump || IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJump ? extraJumpingPower : jumpingPower);
                doubleJump = !doubleJump;
            }

            //if (rb.velocity.y > 0f)
            //{
            //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            //}
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.2f, wallLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        AudioManager.Instance.PlaySound((int)AudioManager.SoundEnum.playerDash, 0.8f);

        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    public void Knockback()
    {
        isKnockbacked = true;
        originalColor = srBody.color;
        srBody.color = Color.cyan;
        rb.AddForce(transform.up * 6f, ForceMode2D.Impulse);

        StartCoroutine(Unknockback());
    }

    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(knockbackTime);
        isKnockbacked = false;
        srBody.color = originalColor;
    }
}