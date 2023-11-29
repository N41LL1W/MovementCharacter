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

    [SerializeField] float speed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }
}
