using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 100; // Vida m�xima
    private int currentHealth; // Vida actual
    private CameraShake cameraShake;
    public Image healthBarFill; // Referencia a la barra de vida

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida al m�ximo
        UpdateHealthUI();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibi� " + damage + " de da�o. Vida restante: " + currentHealth);

        if (cameraShake != null)
        {
            cameraShake.StartShake(); // Activa la animaci�n de sacudida de la c�mara
        }
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        Debug.Log("Jugador ha muerto.");
        // Aqu� podr�as agregar una animaci�n de muerte, reiniciar la escena, etc.
    }
}
