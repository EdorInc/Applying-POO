using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab de la pelota
    public Transform firePoint; // Punto desde donde se disparan los proyectiles
    private float projectileSpeed = 50f; // Velocidad del proyectil
    private float fireRate = 0.1f; // Tiempo entre disparos

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }

        // Ignorar colisión con el jugador
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
    }

}
