using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Daño que hace el proyectil

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else if (collision.gameObject.CompareTag("Player")) // Solo afecta al jugador
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                // Sacudir la cámara solo si el proyectil golpea al jugador
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    cameraShake.StartShake();
                }
            }
            
        }

        // Destruir el proyectil en cualquier colisión
        Destroy(gameObject);
    }
}
