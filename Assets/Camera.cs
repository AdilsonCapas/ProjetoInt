using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;        // Referência ao personagem
    public float smoothSpeed = 0.125f;  // Suavidade do movimento
    public Vector3 offset;          // Offset da câmera em relação ao personagem

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Mantém a posição Z da câmera para evitar mudar o plano
        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }
}
