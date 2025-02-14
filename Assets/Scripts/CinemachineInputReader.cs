using UnityEngine;
using Cinemachine;

public class CinemachineInputReader : MonoBehaviour, AxisState.IInputAxisProvider
{
    [SerializeField] private PlayerInputReader _inputReader;
    private Vector2 _lookInput; 
    private bool _canLook = false;
    private void OnEnable()
    {
        _inputReader.LookEvent += OnLook;  
        _inputReader.LookClickEvent += OnClick;  
    }

    private void OnDisable()
    {
        _inputReader.LookEvent -= OnLook;  
        _inputReader.LookClickEvent -= OnClick;
    }


    private void OnLook(Vector2 look)
    {
        if (_canLook)  
        {
            _lookInput = look;  
        }
    }

    private void OnClick(bool isPressed)
    {
        _canLook = isPressed;  
    }

    public float GetAxisValue(int axis)
    {
        if (!_canLook)  
        {
            return 0;
        }

        switch (axis)
        {
            case 0:
                return _lookInput.x; 
            case 1:
                return _lookInput.y; 
            default:
                return 0;
        }
    }
}

