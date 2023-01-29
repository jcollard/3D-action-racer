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
            get => s_Instance;
        }
        // Start is called before the first frame update
        protected void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (this.gameObject != s_Instance.gameObject)
            {
                Destroy(this.gameObject);
            }
            
        }
    }
}   