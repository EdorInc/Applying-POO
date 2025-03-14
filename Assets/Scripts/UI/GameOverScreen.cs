using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject deathScreen;
    public CameraController cameraController;

    private void Start()
    {
        deathScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Bloquear cursor al inicio
        Cursor.visible = false; // Ocultar cursor al inicio
    }

    public void ShowGameOverScreen()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        cameraController.enabled = false;
        PlayerShooting.isMenuActive = true;

        // Habilitar el cursor para interactuar con la UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void TryAgain()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathScreen.SetActive(false);
        PlayerShooting.isMenuActive = false;
    }
}
