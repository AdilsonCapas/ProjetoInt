using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI; // Arraste aqui o Canvas/Panel de pausa

    void Update()
    {
        // Detecta tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Esconde menu
        Time.timeScale = 1f;           // Continua o jogo
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Mostra menu
        Time.timeScale = 0f;           // Congela o jogo
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;           // Reseta o tempo antes de recarregar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Laboratorio"); // Troque "Menu" pelo nome da sua cena de menu
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
