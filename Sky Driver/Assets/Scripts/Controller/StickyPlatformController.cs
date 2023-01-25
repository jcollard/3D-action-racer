using UnityEngine;

public class StickyPlatformController : MonoBehaviour
{
    [SerializeField]
    private float _stickiness = 40f;
    private void OnCollisionStay(Collision other) {
        PlayerShipController player = other.gameObject.GetComponent<PlayerShipController>();
        if (player != null)
        {
            player.Decelerate(_stickiness * Time.deltaTime);
        }
    }
}