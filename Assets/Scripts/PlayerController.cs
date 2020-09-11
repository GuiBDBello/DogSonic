using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public LevelController levelController;
    public GameObject player;
    public AudioClip jumpSound;
    public AudioClip gameOverSound;

    private GameController gameController;
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;

    float pushPower = 10.0f;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        playerMovement = player.GetComponent<PlayerMovement>();
        mouseLook = Camera.main.GetComponent<MouseLook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case Tags.GameOver:
                GameOver();
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
                hit.collider.gameObject.GetComponent<Destructible>().Shatter();
                break;
            case Tags.Finish:
                Finish();
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

    private void Finish()
    {
        levelController.Finish();
    }

    private void GameOver()
    {
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        AudioController.instance.PlayOneShot(gameOverSound);
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        yield return new WaitForSeconds(2.5f);

        levelController.Respawn(gameObject);
        player.GetComponent<CharacterController>().enabled = true;
        mouseLook.enabled = true;
        playerMovement.enabled = true;
    }
}
