using UnityEngine;

public class AIAgentHitBox : MonoBehaviour
{

    public AIAgentHealth agentHealth;


    public void OnRaycastHit(WeaponController weapon, Vector3 direction)
    {
        agentHealth.TakeDamage(weapon.damage);
    }
}
