using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable Object/Player Input Reader")]
public class PlayerInputReader : ScriptableObject, PlayerInput.IPlayerActions
{
    private PlayerInput _playerInput;
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action<bool> LookClickEvent;
    public event Action JumpEvent;


    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.SetCallbacks(this);
            _playerInput.Enable();
            Debug.Log("PlayerInput enable");
        }
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            JumpEvent?.Invoke();
        }
    }


    public void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }


    public void OnLookClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            LookClickEvent?.Invoke(true);

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            LookClickEvent?.Invoke(false);

        }
    }
}
