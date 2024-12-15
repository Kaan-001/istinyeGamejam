using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyAI : MonoBehaviour
{
    public Animator animator;
    public LayerMask Which; // Saldırı yapabileceği katman
    public float moveSpeed = 3f; // Hareket hızı
    private List<Transform> targetList; // Hedeflerin tutulduğu liste
    private Transform closestTarget; // En yakın hedef
    public GameObject GizmodPos;
    public float attackRange = 0.5f; // Saldırı menzili
    public AudioSource punching;
    public AudioClip punch;
    void Start()
    {
        animator = GetComponent<Animator>();
        ChoosingEnemy();
    }

    public void ChoosingEnemy()
    {
        // Hedef listesini oluştur ve PlayerTeam tag'ine sahip nesneleri ekle
        

        targetList = new List<Transform>();
        
        GameObject[] targets = GameObject.FindGameObjectsWithTag("PlayerTeam");

        foreach (GameObject target in targets)
        {
            targetList.Add(target.transform);
        }
        // En yakın hedefi bul
        closestTarget = FindClosestTarget();

        // Eğer en yakın hedef bulunursa, hareket fonksiyonunu çağır
        if (closestTarget != null)
        {
            StartCoroutine(MoveToTarget());
        }
    }
    public void ChoosingEnemyRandomly()
    {
        // Hedef listesini oluştur ve PlayerTeam tag'ine sahip nesneleri ekle
        targetList = new List<Transform>();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("PlayerTeam");

        foreach (GameObject target in targets)
        {
            targetList.Add(target.transform);
        }
        // En yakın hedefi bul
        closestTarget = GetRandomTarget();

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

        // Rastgele seçilen GameObject'i döndür
        return targetList[randomIndex].transform;
    }
    // En yakın hedefi bulan fonksiyon
    private Transform FindClosestTarget()
    {
        Transform nearest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform target in targetList)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = target;
            }
        }

        return nearest;
    }
    // Hedefe doğru hareket eden fonksiyon
    private IEnumerator MoveToTarget()
    {
        while (closestTarget != null)
        {
            // Hedefe doğru hareket
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);
            animator.Play("Walk");
            Vector2 Pos = closestTarget.position;
            if (this.transform.position.x - Pos.x < 0)
            {
                FaceDirection(Vector2.right);
            }
            else
            {
                FaceDirection(Vector2.left);
            }
            // Hedefe ulaştıysan döngüden çık
            if (Vector2.Distance(transform.position, closestTarget.position) < 1f)
            {
                Debug.Log("Hedefe ulaşıldı!");
                AttackToEnemy();
                break;
            }

            yield return null; // Bir sonraki frame'e kadar bekle
        }
    }
    void AttackToEnemy()
    {
        
        StartCoroutine(Punching());

    }
    IEnumerator Punching()
    {
        Punch();
        animator.Play("Attack");
        punching.PlayOneShot(punch);
        yield return new WaitForSeconds(0.5f); // Yumruk animasyon süresi kadar bekle
        animator.Play("Idle");
        ChoosingEnemyRandomly();
    }
    void Punch()
    {
        Vector2 punchPosition = GizmodPos.transform.position;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPosition, attackRange, Which);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Düşman birine hasar verdi !!");
            
            if (enemy.GetComponent<Enemy>()) 
            {
                enemy.GetComponent<Enemy>().TakeDamage(10);
            
            }
            else 
            {
            enemy.GetComponent<PlayerHealth>().TakeDamage(10);
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
