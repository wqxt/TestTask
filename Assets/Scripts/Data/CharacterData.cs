using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Movement Parameters")]
    [SerializeField, Range(0, 1)] protected internal float _acceleration;
    [SerializeField, Range(0, 15)] protected internal float _maxSpeed;
    [SerializeField] protected internal float _currentSpeed;
    [SerializeField] protected internal Vector2 _gravityVelocity;
    [SerializeField] protected internal Vector3 _direction;
    [SerializeField] protected internal Vector3 _velocity;
    [SerializeField] protected internal Vector2 _currentAnimationBlendvector = new Vector3(0,0,0);
    [SerializeField] protected internal bool _isAir = false;
    [SerializeField] protected internal bool _isGrounded = false;
    [SerializeField] protected internal bool _isRun;


    [Header("Jump Parameters")]
    [SerializeField, Range(-30, 0)] protected internal float _gravityValue = -20f;
    [SerializeField, Range(0, 30)] protected internal float _jumpForce;
    [SerializeField, Range(0, 10)] protected internal float _gravityScale;
    [SerializeField, Range(0, 3)] protected internal float _jumpHeight = 0.5f;
    [SerializeField, Range(0, 10)] protected internal float _jumpLength;
    [SerializeField] protected internal int _jumpAmount;
    [SerializeField] protected internal bool _isJump = false;
    [SerializeField] protected internal int _jumpCount = 0;
}
