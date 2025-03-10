using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : Enemy
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde dispara el enemigo
    public float projectileSpeed = 15f; // Velocidad del proyectil
    public float fireRate = 1.5f; // Tiempo entre disparos

    private float nextFireTime = 0f;

    public override void Start()
    {
        base.Start(); // Llama al Start() de EnemyBase
        enemyName = "Enemigo Negro";
        health = 50;
        speed = 2.5f;
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

    public override void MoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
    }

    public override void OnCollisionEnter(Collision collision)
    {

    }

    public override void CheckPlayerDistance()
    {
        // BlackEnemy no usa este método para hacer daño, solo dispara con proyectiles
    }

    public override void TakeDamage(int damageAmount)
    {
        Debug.Log(enemyName + " resiste el golpe con su armadura.");
        base.TakeDamage(damageAmount); 
    }

    void ShootAtPlayer()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }
}
