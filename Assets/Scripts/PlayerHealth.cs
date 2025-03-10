using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 100; // Vida máxima
    private int currentHealth; // Vida actual
    private CameraShake cameraShake;
    public Slider slider; //barra de vida

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida al máximo
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
        Debug.Log("Jugador recibió " + damage + " de daño. Vida restante: " + currentHealth);

        if (cameraShake != null)
        {
            cameraShake.StartShake(); // Activa la animación de sacudida de la cámara
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
        // Aquí podrías agregar una animación de muerte, reiniciar la escena, etc.
    }
}
