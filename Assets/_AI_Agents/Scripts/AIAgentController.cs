using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AIAgentController : MonoBehaviour
{

    //Components 
    private AIAgentLocomotion aiAgentLocomotion;

    private void Awake()
    {
        aiAgentLocomotion = GetComponent<AIAgentLocomotion>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        aiAgentLocomotion.PathfindToPlayer();
    }



}
