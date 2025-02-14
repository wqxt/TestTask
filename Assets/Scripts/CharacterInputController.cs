using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _inputReader;
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        _inputReader.JumpEvent += HandleJump;
        _inputReader.MoveEvent += HandleMove;
    }

    private void OnDisable()
    {
        _inputReader.JumpEvent -= HandleJump;
        _inputReader.MoveEvent -= HandleMove;
    }

    private void HandleJump()
    {
        _character.Data._isJump = true;
    }

    private void HandleMove(Vector2 input)
    {
        _character.Data._direction = input;
        _character.Data._isRun = true;
    }
}