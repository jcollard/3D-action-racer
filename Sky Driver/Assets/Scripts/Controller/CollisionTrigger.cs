using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<Collider> OnStay { get; private set; }

    [field: SerializeField]
    public UnityEvent<Collider> OnEnter { get; private set; }

    [field: SerializeField]
    public UnityEvent<Collider> OnExit { get; private set; }

    private void OnTriggerStay(Collider collider) => OnStay.Invoke(collider);
    private void OnTriggerExit(Collider collider) => OnExit.Invoke(collider);
    private void OnTriggerEnter(Collider collider) => OnEnter.Invoke(collider);

}