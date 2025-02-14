using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    internal protected CharacterStateMachine _stateMachine;
    public Transform PlayerTransform { get; set; }
    public CharacterController CharacterController { get; set; }
    public Animator Animator { get; set; }

    public CharacterData Data
    {
        get
        {
            return _characterData;
        }

        set
        {
            value = _characterData;

        }
    }

    internal protected AirState _airState;
    internal protected JumpState _jumpState;
    internal protected RunState _runState;
    internal protected IdleState _idleState;

    private void Awake()
    {
        PlayerTransform = this.transform;
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stateMachine = new CharacterStateMachine();

        _idleState = new IdleState(this, _stateMachine);
        _airState = new AirState(this, _stateMachine);
        _runState = new RunState(this, _stateMachine);
        _jumpState = new JumpState(this, _stateMachine);

        _stateMachine.Initialize(_idleState);
    }
    private void Update()
    {
        _stateMachine._currentState.HandleInput();
        _stateMachine._currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine._currentState.PhysicsUpdate();
    }
}