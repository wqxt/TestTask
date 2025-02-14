public class CharacterStateMachine
{
    public CharacterState _currentState { get; set; }

    public void Initialize(CharacterState startState)
    {
        _currentState = startState;
        _currentState.Enter();

    }

    public void ChangeState(CharacterState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}