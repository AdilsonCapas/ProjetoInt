using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O personagem
    public float smoothSpeed = 0.125f; // Suavização
    public Vector3 offset; // Distância entre câmera e player

    [Header("Limites da Câmera")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (target == null) return;

        // Posição desejada baseada no personagem + offset
        Vector3 desiredPosition = target.position + offset;

        // Suaviza movimento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplica limites
        float clampX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        transform.position = new Vector3(clampX, clampY, smoothedPosition.z);
    }
}
