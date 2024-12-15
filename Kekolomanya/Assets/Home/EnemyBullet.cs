using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyBullet : MonoBehaviour
{
    public float speed = 500f; // Speed of the bullet in pixels per second
    private void Start()
    {
        Destroy(gameObject, 1.4f);
    }
    void Update()
    {
        // Move the bullet upwards (in Canvas space)
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Vector2.down* speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the enemy if it collides with the bullet
        if (collision.gameObject.CompareTag("PlayerTeam"))
        {
            Destroy(collision.gameObject);
            // Destroy the enemy
            Destroy(gameObject);
            // Sahne yeniden Olur
            SceneManager.LoadScene(0);

            
        }
    }

}
