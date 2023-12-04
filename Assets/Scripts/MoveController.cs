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

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_input.sqrMagnitude == 0) return;

        // Rotação
        var targetRotation = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
        var rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, targetRotation, 0.0f), rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;

        // Movimentação
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y).normalized;
    }
}
