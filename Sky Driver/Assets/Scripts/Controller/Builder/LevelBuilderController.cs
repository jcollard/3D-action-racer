using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDriver.Builder
{
    public class LevelBuilderController : MonoBehaviour
    {
        [SerializeField]
        private LevelBuilder _builder;
        [SerializeField]
        private PlatformDatabase _platformDatabase;
        [SerializeField]
        private Transform _platformsContainer;

        protected void Awake() {
            string simpleLevel = string.Join("\n", new string[]{
                "XFT SB*",
                "XFT SB*",
                "* BSTFX",
                "* BSTFX",
                "* BSTFX",
            });
            _builder = LevelBuilder.LoadFromString(simpleLevel);
            
            foreach(Transform child in _platformsContainer)
            {
                Destroy(child.gameObject);
            }

            foreach(Platform p in _builder.Platforms)
            {
                _platformDatabase.GetPlatform(p, _platformsContainer);
            }
        }
    }
}


