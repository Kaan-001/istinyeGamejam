using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Vector2 movement;
    private Rigidbody2D rb;

    // Dash Sistemi
    public float dashSpeed = 13f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    // Sprite kontrolü için
    private SpriteRenderer spriteRenderer;

    // Punch Range için Child Transform
    public Transform punchRangeTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Sprite Renderer'ı al
    }

    void Update()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                movement = Vector2.zero;
            }
        }
        else
        {
            // Hareket girişi
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Flip kontrolü
            if (movement.x < 0)
            {
                spriteRenderer.flipX = true; // Sola bak
                FlipPunchRange(true); // Punch Range'i de sola döndür
            }
            else if (movement.x > 0)
            {
                spriteRenderer.flipX = false; // Sağa bak
                FlipPunchRange(false); // Punch Range'i sağa döndür
            }

            // Animasyon ve efekt kontrolü (isteğe bağlı)
            if (movement.x != 0 || movement.y != 0)
            {
                //particle.Play(); animator.Play("Run");
            }
            else if (movement.x == 0 || movement.y == 0)
            {
                //animator.Play("idle"); particle.Stop();
            }

            // Dash kontrolü
            if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
            {
                StartCoroutine(Dash());
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (isDashing ? dashSpeed : moveSpeed) * Time.fixedDeltaTime);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        yield return null;
    }

    // Punch Range'i döndür
    void FlipPunchRange(bool isFlipped)
    {
        if (punchRangeTransform != null)
        {
            Vector3 localScale = punchRangeTransform.localScale;
            localScale.x = isFlipped ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
            punchRangeTransform.localScale = localScale;
        }
    }
}
