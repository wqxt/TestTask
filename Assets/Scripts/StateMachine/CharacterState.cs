using UnityEngine;

public abstract class CharacterState
{
    private protected CharacterStateMachine _stateMachine;
    private protected Character _character;
    public Transform PlayerTransform { get; set; }

    public CharacterState(Character character, CharacterStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _character = character;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void HandleInput() { }

}