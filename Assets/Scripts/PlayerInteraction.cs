using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float rayDistance = 3.5f;
    [SerializeField] private float interactRadius = 0.8f; // Grosor del detector
    [SerializeField] private LayerMask interactableLayer;
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 1f;

        if (Physics.SphereCast(origin, interactableLayer == 0 ? 0.5f : interactRadius, transform.forward, out hit, rayDistance, interactableLayer))
        {
            Debug.Log("¡Interacción realizada con!: " + hit.collider.name);

            Renderer objRenderer = hit.collider.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material.color = Color.green;
            }

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 origin = transform.position + Vector3.up * 1f;
        Gizmos.DrawRay(origin, transform.forward * rayDistance);
        Gizmos.DrawWireSphere(origin + transform.forward * rayDistance, interactRadius);
    }
}