using UnityEngine;
using UnityEngine.Video;

public class Computador : MonoBehaviour
{
    [Header("UI e Vídeo")]
    public GameObject computerPanel; // Painel da UI (arrastar no Inspector)
    public VideoPlayer videoPlayer;  // Video Player do computador (arrastar no Inspector)

    private bool playerNearby = false;

    void Start()
    {
        // Inicialmente, painel e vídeo desativados
        if (computerPanel != null)
            computerPanel.SetActive(false);
        if (videoPlayer != null)
            videoPlayer.gameObject.SetActive(false);
    }

    void Update()
    {
        // Apenas ativa/desativa quando jogador está colidindo
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Alterna painel
            if (computerPanel != null)
                computerPanel.SetActive(!computerPanel.activeSelf);

            // Alterna vídeo
            if (videoPlayer != null)
            {
                if (!videoPlayer.gameObject.activeSelf)
                {
                    videoPlayer.gameObject.SetActive(true);
                    videoPlayer.Play();
                }
                else
                {
                    videoPlayer.Stop();
                    videoPlayer.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerNearby = false;

            // Fecha painel
            if (computerPanel != null && computerPanel.activeSelf)
                computerPanel.SetActive(false);

            // Para vídeo
            if (videoPlayer != null && videoPlayer.gameObject.activeSelf)
            {
                videoPlayer.Stop();
                videoPlayer.gameObject.SetActive(false);
            }
        }
    }
}
