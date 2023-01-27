using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TunnelController : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnExitTunnel { get; private set; }

    public void OnTriggerEnter(Collider other)
    {
        PlayerShipController player = other.attachedRigidbody.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.IsMotionLocked = true;
            Vector3 position = player.transform.position;
            position.x = transform.position.x;
            player.transform.position = position;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        PlayerShipController player = other.attachedRigidbody.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.IsMotionLocked = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        PlayerShipController player = other.attachedRigidbody.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.IsMotionLocked = false;
            OnExitTunnel.Invoke();
        }
    }
}
