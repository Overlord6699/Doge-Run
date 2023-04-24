using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void DieDelegate();
    public event DieDelegate OnPlayerDied;

    [SerializeField]
    private PlayerMotor _motor;
    
    [SerializeField]
    private AnimationController _animationController;

    [SerializeField]
    private int _HP=1;

    private bool _isHitted = false;

    private void Start()
    {
        _motor.enabled = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(_isHitted)
            return;
        
        if (hit.collider.CompareTag("Trap"))
        {
            GetDamage();
            Debug.Log("Hit");
        }

        if (hit.collider.CompareTag("Item"))
        {
            GetItem();
        }
    }

    private void GetItem()
    {}
    
    public void OnLevelUp(int level)
    {
       
        _motor.SetSpeed(level);
    }
    
    private void GetDamage()
    {
        EnableInvincible();
        _HP--;
        
        if (_HP < 1)
        {
            Die();
        }
        else
        {
            _animationController.SetHitTrigger();
        }
    }

    private void Die()
    {
        _animationController.SetDeathTrigger();
        _motor.enabled = false;
        
     
    }

    public void OnDeathAnimationOver()
    {
        OnPlayerDied?.Invoke();
    }

    private void EnableInvincible()
    {
        _isHitted = true;
    }
    
    private void DisableInvincible()
    {
        _isHitted = false;
    }
}
