using UnityEngine;

public class AiAgentHitBox : MonoBehaviour
{

    public AiAgentHealth agentHealth;


    public void OnRaycastHit(WeaponController weapon, Vector3 direction)
    {
        agentHealth.TakeDamage(weapon.damage, direction);
    }
}
