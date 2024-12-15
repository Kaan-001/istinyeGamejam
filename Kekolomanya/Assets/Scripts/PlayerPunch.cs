using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public float punchRange = 1.5f; // Yumruğun menzili
    public int punchDamage = 10; // Yumruk hasarı
    public LayerMask enemyLayer; // Düşmanların olduğu katman
    public Animator animator; // Karakterin Animator bileşeni
    public static bool attackanim = false;
<<<<<<< HEAD
    public AudioSource punching;
    public AudioClip punch;
=======
    public int diedEnemies = 0;

    public GameObject WinningPanel;

>>>>>>> 48c36ac86617f52cace2574a2959fab04854b56d
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attackanim && KekoWcScene.Fight ) // Yumruk atmak için Space tuşu
        {
            Punch();
           
        }
        
    }
    public IEnumerator Anim()
    {
        attackanim = true;
        punching.PlayOneShot(punch);
        //Yumruk sesi

        yield return new WaitForSeconds(0.5f);
        attackanim = false;
    }

    void Punch()
    {
        // Animasyonu oynat
        //if (animator != null)
        //{
        //animator.SetTrigger("Punch");
        //}

        // Yumruğun isabet ettiği hedefleri kontrol et
        StartCoroutine(Anim());
        Vector2 punchPosition = transform.position + transform.right * 1f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPosition, punchRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Düşmanlara hasar ver
            enemy.GetComponent<Enemy>().TakeDamage(10);
            if(enemy.GetComponent<Enemy>().health <= 0)
            {
                diedEnemies++;
            }

            if(diedEnemies == 2)
            {
                WinningPanel.SetActive(true);
            }
        }
    }

    // Yumruğun menzilini görsel olarak göstermek için
    private void OnDrawGizmosSelected()
    {
        Vector2 punchPosition = transform.position + transform.right * 1f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchPosition, punchRange);
    }
}
