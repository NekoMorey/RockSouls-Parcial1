using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configuración del Proyectil (Punto 8)")]
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float projectileForce = 20f;
    [SerializeField] private float destroyTime = 3f;

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootMagic();
        }
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            ShootMagic();
        }
    }
    private void ShootMagic()
    {
        if (magicPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Falta asignar el magicPrefab o el spawnPoint en el Inspector.");
            return;
        }

        GameObject magic = Instantiate(magicPrefab, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = magic.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawnPoint.forward * projectileForce;
        }

        Destroy(magic, destroyTime);
    }
}