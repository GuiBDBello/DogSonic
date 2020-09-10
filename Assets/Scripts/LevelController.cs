using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public SceneController sceneController;

    private Vector3 checkpoint;

    void Start()
    {
        checkpoint = new Vector3(0f, 2f, 0f);
    }

    public void SetCheckpoint(Vector3 position)
    {
        this.checkpoint = position;
    }

    public void Respawn(GameObject player)
    {
        player.transform.position = checkpoint;
    }

    public void Finish()
    {
        sceneController.LoadMainMenu();
        Cursor.lockState = CursorLockMode.None;
    }
}
