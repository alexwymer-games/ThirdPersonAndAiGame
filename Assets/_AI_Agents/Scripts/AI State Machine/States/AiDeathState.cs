using UnityEngine;

public class AiDeathState : AiState
{

    public Vector3 direction;

    public AiStateId GetId()
    {
        return AiStateId.DEATH;
    }


    public void Enter(AiAgentController agent)
    {
        agent.agentRagdoll.ActivateRagdoll();

        direction.y = 0.5f;
        agent.agentRagdoll.ApplyForce(direction * agent.aiAgentConfig.dieForce);

        agent.agentHealthBar.gameObject.SetActive(false);
        agent.skinnedMesh.updateWhenOffscreen = true;
    }

    public void Update(AiAgentController agent)
    {

    }

    public void Exit(AiAgentController agent)
    {
        
    }

    

    
}
