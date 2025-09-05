using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;        // Refer�ncia ao personagem
    public float smoothSpeed = 0.125f;  // Suavidade do movimento
    public Vector3 offset;          // Offset da c�mera em rela��o ao personagem

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Mant�m a posi��o Z da c�mera para evitar mudar o plano
        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }
}
