using UnityEngine;

public class AIAgentRagdoll : MonoBehaviour
{

    public Rigidbody[] ragdollRigidBodies;
    Animator agentAnimator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();
        agentAnimator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidBody in ragdollRigidBodies) 
        {
            rigidBody.isKinematic = true;        
        }
        agentAnimator.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach(var rigidBody in ragdollRigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        agentAnimator.enabled = false;  
    }
}
