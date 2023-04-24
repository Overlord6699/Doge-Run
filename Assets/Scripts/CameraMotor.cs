using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraMotor : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _distance = 10f;
    [SerializeField]
    private float _minHeight = 3f;
    [SerializeField]
    private float _maxHeight = 5f;
    
[SerializeField]
    private float _transition = 0f;
    [SerializeField]
    private float _animDuration = 1.5f;
    [SerializeField]
    private Vector3 _animOffset = new Vector3(0,1,0);

    [SerializeField]
    private Transform _cameraSpawn;

    public void Init(Transform playerTransform)
    {
        _transition = 0f;
        transform.position = _cameraSpawn.position;
        _target = playerTransform;
    }

    private void Update()
    {
        var moveVector = transform.position;
        var dist = (transform.position - _target.position).magnitude;

        moveVector.y = Mathf.Clamp(moveVector.y, _minHeight,_maxHeight);
        


        if (_transition > 1f)
        {
            if (dist > _distance)
            {
                //moveVector.z = Vector3.Lerp(transform.position, _target.position, Time.deltaTime).z;
                moveVector.z = _target.position.z - _distance;
                transform.position = moveVector;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector - _animOffset, moveVector, _transition);
            _transition += Time.deltaTime / _animDuration;
        }
    }
}
