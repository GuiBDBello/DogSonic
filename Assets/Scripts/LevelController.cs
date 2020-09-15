using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public SceneController sceneController;
    public Text pointsText;
    public Text timeText;
    public Text deathsText;

    public Text endGameTimeText;
    public Text endGameDeathsText;
    public Text endGamePointsText;

    private bool isGameOver = false;
    private Vector3 checkpoint = new Vector3(0f, 2f, 0f);
    private int points = 0;
    private float time = 0f;
    private int deaths = 0;

    private void Update()
    {
        if (!isGameOver)
        {
            time += Time.deltaTime;

            pointsText.text = "Points: " + points;
            timeText.text = "Time: " + System.Math.Round(time, 2);
            deathsText.text = "Deaths: " + deaths;
        }
        else
        {
            endGameTimeText.text = "Time (* -10): " + System.Math.Round(time, 2);
            endGameDeathsText.text = "Deaths (* -1000): " + deaths;
            endGamePointsText.text = "Points: " + GetPoints();
        }
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpoint = position;
    }

    public float GetPoints()
    {
        return (float) (points + System.Math.Round(time * -10, 2) + (deaths * -1000));
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public void PlayerDied()
    {
        deaths++;
    }

    public void Respawn(GameObject player)
    {
        PlayerDied();
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.SetPositionAndRotation(checkpoint, Quaternion.identity);
        player.GetComponent<CharacterController>().enabled = true;
    }

    public void Restart()
    {
        sceneController.ReloadActiveScene();
    }
    /*
    public void Finish(GameObject player)
    {
        sceneController.LoadMainMenu();
    }
    */
    public void Exit()
    {
        sceneController.LoadMainMenu();
    }
}
