using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SkyDriver.Audio;

public class TitleScreenController : MonoBehaviour
{
    

    public void PlayGame()
    {
        SceneManager.LoadScene("Level Select");
        
    }

    protected void Awake()
    {
        MusicController.Instance.StartTrack(0);
    }
}
