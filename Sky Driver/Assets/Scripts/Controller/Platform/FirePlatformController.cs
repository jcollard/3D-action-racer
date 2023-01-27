using UnityEngine;

public class FirePlatformController : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        PlayerShipController player = other.attachedRigidbody.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.Explode();
        }
    }
}