using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject graphics;
    public float speed = 12f;
    public float gravity = -9.81f * 2.5f;
    public float jumpHeight = 5f;
    public float playerHeight = 1.8f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) Fall();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) Jump();

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl)) Crouch();
        if (Input.GetKeyUp(KeyCode.LeftControl)) Stand();
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)) Sprint();
        if (Input.GetKeyUp(KeyCode.LeftShift)) Stand();
    }

    private void Fall()
    {
        velocity.y = -2f;
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Crouch()
    {
        Debug.Log(playerHeight);
        controller.height = playerHeight;
        //graphics.transform.localScale = new Vector3(graphics.transform.localScale.x, playerHeight / 2f, graphics.transform.localScale.z);
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, (playerHeight / 2) - 0.1f, mainCamera.transform.position.z);
        graphics.transform.localScale = new Vector3(
            graphics.transform.localScale.x,
            playerHeight / 2f,
            graphics.transform.localScale.z
        );
        mainCamera.transform.position = new Vector3(
            mainCamera.transform.position.x,
            playerHeight * (playerHeight - 0.2f),
            mainCamera.transform.position.z
        );
        speed = 12f / 2f;
    }

    private void Stand()
    {
        Debug.Log(playerHeight);
        controller.height = playerHeight * 2f;
        //graphics.transform.localScale = new Vector3(graphics.transform.localScale.x, playerHeight - 0.1f, graphics.transform.localScale.z);
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, playerHeight - 0.1f, mainCamera.transform.position.z);
        graphics.transform.localScale = new Vector3(
            graphics.transform.localScale.x,
            playerHeight - 0.1f,
            graphics.transform.localScale.z
        );
        mainCamera.transform.position = new Vector3(
            mainCamera.transform.position.x,
            playerHeight * (playerHeight - 0.1f),
            mainCamera.transform.position.z
        );
        speed = 12f;
    }

    private void Sprint()
    {
        speed = 12f * 2;
    }
}
