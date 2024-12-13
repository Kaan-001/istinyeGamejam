using System.Collections;
using System.Linq;
using UnityEngine;

public class NPCFighter : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    public float lowHealthThreshold = 20f;
    public float retreatDistance = 5f;
    public float retreatTime = 3f;

    // Hareket sınırları
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    // Kaçış noktası ve kaçış süresi
    private Vector3 retreatPosition;
    private bool isRetreating = false;
    private float retreatWaitTime = 3f;
    private float retreatTimer = 0f;

    private Transform target;
    private float attackCooldownTimer = 0f;
    private Character character;

    void Start()
    {
        character = GetComponent<Character>();
        ClampPosition();
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (isRetreating)
        {
            RetreatBehavior();
        }
        else
        {
            FightBehavior();
        }

    }

    void FightBehavior()
    {
        if (character.health <= lowHealthThreshold && !isRetreating)
        {
            StartRetreat();
        }

        if (target == null || target.GetComponent<Character>().health <= 0)
        {
            FindTarget();
        }

        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance > attackRange)
            {
                MoveTowardsTarget();
            }
            else if (attackCooldownTimer <= 0)
            {
                Attack();
            }
        }
    }

    void StartRetreat()
    {
        isRetreating = true;
        // Kaçış noktasını rastgele belirle
        retreatPosition = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            transform.position.z
        );
        Debug.Log(retreatPosition);
        retreatTimer = retreatWaitTime;
    }

    void RetreatBehavior()
    {
        // Kaçış noktasına doğru hareket et
        Vector2 direction = (retreatPosition - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

        // Kaçış noktasına ulaştığında bekleme
        if (Vector2.Distance(transform.position, retreatPosition) < 0.5f)
        {
            retreatTimer -= Time.deltaTime;

            if (retreatTimer <= 0)
            {
                // Bekleme süresi bittiğinde dövüşe geri dön
                isRetreating = false;
            }
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);  // Sol tarafa dönme
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);   // Sağ tarafa dönme
        }
    }

    void Attack()
    {
        attackCooldownTimer = attackCooldown;

        Character targetCharacter = target.GetComponent<Character>();
        if (targetCharacter != null)
        {
            targetCharacter.TakeDamage(attackDamage);
        }
    }

    void FindTarget()
    {
        Character[] potentialTargets = FindObjectsOfType<Character>()
            .Where(c => c.team != character.team && c.health > 0)
            .ToArray();

        if (potentialTargets.Length > 0)
        {
            target = potentialTargets[Random.Range(0, potentialTargets.Length)].transform;
        }
    }

    void ClampPosition()
    {
        // Mevcut pozisyonu sınırlar içinde tut
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
