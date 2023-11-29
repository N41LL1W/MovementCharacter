using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private InputAction movementInput;
    private Animator animator;
    private CharacterController controller;
    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        movementInput = new InputAction("Player.Movement");
        movementInput.Enable();
        movementInput.performed += OnMovementInput;
        movementInput.canceled += OnMovementInput;

        controller = GetComponentInChildren<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController component not found on the GameObject.");
        }

        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("AnimatorController component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        Vector2 movementValue = context.ReadValue<Vector2>();

        // Debug.Log para verificar os valores de entrada
        Debug.Log($"Movement Input: {movementValue}");

        // Normalize o vetor de movimento para garantir que o movimento seja suave em todas as direções.
        Vector3 movement = new Vector3(movementValue.x, 0f, movementValue.y).normalized;

        // Debug.Log para verificar a velocidade calculada
        Debug.Log($"Speed: {movement.magnitude}");

        // Atualiza o parametro de velocidade do Animator.
        animator.SetFloat("Speed", movement.magnitude);

        if (movement.magnitude >= 0.1f) // Verifique se há um movimento significativo
        {
            // Debug.Log para verificar a direção do movimento
            Debug.Log($"Movement Direction: {movement}");

            // Aplique o movimento ao personagem.
            controller.Move(movement * moveSpeed * Time.deltaTime);
        }
    }
}
