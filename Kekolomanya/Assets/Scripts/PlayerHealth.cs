using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
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
        Debug.Log("Canın gitti dostum :  " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameOverPanel.SetActive(true);  
    }
}
