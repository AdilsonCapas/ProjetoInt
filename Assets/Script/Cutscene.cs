using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CutScene : MonoBehaviour
{
    public string[] textos;
    public GameObject[] imagens; 
    private int indiceAtual = 0;
    public TextMeshProUGUI textoCutsine;
    public float velocidadeDigitacao = 0.02f;
    private Coroutine coroutineDigitando; 
    //public Audios audios;

    void Start()
    {
        // Garante que apenas a primeira imagem esteja ativa no início
        AtivarSomente(indiceAtual);
        AtualizarTexto(indiceAtual);
        MostrarTextoDigitando(indiceAtual);
    }

    public void Trocar()
    {
        // Verifica se está na última imagem
        if (indiceAtual == imagens.Length - 1)
        {
            SceneManager.LoadScene("Cenajogo");
        }
        // Incrementa o índice, voltando ao 0 se passar do último
        indiceAtual = (indiceAtual + 1) % imagens.Length;
        AtivarSomente(indiceAtual);
        AtualizarTexto(indiceAtual);
        MostrarTextoDigitando(indiceAtual);
    }

    void AtivarSomente(int indiceParaAtivar)
    {
        for (int i = 0; i < imagens.Length; i++)
        {
            imagens[i].SetActive(i == indiceParaAtivar);
            // audios.TOCA = true;
            //audios.PARA = true;
            //Audios.instance.Pipipi();
        }
    }
    void AtualizarTexto(int indice)
    {
        if (textoCutsine != null && textos.Length > indice)
        {
            textoCutsine.text = textos[indice];
            //audios.PARA = true;
            //Audios.instance.Pipipi();
        }
    }
    void MostrarTextoDigitando(int indice)
    {
        if (textoCutsine != null && textos.Length > indice)
        {
            // Para a animação anterior (caso esteja em andamento)
            if (coroutineDigitando != null)
            {
                StopCoroutine(coroutineDigitando);
            }
            coroutineDigitando = StartCoroutine(DigitarTexto(textos[indice]));
            //audios.PARA = true;
            //Audios.instance.Pipipi();
            
        }
    }

    IEnumerator DigitarTexto(string texto)
    {
        textoCutsine.text = "";
        foreach (char letra in texto)
        {
            textoCutsine.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
            
        }
    }
}
