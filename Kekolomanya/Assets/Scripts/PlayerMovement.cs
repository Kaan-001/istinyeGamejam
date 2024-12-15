using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // Hareket hızı
    private Vector2 movement;   // Hareket vektörü
    private Rigidbody2D rb;     // Rigidbody bileşeni

    private Animator animator;  // Animator bileşeni
    private SpriteRenderer spriteRenderer; // Sprite Renderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Sprite Renderer'ı al
        animator = GetComponent<Animator>(); // Animator bileşenini al
    }

    void Update()
    {
        // Hareket girişi
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Karakterin yönünü kontrol et (Flip işlemi)
        if (movement.x < 0)
        {
            FaceDirection(Vector2.left); // Sola dön
        }
        else if (movement.x > 0)
        {
            FaceDirection(Vector2.right); // Sağa dön
        }

        // Animasyon kontrolü
        AnimationControl();
    }

    void FixedUpdate()
    {
        // Rigidbody ile pozisyon hareketi
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void AnimationControl()
    {
        // Eğer hareket varsa "walk" animasyonu, yoksa "Idle" animasyonu oynat
        if (movement.x != 0 || movement.y != 0)
        {
            animator.Play("walk");
        }
        else
        {
            animator.Play("Idle");
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Anim());
        }
    }

    public IEnumerator Anim()
    {
        animator.Play("Attack");
        yield return new WaitForSeconds(5f);
    }

    void FaceDirection(Vector2 direction)
    {
        // Karakterin yönünü değiştirmek için Sprite'ı çevir
        if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Sağa dön
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Sola dön
        }
    }
}
