using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMotor : MonoBehaviour
{
    
    private CharacterController _characterController;
    
    
    [FormerlySerializedAs("_baseSpeed")] [SerializeField] private float _baseForwardSpeed = 5f;
    [SerializeField]
    private float _horizontalSpeed = 4;

    
    [SerializeField]
    private float _gravity = 10;
    [SerializeField]
    private float _animDuration = 2f;
    
    private Vector3 _moveVector;
    private float _verticalVelocity;
    private float _speed;


    [SerializeField]
    private MovementBehaviour _horizontalMovementBehavior;
    
    private void Start()
    {
        _speed = _baseForwardSpeed;
        _characterController = GetComponent<CharacterController>();
    }
    
    
    private void Update()
    {
        if (Time.time < _animDuration)
        {
            _characterController.Move((Vector3.forward * _speed) * Time.deltaTime);
            return;
            
        }
        
        _moveVector = Vector3.zero;
        _moveVector.z = _speed;

        var moveHorizontalValue = 0f;

        moveHorizontalValue = _horizontalMovementBehavior.MoveHorizontal();
        
#if UNITY_EDITOR || UNITY_STANDALONE 
        moveHorizontalValue = Input.GetAxisRaw("Horizontal");
#endif
 
        
        //smooth movement
        _moveVector.x = moveHorizontalValue * _horizontalSpeed;
        
        if (_characterController.isGrounded)
        {
            _verticalVelocity = -0.5f;
        }
        else
        {
            _verticalVelocity -= _gravity *Time.deltaTime;
        }

        _moveVector.y = _verticalVelocity;


        _characterController.Move(_moveVector* Time.deltaTime);
    }

    public void SetSpeed(int multiplier)
    {
        _speed = _baseForwardSpeed + multiplier;
    }

}
