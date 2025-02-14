using UnityEngine;

public class RunState : CharacterState
{
    public RunState(Character character, CharacterStateMachine stateMachine) : base(character, stateMachine) {}

    public override void Enter()
    {
        _character.Data._jumpCount = 0;
        _character.Data._isRun = true;
        _character.Data._gravityVelocity.y = _character.Data._gravityValue;
    }

    public override void HandleInput()
    {
        _character.Animator.SetFloat("Direction", _character.Data._currentAnimationBlendvector.magnitude);
    }


    public override void Exit()
    {
        _character.Data._isRun = false;
        _character.Data._gravityVelocity = Vector2.zero;
    }

    public override void LogicUpdate()
    {
        if (_character.Data._direction.x == 0 && _character.Data._direction.y == 0)
        {
            _character._stateMachine.ChangeState(_character._idleState);
        }

        if (_character.Data._isJump )
        {
            _stateMachine.ChangeState(_character._jumpState);
        }

        if (!_character.Data._isGrounded)
        {
            _stateMachine.ChangeState(_character._airState);
        }
    }

    public override void PhysicsUpdate()
    {

        Vector3 moveDirection = GetCameraRelativeDirection();

        CheckRotation(moveDirection);

        _character.Data._velocity = moveDirection * _character.Data._currentSpeed + Vector3.up * _character.Data._gravityVelocity.y;
        SetSpeed();

        _character.CharacterController.Move(_character.Data._velocity * Time.fixedDeltaTime);
    }

    private void CheckRotation(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    private void SetSpeed()
    {
        if (_character.Data._currentSpeed < _character.Data._maxSpeed)
        {
            _character.Data._currentSpeed += _character.Data._acceleration;
        }
    }
    private Vector3 GetCameraRelativeDirection()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return (right * _character.Data._direction.x) + (forward * _character.Data._direction.y);
    }
}