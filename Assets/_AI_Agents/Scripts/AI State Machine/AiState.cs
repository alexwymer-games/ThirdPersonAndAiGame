using UnityEngine;

public enum AiStateId
{
    IDLE,
    CHASE,
    DEATH
}

public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgentController agent);
    void Update(AiAgentController agent);
    void Exit(AiAgentController agent);
}
