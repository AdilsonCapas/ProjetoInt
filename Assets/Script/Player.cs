using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 4f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private SpriteRenderer sr; // 👈 vamos usar pra flipar o sprite

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>(); // 👈 pega o SpriteRenderer
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Atualiza parâmetros no Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        bool isWalking = movement.sqrMagnitude > 0;
        animator.SetBool("isWalking", isWalking);

        // 👇 Flip no sprite para reaproveitar Walk Left como Walk Right
        if (movement.x > 0)
        {
            sr.flipX = true;  // andando para direita
        }
        else if (movement.x < 0)
        {
            sr.flipX = false; // andando para esquerda (normal)
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lab"))
        {
            SceneManager.LoadScene("Laboratorio");
        }

        if (collision.gameObject.CompareTag("worlds"))
        {
            SceneManager.LoadScene("ecolhaMapa");
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ct"))
        {
            SceneManager.LoadScene("Saladecontrole");
        }

        if (other.gameObject.CompareTag("door"))
        {
            SceneManager.LoadScene("Corredor");
        }
        
    }


}