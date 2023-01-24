using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _distance = 10;

    void Update()
    {
        float newZ = _target.position.z - _distance;
        float x = transform.position.x;
        float y = transform.position.y;
        transform.position = new (x, y, newZ);
    }
}
