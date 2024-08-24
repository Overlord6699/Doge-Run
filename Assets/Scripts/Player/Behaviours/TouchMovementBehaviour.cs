using UnityEngine;
using  Unity;

public class TouchMovementBehaviour : MovementBehaviour
{
    
    private float _moveValue;
    
    private int _screenCenter = Screen.width / 2;
    
    private float GetSpeedRatio(float touchPosX)
    {
        float centerOffset = Mathf.Abs(touchPosX - _screenCenter);
        return (centerOffset / _screenCenter);
    }
    
    public override float MoveHorizontal()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            
            var dir = touch.position.x > _screenCenter ? 1f : -1f;
            var ratio = GetSpeedRatio(touch.position.x);


            return ratio * dir;
        }
#endif

        return 0f;
    }
}
