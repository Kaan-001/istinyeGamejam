using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public float punchRange = 1.5f; // Yumruğun menzili
    public int punchDamage = 10; // Yumruk hasarı
    public LayerMask enemyLayer; // Düşmanların olduğu katman
    //public Animator animator; // Karakterin Animator bileşeni

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Yumruk atmak için Space tuşu
        {
            Punch();
        }
    }

    void Punch()
    {
        // Animasyonu oynat
        //if (animator != null)
        //{
        //animator.SetTrigger("Punch");
        //}

        // Yumruğun isabet ettiği hedefleri kontrol et
        Vector2 punchPosition = transform.position + transform.right * 1f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPosition, punchRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Düşmanlara hasar ver
            enemy.GetComponent<Character>().TakeDamage(100);
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
