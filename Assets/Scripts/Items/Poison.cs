using UnityEngine;

public class Poison : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ReceivePoison(1);
            Destroy(gameObject);
        }
    }
}
