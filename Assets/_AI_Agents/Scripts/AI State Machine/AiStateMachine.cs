using UnityEngine;

public class AiStateMachine
{

    public AiState[] states;
    public AiAgentController aiAgent;
    public AiStateId currentState;

    public AiStateMachine(AiAgentController aiAgent)
    {
        this.aiAgent = aiAgent;
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        states = new AiState[numStates];
    }

    public void RegisterState(AiState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public AiState GetState(AiStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(aiAgent);
    }


    public void ChangeState(AiStateId newState)
    {
        GetState(currentState)?.Exit(aiAgent);

        currentState = newState;

        GetState(currentState)?.Enter(aiAgent);
    }
}
