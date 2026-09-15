using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private Color victoryColor = Color.green;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null)
        {
            Debug.Log("<color=green>¡NIVEL COMPLETADO!</color>");

            Renderer zoneRenderer = GetComponent<Renderer>();
            if (zoneRenderer != null)
            {
                zoneRenderer.material.color = victoryColor;
            }
        }
    }
}