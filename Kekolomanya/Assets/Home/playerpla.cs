using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerpla : MonoBehaviour
{
    public float moveSpeed = 4f; // Hareket h�z�
    private Vector2 movement;   // Hareket vekt�r�
    private Rigidbody2D rb;     // Rigidbody bile�eni

    private Animator animator;  // Animator bile�eni
    private SpriteRenderer spriteRenderer; // Sprite Renderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Sprite Renderer'� al
        animator = GetComponent<Animator>(); // Animator bile�enini al
    }

    void Update()
    {
        

            // Hareket giri�i
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Karakterin y�n�n� kontrol et (Flip i�lemi)
            if (movement.x < 0)
            {
                FaceDirection(Vector2.left); // Sola d�n
            }
            else if (movement.x > 0)
            {
                FaceDirection(Vector2.right); // Sa�a d�n
            }

            // Animasyon kontrol�
            AnimationControl();
        
    }

    void FixedUpdate()
    {
        // Rigidbody ile pozisyon hareketi
        if (!PlayerPunch.attackanim) rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void AnimationControl()
    {
        // E�er hareket varsa "walk" animasyonu, yoksa "Idle" animasyonu oynat
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
        // Karakterin y�n�n� de�i�tirmek i�in Sprite'� �evir
        if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Sa�a d�n
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Sola d�n
        }
    }
}
