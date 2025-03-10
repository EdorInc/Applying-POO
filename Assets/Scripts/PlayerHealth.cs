using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 100; // Vida m�xima
    private int currentHealth; // Vida actual
    private CameraShake cameraShake;
    public Slider slider; //barra de vida

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida al m�ximo
        cameraShake = Camera.main.GetComponent<CameraShake>();
        SetHealth(maxHealth);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibi� " + damage + " de da�o. Vida restante: " + currentHealth);

        if (cameraShake != null)
        {
            cameraShake.StartShake(); // Activa la animaci�n de sacudida de la c�mara
        }
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jugador ha muerto.");
        // Aqu� podr�as agregar una animaci�n de muerte, reiniciar la escena, etc.
    }
}
