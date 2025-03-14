using UnityEngine;

public class AiAgentRagdoll : MonoBehaviour
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

    public void ApplyForce(Vector3 force)
    {
        var rigidBody = agentAnimator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
