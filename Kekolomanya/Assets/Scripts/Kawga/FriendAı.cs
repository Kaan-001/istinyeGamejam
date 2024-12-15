using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAı : MonoBehaviour
{
    public float moveSpeed = 3f; // Hareket hızı
    public Vector2 minBounds;   // Minimum x ve y sınırları
    public Vector2 maxBounds;   // Maksimum x ve y sınırları
    private Vector2 targetPosition; // Hedef pozisyon
    public LayerMask Which;
    public float attackRange = 0.5f; // Saldırı menzili
    private List<Transform> targetList;
    private Transform closestTarget; // En yakın hedef
    public GameObject GizmodPos;
    private Transform RandomTargetPosition;
    void Start()
    {
        // İlk hedefi belirle
        SetRandomTargetPosition();
    }

    // Rastgele bir hedef pozisyon belirler
    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        targetPosition = new Vector2(randomX, randomY);
        Debug.Log($"Yeni hedef pozisyon: {targetPosition}");
        StartCoroutine(MoveToRandomPos());
    }

    // Hedefe doğru hareket eder
    private IEnumerator MoveToRandomPos()
    {
        while (targetPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            //FLİP KODU BURAYA GELİCEK

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                ChoosingEnemyRandomly();
                break;
            }
            yield return null;
        }
    }

    public void ChoosingEnemyRandomly()
    {
        // Hedef listesini oluştur ve PlayerTeam tag'ine sahip nesneleri ekle
        targetList = new List<Transform>();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("EnemyTeam");

        foreach (GameObject target in targets)
        {
            targetList.Add(target.transform);
        }
        // En yakın hedefi bul
        RandomTargetPosition = GetRandomTarget();

        // Eğer en yakın hedef bulunursa, hareket fonksiyonunu çağır
        if (closestTarget != null)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    private Transform GetRandomTarget()
    {
        // Eğer listede eleman yoksa null döndür
        if (targetList == null || targetList.Count == 0)
        {
            Debug.LogWarning("Target listesi boş!");
            return null;
        }

        // Rastgele bir indeks seç
        int randomIndex = Random.Range(0, targetList.Count);
        Debug.Log(randomIndex);

        // Rastgele seçilen GameObject'i döndür
        return targetList[randomIndex].transform;
    }

    private IEnumerator MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, GetRandomTarget().position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            AttackToEnemy();
        }
        yield return null;
    }

    void AttackToEnemy()
    {

        StartCoroutine(Punching());

    }

    IEnumerator Punching()
    {
        Punch();
        //animator.Play("Attack");
        yield return new WaitForSeconds(1.5f); // Yumruk animasyon süresi kadar bekle
        //animator.Play("Idle");
        ChoosingEnemyRandomly();
    }

    void Punch()
    {
        Vector2 punchPosition = GizmodPos.transform.position;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPosition, attackRange, Which);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("KEKOYA HASAR birine hasar verdi !!");
            if (enemy.GetComponent<Enemy>())
            {
                enemy.GetComponent<Enemy>().TakeDamage(10);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 punchPosition = GizmodPos.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchPosition, attackRange);
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
