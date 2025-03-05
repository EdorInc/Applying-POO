using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima
    private int currentHealth; // Vida actual
    private CameraShake cameraShake;

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida al máximo
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibió " + damage + " de daño. Vida restante: " + currentHealth);

        if (cameraShake != null)
        {
            cameraShake.StartShake(); // Activa la animación de sacudida de la cámara
        }

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
