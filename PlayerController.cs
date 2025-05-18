using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 10f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController characterController;
    private Vector3 velocity;
    private Vector2 inputDirection;

    [SerializeField] private StaminaSystem staminaSystem;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        staminaSystem = GetComponent<StaminaSystem>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        inputDirection = new Vector2(moveX, moveZ);

        if (characterController.isGrounded)
        {
            velocity.y = Input.GetKeyDown(KeyCode.Space) ? jumpForce : -0.1f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        float currentSpeed = baseMoveSpeed * (staminaSystem != null ? staminaSystem.SpeedMultiplier : 1f);
        Vector3 move = transform.right * inputDirection.x + transform.forward * inputDirection.y;
        Vector3 finalMove = new Vector3(move.x, velocity.y, move.z);

        characterController.Move(finalMove * currentSpeed * Time.deltaTime);
    }
}
