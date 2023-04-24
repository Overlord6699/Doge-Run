using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private bool _isAvailable = true;
    
    private void Start()
    {
        _animator.SetBool("Run", true);
    }

    public void DisableAnimator()
    {
        _animator.enabled = false;
    }
    
    public void EnableAnimator()
    {
        _animator.enabled = true;
    }

    public void EnableAnimations()
    {
        _isAvailable = true;
    }

    public void DisableAnimations()
    {
        _isAvailable = false;
    }
    
    public void SetDeathTrigger()
    {
        _animator.SetTrigger("Death");
    }

    public void SetHitTrigger()
    {
        _animator.SetTrigger("Hit");
    }
}
