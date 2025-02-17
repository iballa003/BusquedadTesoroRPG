using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objeto que la cámara sigue (por ejemplo, el jugador)
    public Vector2 minBounds; // Límite mínimo (esquina inferior izquierda del mapa)
    public Vector2 maxBounds; // Límite máximo (esquina superior derecha del mapa)

    private Camera mainCamera;
    private float halfCameraWidth;
    private float halfCameraHeight;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        // Calcula el tamaño de la cámara en unidades del mundo
        halfCameraHeight = mainCamera.orthographicSize;
        halfCameraWidth = halfCameraHeight * mainCamera.aspect;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Obtén la posición del jugador
            Vector3 targetPosition = target.position;

            // Limita la posición de la cámara dentro de los bordes del mapa
            float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x + halfCameraWidth, maxBounds.x - halfCameraWidth);
            float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + halfCameraHeight, maxBounds.y - halfCameraHeight);

            // Actualiza la posición de la cámara
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
