using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    [SerializeField]
    private PlayerShipController _player;

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            float distance = Mathf.Min(.2f, .0005f * _player.transform.position.z);
            transform.localScale = Vector3.one + Vector3.one * distance;

        }
    }
}
