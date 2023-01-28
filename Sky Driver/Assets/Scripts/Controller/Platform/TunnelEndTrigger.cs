using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TunnelEndTrigger : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnExitTunnel { get; private set; }

    public void OnTriggerEnter(Collider other)
    {
        PlayerShipController player = other.attachedRigidbody.GetComponent<PlayerShipController>();
        if (player != null)
        {
            OnExitTunnel.Invoke();
        }
    }
}
