using UnityEngine;

public class AirState : CharacterState
{
    public AirState(Character character, CharacterStateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        _character.Data._isAir = true;
        _character.Animator.SetBool("Jump", true);
    }

    public override void Exit()
    {
        _character.Animator.SetBool("Jump", false);
        _character.Data._isAir = false;
        _character.Data._isJump = false; //  reset  jump if  was a double jump
    }

    public override void LogicUpdate()
    {
        if (_character.Data._isJump && _character.Data._jumpCount < _character.Data._jumpAmount) // for double jump
        {
            _stateMachine.ChangeState(_character._jumpState);
        }


        if (_character.Data._isGrounded)
        {
            if (_character.Data._direction.x != 0 || _character.Data._direction.y != 0)
            {
                _stateMachine.ChangeState(_character._runState);

            }
            else
            {
                _stateMachine.ChangeState(_character._idleState);
            }

        }
    }

    public override void PhysicsUpdate()
    {

        Vector3 moveDirection = GetCameraRelativeDirection();

        if (_character.Data._gravityVelocity.y > _character.Data._gravityValue)
        {
            _character.Data._gravityVelocity.y += _character.Data._gravityValue * _character.Data._gravityScale * Time.fixedDeltaTime;
        }
        else
        {
            _character.Data._gravityVelocity.y = _character.Data._gravityValue;
        }

        if (_character.Data._currentSpeed < _character.Data._maxSpeed)
        {
            _character.Data._currentSpeed += _character.Data._acceleration;
        }

        _character.Data._velocity = moveDirection * _character.Data._currentSpeed + Vector3.up * _character.Data._gravityVelocity.y;


        CheckRotation(moveDirection);

        _character.CharacterController.Move(_character.Data._velocity * Time.fixedDeltaTime);
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

    private void CheckRotation(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }
}