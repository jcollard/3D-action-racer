using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipBodyCollider : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<Collision> OnBodyCollision { get; private set; }    

    

    private void OnCollisionEnter(Collision other)
    {
        OnBodyCollision.Invoke(other);
    }

}
