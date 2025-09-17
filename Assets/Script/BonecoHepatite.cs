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

    [Header("Tiro")]
    public GameObject projetilPrefab; // prefab da bala
    public Transform pontoDisparo;    // posição de onde sai a bala
    public float velocidadeProjetil = 10f;
    public float tempoEntreTiros = 0.5f; // cooldown entre tiros
    private float timerTiro = 0f;        // contador do cooldown
    private bool estaAtirando = false;   // controle do estado de tiro

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        escalaOriginal = transform.localScale;
        pontoInicial = transform.position; // Salva a posição inicial
    }

    void Update()
    {
        // Atualiza o timer do cooldown
        timerTiro += Time.deltaTime;

        // Toggle animação de tiro com T
        if (Input.GetKeyDown(KeyCode.T))
        {
            estaAtirando = !estaAtirando;
            anim.SetBool("estaAtirando", estaAtirando);

            if (estaAtirando)
            {
                anim.Play("Tiro", -1, 0f); // entra na animação de tiro
            }
            else
            {
                anim.Play("Idle", -1, 0f); // volta para Idle
            }
        }

        // Disparo com botão esquerdo do mouse
        if (estaAtirando && Input.GetMouseButtonDown(0))
        {
            if (timerTiro >= tempoEntreTiros)
            {
                Disparar();
                timerTiro = 0f; // reseta cooldown
            }
        }

        // Movimento, pulo e escalada só se não estiver atirando
        if (!estaAtirando)
        {
            Move();
            Jump();
            Climb();
        }
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbSpeed);
            anim.SetBool("estaEscalando", true);
        }
        else
        {
            anim.SetBool("estaEscalando", false);
        }
    }

    void Disparar()
    {
        if (projetilPrefab == null || pontoDisparo == null)
            return;

        GameObject bala = Instantiate(projetilPrefab, pontoDisparo.position, Quaternion.identity);

        float direcao = Mathf.Sign(transform.localScale.x); // direção do personagem
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
        if (rbBala != null)
        {
            rbBala.linearVelocity = new Vector2(velocidadeProjetil * direcao, 0f);
        }

        // opcional: destruir bala depois de 3 segundos para limpar cena
        Destroy(bala, 3f);
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
            StartCoroutine(TomarDanoERespawn());
        }
        if (collision.gameObject.CompareTag("Vilao"))
        {
            StartCoroutine(TomarDanoERespawn());
        }

        // Portais
        if (collision.gameObject.CompareTag("Portal"))
        {
            SceneManager.LoadScene("Daviteama");
        }
        if (collision.gameObject.CompareTag("Portal2"))
        {
            SceneManager.LoadScene("Jamal");
        }
        if (collision.gameObject.CompareTag("Portal3"))
        {
            SceneManager.LoadScene("Vermelho");
        }
        if (collision.gameObject.CompareTag("Portal4"))
        {
            SceneManager.LoadScene("Vermelho");
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

    IEnumerator TomarDanoERespawn()
    {
        anim.SetTrigger("tomouDano");
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        yield return new WaitForSeconds(1f);

        Respawn();

        rb.simulated = true;

        anim.ResetTrigger("tomouDano");
        anim.SetBool("estaCorrendo", false);
        anim.SetBool("estaPulando", false);
        anim.SetBool("estaEscalando", false);

        anim.Play("Idle", -1, 0f);
    }

    void Respawn()
    {
        transform.position = pontoInicial;
        rb.linearVelocity = Vector2.zero;
    }
}
