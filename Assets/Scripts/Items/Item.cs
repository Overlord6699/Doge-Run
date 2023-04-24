using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log( gameObject.name +" was hit");
    }
}
