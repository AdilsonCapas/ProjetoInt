using UnityEngine;
using UnityEngine.SceneManagement; 

public class Enemy : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("morte");

            // OU:
            // Destroy(collision.gameObject);
        }
    }
}