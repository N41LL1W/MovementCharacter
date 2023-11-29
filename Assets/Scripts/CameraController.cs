using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 2f;

    private void OnEnable()
    {
        // Registra o método OnCameraRotation para a ação CameraRotation
        InputSystem.EnableDevice(InputSystem.GetDevice<Mouse>());

        // Cria uma ação e se inscreve para o evento performed
        UnityEngine.InputSystem.InputAction action = new UnityEngine.InputSystem.InputAction(binding: "<Mouse>/delta");
        action.AddBinding("<Mouse>/delta");
        action.performed += OnCameraRotation;
        action.Enable();
    }

    private void OnDisable()
    {
        // Remove o método OnCameraRotation da ação CameraRotation ao desativar o script
        InputSystem.DisableDevice(InputSystem.GetDevice<Mouse>());
    }

    private void OnCameraRotation(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Leia os valores de delta do mouse
        Vector2 mouseDelta = context.ReadValue<Vector2>();

        // Ajusta a sensibilidade da rotação
        float rotationX = -mouseDelta.y * rotationSpeed;
        float rotationY = mouseDelta.x * rotationSpeed;

        // Aplica a rotação à câmera (ou objeto desejado)
        transform.Rotate(rotationX, rotationY, 0f);
    }
}
