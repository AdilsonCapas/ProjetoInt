using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonecoHepatite : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movimento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float climbSpeed = 3f; // velocidade de subir com "E"
    private bool noChao = true;
    private bool podeEscalar = false; // true quando está tocando a parede

    private Vector3 escalaOriginal;
    private Vector3 pontoInicial; // Ponto inicial da fase

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        escalaOriginal = transform.localScale;
        pontoInicial = transform.position; // Salva a posição inicial
    }

    void Update()
    {
        Move();
        Jump();
        Climb();
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Virar personagem
        if (moveInput != 0)
        {
            Vector3 scale = escalaOriginal;
            scale.x *= Mathf.Sign(moveInput);
            transform.localScale = scale;
        }

        anim.SetBool("estaCorrendo", moveInput != 0);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("estaPulando", true);
            noChao = false;
        }
    }

    void Climb()
    {
        // Só sobe se estiver tocando a parede
        if (podeEscalar && Input.GetKey(KeyCode.E))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbSpeed); // sobe automaticamente
            anim.SetBool("estaEscalando", true);
        }
        else
        {
            anim.SetBool("estaEscalando", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta chão
        if (collision.gameObject.CompareTag("Chao"))
        {
            noChao = true;
            anim.SetBool("estaPulando", false);
        }

        // Detecta parede para escalada
        if (collision.gameObject.CompareTag("Parede"))
        {
            podeEscalar = true;
        }

        // Detecta dano
        if (collision.gameObject.CompareTag("veneno"))
        {
            anim.SetTrigger("tomouDano");
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            Respawn(); // Volta ao início da fase
        }

        // Portal
        if (collision.gameObject.CompareTag("Portal"))
        {
            SceneManager.LoadScene("fase2 P3");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Saiu da parede
        if (collision.gameObject.CompareTag("Parede"))
        {
            podeEscalar = false;
        }
    }

    void Respawn()
    {
        transform.position = pontoInicial;
        rb.linearVelocity = Vector2.zero;
    }
}
