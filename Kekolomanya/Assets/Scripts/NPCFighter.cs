using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NPCFighter : MonoBehaviour
{
    public LayerMask Which; // Saldırı yapabileceği katman
    private Character character;
    public GameObject[] Enemys; // Tüm düşmanlar
    public List<GameObject> enemies = new List<GameObject>(); // Filtrelenmiş düşmanlar
    public GameObject currentTarget; // Şu anki hedef
    public Transform currentGoTarget; // Hedefin yakın child'ı
    public bool isAttacking = false, OncePunch = true,RandomWhere=false;
    public float moveSpeed = 3f; // Hareket hızı
    public float attackRange = 0.2f; // Saldırı menzili

    //Min and Max Konrdinatlar
    public float minX = -7f; // Minimum x koordinatı
    public float maxX = 7f;  // Maksimum x koordinatı
    public float minY = -3f;  // Minimum y koordinatı
    public float maxY = 1.50f;   // Maksimum y koordinatı
   


    void Start()
    {
        // Karakter bileşenini al
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

        // En yakın child'ı seç
        SelectClosestChild();
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
        switch (Wow)
        {
        case 0:
                while (Vector2.Distance(transform.position, currentGoTarget.position) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, currentGoTarget.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
                AttackToEnemy();

                

                break;
        case 1:
                while (Vector2.Distance(transform.position, currentGoTarget.position) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, currentGoTarget.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
                AttackToEnemy();

                
                break;
        case 2: Vector2 Pos = SetRandomTargetPosition(); 
                
                while (Vector2.Distance(transform.position, Pos) > 0.1f)
                {
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
        yield return new WaitForSeconds(1.5f); // Yumruk animasyon süresi kadar bekle
        ChoosingEnemy();
    }

    void Punch()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, Which);

        foreach (Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<Enemy>()?.TakeDamage(5);
            Debug.Log("Hasar verildi");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
