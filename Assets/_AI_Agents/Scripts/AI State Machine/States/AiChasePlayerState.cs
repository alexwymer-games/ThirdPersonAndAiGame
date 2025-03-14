using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AiChasePlayerState : AiState
{

    //State Variables 
    float pathTimer = 0.0f;

    public AiStateId GetId()
    {
        return AiStateId.CHASE;
    }


    public void Enter(AiAgentController agent)
    {
       
    }

    public void Update(AiAgentController agent)
    {
       PathfindToPlayer(agent);
    }

    public void Exit(AiAgentController agent)
    {

    }


    public void PathfindToPlayer(AiAgentController agent)
    {

        Debug.Log("Pathfind");

        if (!agent.enabled)
        {
            return;
        }

        //Update Timer
        pathTimer -= Time.deltaTime;

        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }
       
        if (pathTimer < 0.0f)
        {
            Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0;

            if (direction.sqrMagnitude > agent.aiAgentConfig.maxDistance * agent.aiAgentConfig.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.position;
                }
            }
            //Reset Timer
            pathTimer = agent.aiAgentConfig.maxTime;
        }
    }
}
