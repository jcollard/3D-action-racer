using UnityEngine;

public class BoostPlatformController : MonoBehaviour
{
    [SerializeField]
    private float _boostiness = 40f;
    private void OnTriggerStay(Collider other) {
        PlayerShipController player = other.attachedRigidbody.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.Accelerate(_boostiness * Time.deltaTime);
        }
    }
}