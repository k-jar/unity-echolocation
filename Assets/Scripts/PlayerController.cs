using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    private PlayerAudio playerAudio;
    public ParticleSystem echolocationRing;

    [SerializeField] private float playerSpeed = 2.0f;
    //[SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private float footstepDelay = 0.4f;
    [SerializeField] private float footstepTimer = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        playerAudio = GetComponent<PlayerAudio>();

        // Used for footstep audio and echolocation rings
        StartCoroutine(CheckForFootstep());

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private bool IsMoving()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        return (movement.x != 0 || movement.y != 0);
    }
    private IEnumerator CheckForFootstep()
    {
        while (true)
        {
            if (IsMoving())
            {
                footstepTimer += Time.deltaTime;
            }
            if (footstepTimer > footstepDelay)
            {
                playerAudio.PlayFootstep();
                echolocationRing.Play();
                footstepTimer = 0f;
            }
            yield return null;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Player collided with " + other.name);
        if (other.CompareTag("ObjectNoise"))
        {
            Debug.Log("Inside if statement");
            other.GetComponentInParent<NoisyObjectController>().TemporarilyActivate();
        }
    }
}
