public enum AIState
{
    Wander,
    Seek,
    Flee
}
public class StateMachine
{
    public AIState currentState;

    public StateMachine(AIState initialState)
    {
        currentState = initialState;
    }

    public void ChangeState(AIState newState)
    {
        currentState = newState;
    }
}
