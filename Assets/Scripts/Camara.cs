using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    [SerializeField] private Transform target; 
    [Header("Ajustes de Posición")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -5f); 
    [SerializeField] private float smoothSpeed = 10f; 
    [Header("Ajustes de Rotación Orbital")]
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float minPitch = -20f; 
    [SerializeField] private float maxPitch = 60f;  
    private float currentYaw = 0f;   
    private float currentPitch = 0f; 
    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        HandleCameraRotation();
        HandleCameraPosition();
    }

    private void HandleCameraRotation()
    {
        currentYaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        currentPitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
    }

    private void HandleCameraPosition()
    {
        Quaternion targetRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 targetPosition = target.position + targetRotation * offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}