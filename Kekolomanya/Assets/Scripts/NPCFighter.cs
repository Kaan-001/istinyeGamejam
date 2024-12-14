using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
public class NPCFighter : MonoBehaviour
{
 
    private Character character;
    public GameObject[] Enemys;
    public List<GameObject> enemies;
    public GameObject currentTarget;
    public Transform currentGoTarget;
    public bool isAttacking, OncePunch;
    // bütün oyuncuları buluyor 
    void Start()
    {
        character = GetComponent<Character>();
        
        Enemys = GameObject.FindGameObjectsWithTag("Fighter");

        for (int i = 0; i < Enemys.Length; i++)
        {
          
            if (!enemies.Contains(Enemys[i]) && character.team != Enemys[i].GetComponent<Character>().team) 
            {
                enemies.Add(Enemys[i]);
            }
        }
        ChoosingEnemy();

    }
    public void ChoosingEnemy()
    {
        int randomChooseNum = UnityEngine.Random.Range(0, enemies.Count);

        if (enemies.Count > 0 && enemies[randomChooseNum].GetComponent<Character>().canSelectable ) 
        {
            enemies[randomChooseNum].GetComponent<Character>().canSelectable = true;

            currentTarget = enemies[randomChooseNum]; 
        }

        if(currentTarget!=null) GoToEnemy();
       
    }
    void GoToEnemy() 
    {
        if (this.transform.position.x-currentTarget.transform.position.x < 0) 
        {
            SelectClosestChild();
        //düşmanım sağda
        }
        else 
        {
            //düşmanım solda
            SelectClosestChild();
        }
        // current targetin 2 childindan en yakın olan child seçilir ve o child a gidilir
    }

    // 1. En yakın child'ı seç
    void SelectClosestChild()
    {
        // Eğer currentTarget atanmadıysa hata ver ve işlemi durdur
        if (currentTarget == null)
        {
            Debug.LogWarning("currentTarget atanmadı!");
            return;
        }

        // currentTarget'ın child sayısını kontrol et
        if (currentTarget.transform.childCount < 2)
        {
            Debug.LogWarning("currentTarget'ın yeterli sayıda child'ı yok!");
            return;
        }

        // Child'lara erişim
        Transform child1 = currentTarget.transform.GetChild(0);
        Transform child2 = currentTarget.transform.GetChild(1);

        // Mesafeleri hesapla
        float distanceToChild1 = Vector2.Distance(transform.position, child1.position);
        float distanceToChild2 = Vector2.Distance(transform.position, child2.position);

        // En yakın olan child'ı seç
        currentGoTarget = distanceToChild1 < distanceToChild2 ? child1 : child2;

       
    }
    void MoveAndAttack()
    {

        // Pozisyona hareket
        if (!isAttacking) 
        {
            transform.position = Vector3.MoveTowards(transform.position, currentGoTarget.position, Time.deltaTime * 5f);
        }
      

        // Eğer saldırı mesafesine ulaşıldıysa saldır
        //saldiri mesafesi üret
        if (Vector3.Distance(transform.position, currentGoTarget.position) <= 0.1f)
        {
          
            AttackToEnemy();
            // Saldırı işlemleri burada yapılabilir
        }
        
    }
    public void Update()
    {
        MoveAndAttack();
    }
    void AttackToEnemy() 
    {
        isAttacking = true;
        if (OncePunch) 
        {
        }
        //vurma animasyonu

    }
    IEnumerator Punching() 
    {
        Punch();
        yield return new WaitForSeconds(1.5f);
        OncePunch = true;
        isAttacking = false;
    }
    void Punch()
    {
        // Animasyonu oynat
        //if (animator != null)
        //{
        //animator.SetTrigger("Punch");
        //}

        // Yumruğun isabet ettiği hedefleri kontrol et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Düşmanlara hasar ver
            enemy.GetComponent<Enemy>()?.TakeDamage(5);
        }
    }

    // Yumruğun menzilini görsel olarak göstermek için
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
