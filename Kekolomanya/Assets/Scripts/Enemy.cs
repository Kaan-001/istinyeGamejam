using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Düşman hasar aldı! Kalan sağlık: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Düşman öldü!");
        Destroy(gameObject); // Düşmanı yok et
    }
}
