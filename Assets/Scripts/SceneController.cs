using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSampleScene()
    {
        SceneManager.LoadScene(Scenes.SampleScene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
