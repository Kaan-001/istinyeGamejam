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
    public bool canSelectable = true;
    public Team team; // Karakterin takımı
    public int health = 100; // Sağlık değeri
    public bool dead=false;
    public void TakeDamage(int damage, GameObject Focus)
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
        dead = true;
    }
}
