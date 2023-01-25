using UnityEngine;

public class BoostPlatformController : MonoBehaviour
{
    [SerializeField]
    private float _boostiness = 40f;
    private void OnCollisionStay(Collision other) {
        PlayerShipController player = other.gameObject.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.Accelerate(_boostiness * Time.deltaTime);
        }
    }
}