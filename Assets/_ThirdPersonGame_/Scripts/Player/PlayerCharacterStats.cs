using UnityEngine;

public class PlayerCharacterStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private int currentStamina;
    [SerializeField] private int maxStamina;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Health Functions
    private void RegenerateHealth()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Death");
            Die();
        }
    }

    private void Die()
    {

    }


    //Stamina Functions 
    private void RegenerateStamina()
    {

    }

    
}
