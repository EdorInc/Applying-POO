using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el editor
        #else
                    Application.Quit(); // Cierra la aplicación en una build
        #endif
    }

}
