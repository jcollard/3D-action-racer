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
        [SerializeField]
        private string _levelFileName;

        protected void Awake() {
            _builder = LevelBuilder.LoadFromFile(_levelFileName);
            
            foreach(Transform child in _platformsContainer)
            {
                Destroy(child.gameObject);
            }

            foreach(Platform p in _builder.Platforms)
            {
                PlacePlatform(p);
            }
        }

        private void PlacePlatform(Platform platform)
        {
            GameObject prefab = _platformDatabase.GetPlatform(platform.Type);
            GameObject platformGameObject = Instantiate(prefab, _platformsContainer);
            platformGameObject.transform.localScale = new Vector3(1, 1, platform.Length);
            float z = platform.StartPosition + platform.Length * 0.5f;
            platformGameObject.transform.position = new Vector3(platform.Column, 0, z);
        }
    }
}


