using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;

    public LevelController levelController;
    public GameObject graphics;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        Debug.Log(collision.collider);
        Debug.Log(collision.collider.name);
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
            case Tags.Finish:
                Finish();
                break;
            case Tags.GameOver:
                GameOver();
                break;
            default:
                break;
        }
    }

    private void Checkpoint(GameObject checkpoint)
    {
        Debug.Log("Pimba");
        levelController.SetCheckpoint(checkpoint.transform.position);
        Destroy(checkpoint);
    }

    private void Finish()
    {
        Debug.Log("Cabol");
        levelController.Finish();
    }

    private void GameOver()
    {
        Debug.Log("Moreu");
        levelController.Respawn(gameObject);
    }
}
