using UnityEngine;

public class Box : Item
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameObject _base;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Player"))
        {
            //Destroy(gameObject);
            _base.SetActive(false);
            _animator.SetTrigger("Bang");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
