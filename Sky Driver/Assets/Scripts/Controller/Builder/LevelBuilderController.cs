using System.Collections;
using System.Collections.Generic;
using SkyDriver.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkyDriver.Builder
{
    public class LevelBuilderController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _levelClearedScreen;
        [SerializeField]
        private PlayerShipController _player;
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

        public void LevelSelect()
        {
            SceneManager.LoadScene("Level Select");
        }

        protected void Awake()
        {
            _builder = LevelData.CurrentBuilder;
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
            int maxZ = 0;
            int maxX = 0;
            foreach (Platform p in _builder.Platforms)
            {
                maxZ = Mathf.Max(maxZ, p.StartPosition + p.Length);
                maxX = Mathf.Max(maxX, p.Column);
                PlacePlatform(p);
            }
            _ground.size = new Vector3((maxX + 1), 1, maxZ);
            _ground.transform.position = new Vector3(maxX * .5f, 0.1f, maxZ * .5f);
        }

        private void PlacePlatform(Platform platform)
        {
            GameObject prefab = _platformDatabase.GetPlatform(platform.Type);
            GameObject platformGameObject = Instantiate(prefab, _platformsContainer);
            platformGameObject.transform.localScale = new Vector3(1, 1, platform.Length);
            float z = platform.StartPosition + platform.Length * 0.5f;
            platformGameObject.transform.position = new Vector3(platform.Column, 0, z);

            ExitTunnelController exitTunnel = platformGameObject.GetComponent<ExitTunnelController>();
            if (exitTunnel != null)
            {
                exitTunnel.OnExitTunnel.AddListener(LevelComplete);
            }
        }

        private void LevelComplete()
        {
            _player.ExitLevel();
            _levelClearedScreen.SetActive(true);
        }
    }
}


