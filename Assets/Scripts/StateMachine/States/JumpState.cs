using UnityEngine;

public class JumpState : CharacterState
{
    private float _jumpHeight;

    public JumpState(Character character, CharacterStateMachine stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {

        if (_character.Data._jumpCount < _character.Data._jumpAmount)
        {
            _character.Data._jumpCount++;
        }

        _character.Animator.SetBool("Jump", true);
        Jump();
    }

    public override void Exit()
    {
        _character.Animator.SetBool("Jump", false);
        _character.Data._isJump = false;
    }

    public override void LogicUpdate()
    {
        if (_character.PlayerTransform.position.y >= _jumpHeight)
        {
            _stateMachine.ChangeState(_character._airState);
        }
    }

    public override void PhysicsUpdate()
    {
        Vector3 moveDirection = GetCameraRelativeDirection();
        _character.Data._velocity = moveDirection * _character.Data._currentSpeed + Vector3.up * _character.Data._gravityVelocity.y;
        CheckRotation(moveDirection);

        _character.CharacterController.Move(_character.Data._velocity * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _character.Data._gravityVelocity.y = Mathf.Sqrt(_character.Data._jumpForce * -2f * _character.Data._gravityValue);
        _jumpHeight = _character.PlayerTransform.position.y + _character.Data._jumpHeight;
        _character.Data._velocity = new Vector3(_character.Data._direction.x * _character.Data._jumpLength, _character.Data._gravityVelocity.y, 0);
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