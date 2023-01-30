using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDriver.Audio
{

    public class SelfDestructable : MonoBehaviour
    {
        public void SelfDestructIn(float seconds)
        {
            Invoke(nameof(SelfDestruct), seconds);
        }
        public void SelfDestruct() => Destroy(this.gameObject);
    }
}