using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public PlayerProjectile projectilePrefab; 
    public Transform firePoint; 
    public AudioSource audioSource;
    public AudioClip shootSound;
    public static bool isMenuActive = false; 

    private float projectileSpeed = 50f; 
    private float fireRate = 0.1f; 
    private float nextFireTime = 0f;

    void Update()
    {
        if (isMenuActive) return;
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        PlayerProjectile projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Ignorar colisión con el jugador
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
    }

}
