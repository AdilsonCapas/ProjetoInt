using UnityEngine;

public class Vilao : MonoBehaviour
{
    public int vida = 3; // morre depois de 3 acertos

    public void TomarDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
