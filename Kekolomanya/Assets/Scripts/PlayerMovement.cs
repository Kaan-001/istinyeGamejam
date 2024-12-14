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
    private Animator animator;
    // Sprite kontrolü için
    private SpriteRenderer spriteRenderer;

    // Punch Range için Child Transform
    public Transform punchRangeTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Sprite Renderer'ı al

        animator = GetComponent<Animator>();
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
                FaceDirection(Vector2.left); // Face left

            }
            else if (movement.x > 0)
            {
                FaceDirection(Vector2.right); // Face left
            }

            animationControl();
            // Animasyon ve efekt kontrolü (isteğe bağlı)


            // Dash kontrolü
            if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
            {
                StartCoroutine(Dash());
            }
        }
    }
    void animationControl() 
    {
        if (movement.x != 0 || movement.y != 0)
        {
            animator.Play("walk");
        }
        else
        {
            animator.Play("Idle");
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

    void FaceDirection(Vector2 direction)
    {
        // Yönü değiştirmek için rotayı ayarla
        if (direction == Vector2.right)
        {
            // Sağ yönü göster
            transform.rotation = Quaternion.Euler(0, 0, 0); // Yalnızca X-Y düzleminde döndürme
        }
        else if (direction == Vector2.left)
        {
            // Sol yönü göster
            transform.rotation = Quaternion.Euler(0, 180, 0); // 180 derece döndür
        }
    }
}
