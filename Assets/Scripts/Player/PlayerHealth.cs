using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 100; // Vida máxima

    private int currentHealth;
    public int CurrentHealth  // ENCAPSULATION
    {
        get { return currentHealth; }
        private set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    private bool isDead;
    public bool IsDead  // ENCAPSULATION
    {
        get { return isDead; }
        private set { isDead = value; }
    }

    private CameraShake cameraShake;
    public Slider slider;
    public GameOverScreen gameOverScreen;

    void Start()
    {
        CurrentHealth = maxHealth;
        cameraShake = Camera.main.GetComponent<CameraShake>();
        SetHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (cameraShake != null)
        {
            cameraShake.StartShake();
        }

        SetHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void SetHealth(int health)
    {
        if (slider != null)
        {
            slider.value = health;
        }
    }


    void Die()
    {
        Debug.Log("Jugador ha muerto.");
        gameOverScreen.ShowGameOverScreen();
        IsDead = true;
    }
}
