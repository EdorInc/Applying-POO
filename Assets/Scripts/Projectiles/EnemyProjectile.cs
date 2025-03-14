using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    void Start()
    {
        speed = 80f;
        damage = 30; 
        targetTag = "Player"; // Solo afecta al jugador
    }

    public void Initialize(int projectileDamage, string target)
    {
        damage = projectileDamage;
        targetTag = target;
    }

    public override void Launch(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * speed, ForceMode.Impulse);
        }
    }

    protected override void ApplyDamage(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);

            // Sacudir la cámara cuando el jugador recibe un impacto
            CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
            if (cameraShake != null)
            {
                cameraShake.StartShake();
            }
        }
    }
}
