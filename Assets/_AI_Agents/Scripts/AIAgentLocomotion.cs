using UnityEngine;
using UnityEngine.AI;

public class AIAgentLocomotion : MonoBehaviour
{
    [Header("Agent Settings")]
    public Transform playerTransform;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;


    //Components
    NavMeshAgent navMeshAgent;
    Animator agentAnimator;

    //Private Variables 
    float pathTimer = 0.0f;

    private void Awake()
    {
        //Get Attached Components 
        navMeshAgent = GetComponent<NavMeshAgent>();
        agentAnimator = GetComponent<Animator>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PathfindToPlayer()
    {
        //Update Timer
        pathTimer -= Time.deltaTime;

        if (pathTimer < 0.0f)
        {
            //Reset Destination - Avoid Sqrt Check
            float sqDistance = (playerTransform.position - navMeshAgent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                navMeshAgent.destination = playerTransform.position;
            }

            //Reset Timer
            pathTimer = maxTime;
        }

        //Update Animator Values
        agentAnimator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
}
