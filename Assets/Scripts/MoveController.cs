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

    [SerializeField] float rotationSpeed = 10.0f; // Nova variável para controlar a velocidade de rotação
    [SerializeField] float speed = 5.0f;

    private Transform mainCameraTransform; // Rotação

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // Encontrar a câmera principal (Cinemachine FreeLook)
        mainCameraTransform = Camera.main.transform; // Rotação
    }

    private void Update()
    {
        if (_input.sqrMagnitude == 0) return;

        // Rotação baseada na câmera
        RotateWithCamera();

        // Rotação
        //var targetRotation = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
        //var rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, targetRotation, 0.0f), rotationSpeed * Time.deltaTime);
        //transform.rotation = rotation;

        // Movimentação
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    private void RotateWithCamera()
    {
        // Obter a rotação da câmera sem rotação em Y (evita inclinação para cima/baixo)
        Vector3 cameraForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        // Rotação da câmera em relação ao plano horizontal
        Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);

        // Rotação do input em relação à rotação da câmera
        Vector3 inputDirection = new Vector3(_input.x, 0, _input.y);
        _direction = cameraRotation * inputDirection.normalized;

        // Rotação suave em direção à nova direção
        Quaternion targetRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y).normalized;
    }
}
