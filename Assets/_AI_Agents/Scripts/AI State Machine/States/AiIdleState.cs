using UnityEngine;

public class AiIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.IDLE;
    }


    public void Enter(AiAgentController agent)
    {
     
        
    }

    public void Update(AiAgentController agent)
    {
        //Basic FOV Check
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.aiAgentConfig.maxSightDistance) 
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0.0f) 
        {
            
            agent.aiStateMachine.ChangeState(AiStateId.CHASE);
        }
    }

    public void Exit(AiAgentController agent)
    {

    } 
}
