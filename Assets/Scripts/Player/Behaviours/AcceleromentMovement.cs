using System;
using UnityEngine;

public class AcceleromentMovement : MovementBehaviour
{
    [SerializeField]
    private float _tiltThreshold = 0.1f;

    private float _moveHorizontal = 0f;
    
    public override float MoveHorizontal()
    {
        float tiltX = Input.acceleration.x;

        _moveHorizontal = 0;
        
        if (Mathf.Abs(tiltX) > _tiltThreshold)
        {
            _moveHorizontal = Mathf.Sign(tiltX);
        }

        return _moveHorizontal;
    }
}
