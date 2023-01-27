using UnityEngine;

public class NonePlatformController : MonoBehaviour
{
    public BoxCollider Ground { get; set; }
    
    public void DeactivateGround(Collider collider){
        if (collider.attachedRigidbody.GetComponent<PlayerShipController>() != null)
        {
            Ground.gameObject.SetActive(false);
        }
    }

    public void ActivateGround(Collider collider) {
        if (collider.attachedRigidbody.GetComponent<PlayerShipController>() != null)
        {
            Invoke(nameof(ActivateGround), 1f);
        }
    }

    private void ActivateGround() => Ground.gameObject.SetActive(true);


}