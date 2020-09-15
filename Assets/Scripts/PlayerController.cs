using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public LevelController levelController;
    public GameObject player;
    public AudioClip jumpSound;
    public AudioClip bounceSound;
    public AudioClip gameOverSound;
    public AudioClip finishSound;
    public GameObject pauseMenu;
    public GameObject endGameMenu;

    private PlayerMovement playerMovement;
    private MouseLook mouseLook;
    private bool isMenuOpened = false;

    float pushPower = 10.0f;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        mouseLook = Camera.main.GetComponent<MouseLook>();
    }

    private void Update()
    {
        if (!levelController.GetGameOver())
        {
            if (Input.GetKeyDown(KeyCode.Escape)) isMenuOpened = true;
            PauseMenu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case Tags.GameOver:
                GameOverCoroutine();
                break;
            case Tags.Trap:
                Trap trap = other.gameObject.GetComponent<Trap>();
                if (trap != null)
                    trap.SetActivated(true);
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*
        Debug.Log(hit);
        Debug.Log(hit.collider);
        Debug.Log(hit.collider.name);
        */

        switch (hit.collider.tag)
        {
            case Tags.Checkpoint:
                Checkpoint(hit.collider.gameObject);
                break;
            case Tags.Destructible:
                if (hit.collider.name.Contains("QMark")) levelController.AddPoints(1000);
                if (hit.collider.name.Contains("Brick")) levelController.AddPoints(100);
                hit.collider.gameObject.GetComponent<Destructible>().Shatter();
                break;
            case Tags.Finish:
                //levelController.Finish(gameObject);
                FinishLevelCoroutine();
                break;
            case Tags.MovingPlatform:
                gameObject.transform.parent = hit.gameObject.transform;
                break;
            case Tags.Trap:
                Trap trap;

                trap = hit.collider.gameObject.GetComponent<TrapGoDown>();
                if (trap != null) trap.SetActivated(true);

                trap = hit.collider.gameObject.GetComponent<TrapGoUp>();
                if (trap != null) trap.SetActivated(true);

                trap = hit.collider.gameObject.GetComponent<TrapThrowUp>();
                if (trap != null) trap.SetActivated(true);

                break;
            default:
                gameObject.transform.parent = null;
                Rigidbody rb = hit.collider.attachedRigidbody;

                // no rigidbody
                if (rb == null || rb.isKinematic) { return; }
                if (hit.moveDirection.y < 0) { return; }
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                rb.velocity = pushDir * pushPower;

                break;
        }
    }

    private void Checkpoint(GameObject checkpoint)
    {
        levelController.SetCheckpoint(checkpoint.transform.position);
        Destroy(checkpoint);
    }

    public void Resume()
    {
        isMenuOpened = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        isMenuOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        levelController.Respawn(gameObject);
    }

    public void Restart()
    {
        isMenuOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        levelController.Restart();
    }

    public void Exit()
    {
        isMenuOpened = false;
        levelController.Exit();
    }

    private void PauseMenu()
    {
        if (isMenuOpened)
        {
            playerMovement.enabled = false;
            mouseLook.enabled = false;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            playerMovement.enabled = true;
            mouseLook.enabled = true;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void GameOverCoroutine()
    {
        StartCoroutine("GameOver");
    }

    IEnumerator GameOver()
    {
        AudioController.instance.Stop();
        AudioController.instance.PlayOneShot(gameOverSound);
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        yield return new WaitForSeconds(2.5f);

        AudioController.instance.Play();
        player.GetComponent<CharacterController>().enabled = true;
        levelController.Respawn(gameObject);
        mouseLook.enabled = true;
        playerMovement.enabled = true;
    }

    public void FinishLevelCoroutine()
    {
        StartCoroutine("FinishLevel");
    }

    IEnumerator FinishLevel()
    {
        levelController.SetGameOver(true);
        SaveScore();

        AudioController.instance.Stop();
        AudioController.instance.PlayOneShot(finishSound);
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        yield return new WaitForSeconds(0.5f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endGameMenu.SetActive(true);
    }

    private void SaveScore()
    {
        string levelName = SceneManager.GetActiveScene().name;
        float points = levelController.GetPoints();
        float highScore = PlayerPrefs.GetFloat(levelName);

        Debug.Log("High Score: " + highScore);
        Debug.Log("Points: " + points);

        if (highScore == 0 || points > highScore)
            PlayerPrefs.SetFloat(levelName, points);
    }
}
