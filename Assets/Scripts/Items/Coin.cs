using UnityEngine;

public class Coin : Item
{
    [SerializeField]
    private int _value = 1;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ReceiveMoney(_value);
            Destroy(gameObject);
        }
    }

}
