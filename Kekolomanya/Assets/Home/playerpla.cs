using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerpla : MonoBehaviour
{
    public float moveSpeed = 4f; // Hareket hýzý
    private Vector2 movement;   // Hareket vektörü
    private Rigidbody2D rb;     // Rigidbody bileþeni

    private Animator animator;  // Animator bileþeni
    private SpriteRenderer spriteRenderer; // Sprite Renderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Sprite Renderer'ý al
        animator = GetComponent<Animator>(); // Animator bileþenini al
    }

    void Update()
    {
        

            // Hareket giriþi
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Karakterin yönünü kontrol et (Flip iþlemi)
            if (movement.x < 0)
            {
                FaceDirection(Vector2.left); // Sola dön
            }
            else if (movement.x > 0)
            {
                FaceDirection(Vector2.right); // Saða dön
            }

            // Animasyon kontrolü
            AnimationControl();
        
    }

    void FixedUpdate()
    {
        // Rigidbody ile pozisyon hareketi
        if (!PlayerPunch.attackanim) rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void AnimationControl()
    {
        // Eðer hareket varsa "walk" animasyonu, yoksa "Idle" animasyonu oynat
        if (!PlayerPunch.attackanim)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.Play("walk");
            }
            else if (movement.x == 0 || movement.y == 0)
            {
                animator.Play("Idle");
            }
        }
        else
        {
            animator.Play("Attack");
        }
    }
    void FaceDirection(Vector2 direction)
    {
        // Karakterin yönünü deðiþtirmek için Sprite'ý çevir
        if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Saða dön
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Sola dön
        }
    }
}
