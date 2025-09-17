using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O personagem
    public float smoothSpeed = 0.125f; // Suaviza��o
    public Vector3 offset; // Dist�ncia entre c�mera e player

    [Header("Limites da C�mera")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (target == null) return;

        // Posi��o desejada baseada no personagem + offset
        Vector3 desiredPosition = target.position + offset;

        // Suaviza movimento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplica limites
        float clampX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        transform.position = new Vector3(clampX, clampY, smoothedPosition.z);
    }
}
