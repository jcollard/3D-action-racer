using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour
{
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
        }
    }
}
