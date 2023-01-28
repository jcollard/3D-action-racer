using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitTunnelController : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnExitTunnel { get; private set; }

    public void Exit() => OnExitTunnel.Invoke();
}
