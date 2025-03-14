using UnityEngine;

public class AiAgentHealth : MonoBehaviour
{

    public int maxHealth;
    [HideInInspector] public float currentHealth;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    

    AiAgentController aiAgentController;
    SkinnedMeshRenderer skinnedMeshRenderer;

    AgentHealthBar healthBar;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aiAgentController = GetComponent<AiAgentController>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        healthBar = GetComponentInChildren<AgentHealthBar>();

        currentHealth = maxHealth;

        //Add Hitbox component 
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            AiAgentHitBox hitbox = rigidBody.gameObject.AddComponent<AiAgentHitBox>();
            hitbox.agentHealth = this;
        }
    }


    public void UpdateAgentHealth()
    {
        blinkTimer -= Time.deltaTime;

        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;

        skinnedMeshRenderer.material.color = Color.white * intensity;
    }


    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        currentHealth -= damageAmount;

        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);

        if (currentHealth <= 0.0f) 
        {
            Die(direction);
        }

        blinkTimer = blinkDuration;
    }

    private void Die(Vector3 direction)
    {   
       AiDeathState deathState = aiAgentController.aiStateMachine.GetState(AiStateId.DEATH) as AiDeathState;

        deathState.direction = direction;
        aiAgentController.aiStateMachine.ChangeState(AiStateId.DEATH);
    }

}
