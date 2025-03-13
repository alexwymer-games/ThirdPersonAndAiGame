using UnityEngine;

public class AIAgentHealth : MonoBehaviour
{

    public int maxHealth;
    [HideInInspector] public float currentHealth;

    AIAgentRagdoll agentRagdoll;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agentRagdoll = GetComponent<AIAgentRagdoll>();

        currentHealth = maxHealth;

        //Add Hitbox component 
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            AIAgentHitBox hitbox = rigidBody.gameObject.AddComponent<AIAgentHitBox>();
            hitbox.agentHealth = this;
        }
    }


    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0.0f) 
        {
            Die();
        }
    }

    private void Die()
    {   
        agentRagdoll.ActivateRagdoll();
    }

}
