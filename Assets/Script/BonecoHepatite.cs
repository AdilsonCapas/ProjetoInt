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
    private bool noChao = true;

    private Vector3 escalaOriginal; // salva a escala original

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        escalaOriginal = transform.localScale; // salva a escala inicial
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // A/D ou setas
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip do personagem mantendo escala original
        if (moveInput != 0)
        {
            Vector3 scale = escalaOriginal;
            scale.x *= Mathf.Sign(moveInput); // esquerda = -1, direita = 1
            transform.localScale = scale;
        }

        // Anima��o de correr
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta ch�o
        if (collision.gameObject.CompareTag("Chao"))
        {
            noChao = true;
            anim.SetBool("estaPulando", false);
        }

        // Detecta inimigo ou dano
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            anim.SetTrigger("tomouDano");
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // para de se mover ao sofrer dano
        }
        if (collision.gameObject.CompareTag("Portal"))
        {
           SceneManager.LoadScene("fase2 P3");
        }
    }
}
