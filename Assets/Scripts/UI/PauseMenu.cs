using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI; 
    private bool isPaused = false;
    public CameraController cameraController;
   
    void Start()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Bloquear cursor al inicio
        Cursor.visible = false; // Ocultar cursor al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f; 
        cameraController.enabled = false;
        isPaused = true;
        PlayerShooting.isMenuActive = true;

        // Habilitar el cursor para interactuar con la UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;
        cameraController.enabled = true;
        isPaused = false;
        PlayerShooting.isMenuActive = false;

        // Ocultar el cursor al reanudar el juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
