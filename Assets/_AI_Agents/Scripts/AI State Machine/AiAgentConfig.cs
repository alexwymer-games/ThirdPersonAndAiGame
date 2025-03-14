using UnityEngine;

[CreateAssetMenu(fileName = "AiAgentConfig", menuName = "ScriptableObjects/AiAgentConfig")]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float dieForce = 0.75f;
    public float maxSightDistance = 5.0f;
}
