using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scenes.MainMenu);
    }

    public void LoadSampleScene()
    {
        SceneManager.LoadScene(Scenes.SampleScene);
    }

    public void LoadLevel01()
    {
        SceneManager.LoadScene(Scenes.Level01);
    }

    public void LoadLevel02()
    {
        SceneManager.LoadScene(Scenes.Level02);
    }

    public void LoadLevel03()
    {
        SceneManager.LoadScene(Scenes.Level03);
    }

    public void LoadLevel04()
    {
        SceneManager.LoadScene(Scenes.Level04);
    }

    public void LoadLevel05()
    {
        SceneManager.LoadScene(Scenes.Level05);
    }

    public void LoadLevel06()
    {
        SceneManager.LoadScene(Scenes.Level06);
    }

    public void LoadLevel07()
    {
        SceneManager.LoadScene(Scenes.Level07);
    }

    public void LoadLevel08()
    {
        SceneManager.LoadScene(Scenes.Level08);
    }

    public void LoadLevel09()
    {
        SceneManager.LoadScene(Scenes.Level09);
    }
}
