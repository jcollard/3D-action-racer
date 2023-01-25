using UnityEngine;

public class FirePlatformController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        PlayerShipController player = other.gameObject.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.Explode();
        }
    }
}