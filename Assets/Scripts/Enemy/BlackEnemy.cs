using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BlackEnemy : Enemy
{
    public EnemyProjectile projectilePrefab;
    public Transform firePoint; 
    public float fireRate = 1.5f;
    public AudioSource audioSourceB;
    public AudioClip shootSound;
    private float nextFireTime = 0f;

    public override void Start()
    {
        base.Start(); 

        enemyName = "Enemigo Negro";
        GetComponent<EnemyHealth>().InitializeHealth(100);
        speed = 5f;
        attackRange = 20f;

        if (firePoint == null)
        {
            firePoint = transform.Find("FirePoint");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time >= nextFireTime)
            {
                ShootAtPlayer();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    // POLYMORPHISM
    public override void MoveTowardsPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            rb.velocity = Vector3.zero; 
            return;
        }

        base.MoveTowardsPlayer();
    }
    void ShootAtPlayer()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // Instanciar directamente como EnemyProjectile
        EnemyProjectile projectile = Instantiate(projectilePrefab, firePoint.position + firePoint.forward * 1f, Quaternion.identity);

        if (projectile != null)
        {
            projectile.Initialize(30, "Player");
            projectile.Launch((player.position - firePoint.position).normalized);
            audioSource.PlayOneShot(shootSound);
        }

        
    }



}