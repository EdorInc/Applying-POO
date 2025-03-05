using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima
    private int currentHealth; // Vida actual
    private CameraShake cameraShake;

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida al m�ximo
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
