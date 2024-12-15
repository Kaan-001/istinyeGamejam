using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NPCFighter : MonoBehaviour
{
    public Animator animator;
    public LayerMask Which; // Saldırı yapabileceği katman
    private Character character;
    public GameObject[] Enemys; // Tüm düşmanlar
    public List<GameObject> enemies = new List<GameObject>(); // Filtrelenmiş düşmanlar
    public GameObject currentTarget; // Şu anki hedef
    public Transform currentGoTarget; // Hedefin yakın child'ı
    public bool isAttacking = false, OncePunch = true, RandomWhere = false;
    public float moveSpeed = 3f; // Hareket hızı
    public float attackRange = 0.5f; // Saldırı menzili

    public GameObject GizmodPos;


    //Min and Max Konrdinatlar
    public float minX = -7f; // Minimum x koordinatı
    public float maxX = 7f;  // Maksimum x koordinatı
    public float minY = -3f;  // Minimum y koordinatı
    public float maxY = 1.50f;   // Maksimum y koordinatı



    void Start()
    {
        // Karakter bileşenini al
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();

        // Tüm düşmanları bul ve filtrele
        Enemys = GameObject.FindGameObjectsWithTag("Fighter");

        foreach (var enemy in Enemys)
        {
            // Aynı takımda olmayan düşmanları listeye ekle
            if (character.team != enemy.GetComponent<Character>().team)
            {
                enemies.Add(enemy);
            }
        }

        ChoosingEnemy();
    }

    public void ChoosingEnemy()
    {
        // Ölü düşmanları listeden çıkar
        enemies.RemoveAll(enemy => enemy == null || enemy.GetComponent<Character>().dead);
        int randomChooseNum = UnityEngine.Random.Range(0, enemies.Count);
        // Eğer düşman listesi boşsa işlem yapılmaz
        if (enemies.Count == 0)
        {
            Debug.LogWarning("Düşman listesi boş!");
            return;
        }
        if (enemies.Count > 0 && enemies.Contains(enemies[randomChooseNum]))
        {
            currentTarget = enemies[randomChooseNum];
        }
        // En yakın düşmanı seç


        if (currentTarget != null)
        {
            GoToEnemy();
        }
    }

    GameObject GetClosestEnemy()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    void GoToEnemy()
    {
        if (currentTarget == null)
        {
            Debug.LogWarning("Geçerli bir hedef yok!");
            return;
        }

        if (this.transform.position.x - currentTarget.transform.position.x < 0)
        {
            // Düşman sağda, sağa dön
            SelectClosestChild();
           // Sağ yöne dön
        }
        else
        {
            // Düşman solda, sola dön
            SelectClosestChild();
           // Sol yöne dön
        }
    }

    void SelectClosestChild()
    {
        if (currentTarget == null || currentTarget.transform.childCount < 2)
        {
            Debug.LogWarning("Hedef geçerli değil veya yeterli child yok!");
            return;
        }

        // İki child'ın mesafesini hesapla ve en yakınını seç
        Transform child1 = currentTarget.transform.GetChild(0);
        Transform child2 = currentTarget.transform.GetChild(1);

        float distanceToChild1 = Vector3.Distance(transform.position, child1.position);
        float distanceToChild2 = Vector3.Distance(transform.position, child2.position);

        currentGoTarget = distanceToChild1 < distanceToChild2 ? child1 : child2;
        StartCoroutine(GoCurrentTarget());
    }

    public IEnumerator GoCurrentTarget()
    {
        int Wow = UnityEngine.Random.Range(0, 3);

        if (this.transform.position.x - currentTarget.transform.position.x < 0)
        {
            FaceDirection(Vector2.right);
        }
        else
        {
            FaceDirection(Vector2.left);
        }

        switch (Wow)
        {
            case 0:
                while (Vector2.Distance(transform.position, currentGoTarget.position) > 0.1f)
                {
                    animator.Play("Walk");
                    transform.position = Vector2.MoveTowards(transform.position, currentGoTarget.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
               
                yield return new WaitForSeconds(1f);
                AttackToEnemy();



                break;
            case 1:
                if (this.transform.position.x - currentTarget.transform.position.x < 0)
                {
                    FaceDirection(Vector2.right);
                }
                else
                {
                    FaceDirection(Vector2.left);
                }
                while (Vector2.Distance(transform.position, currentGoTarget.position) > 0.1f)
                {
                    animator.Play("Walk");
                    transform.position = Vector2.MoveTowards(transform.position, currentGoTarget.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }

               
                yield return new WaitForSeconds(1f);
                AttackToEnemy();


                break;
            case 2:
                Vector2 Pos = SetRandomTargetPosition();
                if (this.transform.position.x - Pos.x < 0)
                {
                    FaceDirection(Vector2.right);
                }
                else
                {
                    FaceDirection(Vector2.left);
                }
                while (Vector2.Distance(transform.position, Pos) > 0.1f)
                {
                    animator.Play("Walk");
                    transform.position = Vector2.MoveTowards(transform.position, Pos, moveSpeed * Time.deltaTime);
                    yield return null;
                }
               
                yield return new WaitForSeconds(2f);
                ChoosingEnemy();
                break;
        }

        // ara sıra Vuracak Ara sıra kacacak

    }

    public Vector2 SetRandomTargetPosition()
    {
        // Rastgele bir hedef pozisyon seç
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    void AttackToEnemy()
    {

        StartCoroutine(Punching());

    }

    IEnumerator Punching()
    {
        Punch();
        animator.Play("Attack");
        yield return new WaitForSeconds(1.5f); // Yumruk animasyon süresi kadar bekle
        animator.Play("Idle");
        ChoosingEnemy();
    }

    void Punch()
    {
        Vector2 punchPosition = GizmodPos.transform.position;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPosition, attackRange, Which);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Character>().TakeDamage(10);
            Debug.Log("Düşman birine hasar verdi !!");
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