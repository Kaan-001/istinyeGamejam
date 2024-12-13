using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum Team
    {
        PlayerTeam,
        EnemyTeam
    }

    public Team team; // Karakterin takımı
    public int health = 100; // Sağlık değeri

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " hasar aldı! Kalan sağlık: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " öldü!");
        Destroy(gameObject); // Karakteri sahneden kaldır
    }
}
