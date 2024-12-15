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
    public GameObject punchPosition;
    public AudioSource punching;
    public AudioClip punch;
    public GameObject[] enemyobj;
    public static int enemcount;

    public int diedEnemies = 0;

    public GameObject WinningPanel;


    private void Start()
    {
        enemyobj = GameObject.FindGameObjectsWithTag("EnemyTeam");

        enemcount= enemyobj.Length;

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

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPosition.transform.position, punchRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Düşmanlara hasar ver
            enemy.GetComponent<Enemy>().TakeDamage(10);

           
        }
    }

    // Yumruğun menzilini görsel olarak göstermek için
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchPosition.transform.position, punchRange);
    }
    public void FixedUpdate()
    {
        if (enemcount == 0) 
        {
            WinningPanel.SetActive(true);
            
        }
    }
}
