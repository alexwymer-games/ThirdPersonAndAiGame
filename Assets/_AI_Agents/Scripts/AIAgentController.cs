using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AiAgentController : MonoBehaviour
{

    //Components 
    private AiAgentLocomotion aiAgentLocomotion;
    private AiAgentHealth aiAgentHealth;

    
    public AiAgentConfig aiAgentConfig;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public AiAgentRagdoll agentRagdoll;
    [HideInInspector] public SkinnedMeshRenderer skinnedMesh;
    [HideInInspector] public AgentHealthBar agentHealthBar;

    public Transform playerTransform;

    //[HideInInspector] public 

    //State Machine 
    [Header("State Machine Settings")]
    [HideInInspector] public AiStateMachine aiStateMachine;
    public AiStateId initialAiState;


    private void Awake()
    {
        //Get Attached Components
        aiAgentLocomotion = GetComponent<AiAgentLocomotion>();
        aiAgentHealth = GetComponent<AiAgentHealth>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        agentRagdoll = GetComponent<AiAgentRagdoll>();
        skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        agentHealthBar = GetComponentInChildren<AgentHealthBar>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        aiStateMachine = new AiStateMachine(this);
        aiStateMachine.RegisterState(new AiChasePlayerState());
        aiStateMachine.RegisterState(new AiDeathState());
        aiStateMachine.ChangeState(initialAiState);
    }

    private void Update()
    {
        aiStateMachine.Update();

        aiAgentLocomotion.UpdateAgentLocomotion();

        aiAgentHealth.UpdateAgentHealth();
    }

    public void SetPlayerTargetReference(Transform _playerTransform)
    {
        playerTransform = _playerTransform;
    }
}
