using UnityEngine;
using UnityEngine.AI;

public class AiAgentLocomotion : MonoBehaviour
{
    //Components
    NavMeshAgent navMeshAgent;
    Animator agentAnimator;

    private void Awake()
    {
        //Get Attached Components 
        navMeshAgent = GetComponent<NavMeshAgent>();
        agentAnimator = GetComponent<Animator>();
    }

    public void UpdateAgentLocomotion()
    {
        if (navMeshAgent.hasPath)
        {
            
            agentAnimator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }
        else
        {
            

            agentAnimator.SetFloat("Speed", 0);
        }
    }

    
}
