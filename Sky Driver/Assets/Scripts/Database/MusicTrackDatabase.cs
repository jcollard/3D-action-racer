using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDriver.Audio
{
    public class MusicTrackDatabase : MonoBehaviour
    {
        [field: SerializeField]
        private AudioClip[] Tracks { get; set; }

        public AudioClip Track(int ix) => Tracks[ix % Tracks.Length];
    }
}