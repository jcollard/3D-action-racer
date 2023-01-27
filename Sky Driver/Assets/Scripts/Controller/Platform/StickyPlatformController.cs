using UnityEngine;

public class StickyPlatformController : MonoBehaviour
{
    [SerializeField]
    private float _stickiness = 40f;
    private void OnTriggerStay(Collider other) {
        PlayerShipController player = other.attachedRigidbody.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.Decelerate(_stickiness * Time.deltaTime);
        }
    }
}