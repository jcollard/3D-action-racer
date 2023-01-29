using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDriver.Audio
{

    public class MusicController : MonoBehaviour
    {
        private static MusicController s_Instance;

        public static MusicController Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    GameObject musicController = new GameObject("Music Controller");
                    musicController.AddComponent<MusicController>();

                }
                return s_Instance;
            }
        }

        [SerializeField]
        private MusicTrackDatabase _tracks;

        private AudioSource _playingAudio;
        private AudioSource _queuedAudio;
        private float _fadeTime = 0;
        private bool _swapQueued;

        public void StartTrack(int id)
        {
            AudioClip track = _tracks.Track(id);
            if (track == _playingAudio.clip) { return; }
            _fadeTime = 1;
            _swapQueued = true;
            _queuedAudio.clip = _tracks.Track(id);
            _queuedAudio.Play();
        }

        protected void Update()
        {
            FadeMusic();
        }

        private void FadeMusic()
        {
            if (_fadeTime > 0)
            {
                _fadeTime -= Time.deltaTime;
                _playingAudio.volume = _fadeTime;
                _queuedAudio.volume = 1 - _fadeTime;
            }
            else if (_swapQueued)
            {
                _swapQueued = false;

                // Swaps variables
                // For students, show them this trick but mention readability / AP exam expectations
                // (_queuedAudio, _playingAudio) = (_playingAudio, _queuedAudio);
                var temp = _playingAudio;
                _playingAudio = _queuedAudio;
                _queuedAudio = temp;
            }
        }

        protected void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
                _playingAudio = gameObject.AddComponent<AudioSource>();
                _queuedAudio = gameObject.AddComponent<AudioSource>();
                _tracks = Resources.Load<MusicTrackDatabase>("Prefabs/MusicTrackDatabase");
                _playingAudio.loop = true;
                DontDestroyOnLoad(this);
            }
            else if (this.gameObject != s_Instance.gameObject)
            {
                Destroy(this.gameObject);
            }

        }
    }
}