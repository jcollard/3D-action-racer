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
        [SerializeField]
        private BoxCollider _ground;

        protected void Awake()
        {
            _builder = LevelBuilder.LoadFromFile(_levelFileName);
            ClearPlatforms();
            BuildPlatforms();
        }

        private void ClearPlatforms()
        {
            foreach (Transform child in _platformsContainer)
            {
                Destroy(child.gameObject);
            }
        }

        private void BuildPlatforms()
        {
            int maxPosition = 0;
            foreach (Platform p in _builder.Platforms)
            {
                maxPosition = Mathf.Max(maxPosition, p.StartPosition + p.Length);
                PlacePlatform(p);
            }
            _ground.size = new Vector3(7, 1, maxPosition);
            _ground.transform.position = new Vector3(3, 0.1f, maxPosition * .5f);
        }

        private void PlacePlatform(Platform platform)
        {
            GameObject prefab = _platformDatabase.GetPlatform(platform.Type);
            GameObject platformGameObject = Instantiate(prefab, _platformsContainer);
            platformGameObject.transform.localScale = new Vector3(1, 1, platform.Length);
            float z = platform.StartPosition + platform.Length * 0.5f;
            platformGameObject.transform.position = new Vector3(platform.Column, 0, z);

            NonePlatformController nonePlatformController = platformGameObject.GetComponent<NonePlatformController>();
            if (nonePlatformController != null)
            {
                nonePlatformController.Ground = _ground;
            }
        }
    }
}


