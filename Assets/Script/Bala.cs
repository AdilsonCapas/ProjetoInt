using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidade = 10f; // velocidade da bala
    public int dano = 1; // 1 bala = 1 acerto

    void Update()
    {
        // Bala sempre vai para a direita
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se bateu no vilao
        if (collision.CompareTag("Vilao"))
        {
            // Pega o script do inimigo
            Vilao vilao = collision.GetComponent<Vilao>();
            if (vilao != null)
            {
                vilao.TomarDano(dano);
            }

            // Destrói a bala
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Chao"))
        {
            // Se bater no chão, também destrói
            Destroy(gameObject);
        }
    }
}
