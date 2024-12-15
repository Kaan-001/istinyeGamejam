using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 500f; // Speed of the bullet in pixels per second

    void Update()
    {
        // Move the bullet upwards (in Canvas space)
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the enemy if it collides with the bullet
        if (collision.gameObject.CompareTag("EnemyTeam"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
