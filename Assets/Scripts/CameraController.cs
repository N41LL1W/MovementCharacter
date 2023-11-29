using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 2f;

    private void OnEnable()
    {
        // Registra o m�todo OnCameraRotation para a a��o CameraRotation
        InputSystem.EnableDevice(InputSystem.GetDevice<Mouse>());

        // Cria uma a��o e se inscreve para o evento performed
        UnityEngine.InputSystem.InputAction action = new UnityEngine.InputSystem.InputAction(binding: "<Mouse>/delta");
        action.AddBinding("<Mouse>/delta");
        action.performed += OnCameraRotation;
        action.Enable();
    }

    private void OnDisable()
    {
        // Remove o m�todo OnCameraRotation da a��o CameraRotation ao desativar o script
        InputSystem.DisableDevice(InputSystem.GetDevice<Mouse>());
    }

    private void OnCameraRotation(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Leia os valores de delta do mouse
        Vector2 mouseDelta = context.ReadValue<Vector2>();

        // Ajusta a sensibilidade da rota��o
        float rotationX = -mouseDelta.y * rotationSpeed;
        float rotationY = mouseDelta.x * rotationSpeed;

        // Aplica a rota��o � c�mera (ou objeto desejado)
        transform.Rotate(rotationX, rotationY, 0f);
    }
}
