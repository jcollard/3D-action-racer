using System.Collections.Generic;
using UnityEngine;
using System;
namespace SkyDriver.Builder
{
    public class PlatformDatabase : MonoBehaviour {
        
        private Dictionary<PlatformType, Entry> _lookup;

        [field: SerializeField]
        public List<Entry> Platforms { get; private set; }
        
        private Dictionary<PlatformType, Entry> PlatformLookup 
        {
            get
            {
                if (_lookup == null)
                {
                    _lookup = new Dictionary<PlatformType, Entry>();
                    foreach(Entry e in Platforms)
                    {
                        _lookup[e.Type] = e;
                    }
                }
                return _lookup;
            }
        }

        public GameObject GetPlatform(Platform platform, Transform parent)
        {
            Entry entry = PlatformLookup[platform.Type];
            GameObject platformGameObject = Instantiate(entry.PlatformPrefab, parent);
            platformGameObject.transform.localScale = new Vector3(1, 1, platform.Length);
            platformGameObject.transform.position = new Vector3(platform.Column, 0, platform.StartPosition);
            return platformGameObject;
        }

        [Serializable]
        public class Entry
        {
            
            private Entry() {}
            [field: SerializeField]
            public PlatformType Type { get; private set; }
            [field: SerializeField]
            public GameObject PlatformPrefab {get; private set; }
        }

    }

    
}