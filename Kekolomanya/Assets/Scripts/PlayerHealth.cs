using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    public GameObject GameOverPanel;

    //public Text HealthText;

    void Start()
    {
        currentHealth = maxHealth;
        //HealthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Playerın Canı :  " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
       if(GameOverPanel!=null) GameOverPanel.SetActive(true); Time.timeScale = 0f; 
    }
}
