using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Nave()
    {
        SceneManager.LoadScene("Corredor");
    }
    public void Mundo1()
    {
        SceneManager.LoadScene("azul");
    }
    public void Mundo2()
    {
        SceneManager.LoadScene("Daviteama");
    }
    public void Mundo3()
    {
        SceneManager.LoadScene("Jamal");
    }
    public void Mundo4()
    {
        SceneManager.LoadScene("Vermelho");
    }


}
