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

    private int _screenCenter = Screen.width / 2;
    
    private void Start()
    {
        _speed = _baseForwardSpeed;
        _characterController = GetComponent<CharacterController>();
    }


    private float GetSpeedRatio(float touchPosX)
    {
        float centerOffset = Mathf.Abs(touchPosX - _screenCenter);
        return (centerOffset / _screenCenter);
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
        
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            
            var dir = touch.position.x > _screenCenter ? 1f : -1f;
            var ratio = GetSpeedRatio(touch.position.x);
            
            //smooth movement
            _moveVector.x = ratio*dir*_horizontalSpeed;
        }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE 
        _moveVector.x = Input.GetAxisRaw("Horizontal") * _horizontalSpeed;
#endif

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
