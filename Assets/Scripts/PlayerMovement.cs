using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerController playerController;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float initialSpeed = 12f;
    public float jumpHeight = 5f;
    public float groundDistance = 0.2f;

    private Vector3 velocity;
    private Camera mainCamera;

    private float gravity;
    private float speed;
    private float playerHeight;
    private bool isGrounded;
    private bool isCrouched;

    private void Start()
    {
        mainCamera = Camera.main;
        gravity = -9.81f * 2.5f;
        speed = initialSpeed;
        playerHeight = playerController.player.transform.GetChild(0).transform.localScale.y;
    }
    /*
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
    */
    private void Update()
    {
        isGrounded = Physics.CheckCapsule(groundCheck.position, gameObject.transform.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) Jump();

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl)) Crouch();
        if (isCrouched && Input.GetKeyUp(KeyCode.LeftControl)) Stand();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouched) Sprint();
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouched) Walk();
    }

    public float getGravity()
    {
        return gravity;
    }

    public void ThrowPlayerUp()
    {
        velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
        AudioController.instance.PlayOneShot(playerController.jumpSound);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        AudioController.instance.PlayOneShot(playerController.jumpSound);
    }

    private void Crouch()
    {
        isCrouched = true;
        characterController.height = playerHeight;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0.5f, gameObject.transform.localScale.z);
        Walk();
    }

    private void Stand()
    {
        isCrouched = false;
        characterController.height = playerHeight * 2f;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, gameObject.transform.localScale.z);
        Walk();
    }

    private void Sprint()
    {
        speed = initialSpeed * 2;
    }

    private void Walk()
    {
        speed = initialSpeed;
    }
}
