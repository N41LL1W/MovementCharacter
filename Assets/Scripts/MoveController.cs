using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    Vector2 _input;
    CharacterController _characterController;
    Vector3 _direction;

    [SerializeField] float rotationSpeed = 10.0f; // Nova vari�vel para controlar a velocidade de rota��o
    [SerializeField] float speed = 5.0f;

    private Transform mainCameraTransform; // Rota��o

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // Encontrar a c�mera principal (Cinemachine FreeLook)
        mainCameraTransform = Camera.main.transform; // Rota��o
    }

    private void Update()
    {
        if (_input.sqrMagnitude == 0) return;

        // Rota��o baseada na c�mera
        RotateWithCamera();

        // Rota��o
        //var targetRotation = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
        //var rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, targetRotation, 0.0f), rotationSpeed * Time.deltaTime);
        //transform.rotation = rotation;

        // Movimenta��o
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    private void RotateWithCamera()
    {
        // Obter a rota��o da c�mera sem rota��o em Y (evita inclina��o para cima/baixo)
        Vector3 cameraForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        // Rota��o da c�mera em rela��o ao plano horizontal
        Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);

        // Rota��o do input em rela��o � rota��o da c�mera
        Vector3 inputDirection = new Vector3(_input.x, 0, _input.y);
        _direction = cameraRotation * inputDirection.normalized;

        // Rota��o suave em dire��o � nova dire��o
        Quaternion targetRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y).normalized;
    }
}
