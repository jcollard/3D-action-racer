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

        public GameObject GetPlatform(PlatformType type) => PlatformLookup[type].PlatformPrefab;

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