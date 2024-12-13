using System.Collections;
using UnityEngine;

public class EnemyAı : MonoBehaviour
{
    public Transform player; // Oyuncunun transformu
    public float moveSpeed = 3f; // Düşmanın hareket hızı
    public float attackRange = 1.5f; // Yumruk atma menzili
    public int punchDamage = 10; // Yumruk hasarı
    public float attackCooldown = 1f; // Saldırı arasındaki süre
    private float attackCooldownTimer = 0f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Eğer animasyon kullanıyorsanız
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // Oyuncuya doğru hareket
        if (!isAttacking && Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

            // Yüzün yönünü ayarla
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Sola bak
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // Sağa bak
            }

            // Animasyon kontrolü
            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }
        else if (!isAttacking && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (attackCooldownTimer <= 0)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        // Animasyon oynat (isteğe bağlı)
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Oyuncuya hasar ver
        yield return new WaitForSeconds(0.3f); // Saldırının gecikme süresi (animasyona bağlı olarak ayarlayın)
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(punchDamage);
            }
        }

        // Saldırı soğumasını başlat
        attackCooldownTimer = attackCooldown;
        isAttacking = false;
    }
}
