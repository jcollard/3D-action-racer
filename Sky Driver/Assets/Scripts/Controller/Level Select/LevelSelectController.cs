using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SkyDriver.Builder;

namespace SkyDriver.Level
{
    public class LevelSelectController : MonoBehaviour
    {
        public void SelectLevel(TextAsset levelData)
        {
            LevelData.CurrentBuilder = LevelBuilder.LoadFromTextAsset(levelData);
            SceneManager.LoadScene("Level");
        }
    }
}