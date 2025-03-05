using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public string enemyName;
    public int health;
    public float speed;
    public int damage;
    public float damageCooldown = 1.5f;
    public float attackRange = 1f; // Distancia para atacar con Raycast
    public float pushForce = 5f; // Fuerza de empuje

    private bool isTouchingPlayer = false;
    private PlayerHealth playerHealth;
    protected Rigidbody rb;
    protected Transform player; // Referencia al jugador

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Busca al jugador
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
        CheckPlayerDistance();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(enemyName + " golpeó al jugador.");

            // Obtener el script de vida del jugador
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Le quita vida según el daño del enemigo

                // Iniciar daño recurrente cada X segundos
                if (!isTouchingPlayer)
                {
                    isTouchingPlayer = true;
                    InvokeRepeating("ApplyContinuousDamage", damageCooldown, damageCooldown);
                }

                // Empujar al jugador hacia atrás
                rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                    pushDirection.y = 0.2f;
                    float pushForce = 5f;
                    rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                }
            }

            // Obtener Rigidbody del jugador para empujarlo
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                pushDirection.y = 0.2f; // Pequeño impulso hacia arriba

                float pushForce = 5f;
                playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            CancelInvoke("ApplyContinuousDamage"); // Detiene el daño si el jugador escapa
        }
    }

    void ApplyContinuousDamage()
    {
        if (isTouchingPlayer && playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log(enemyName + " sigue haciendo daño al jugador.");
        }
    }

    public virtual void MoveTowardsPlayer()
    {
        if (player == null || isTouchingPlayer) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // No cambia la altura

        // Aceleración progresiva usando Lerp
        Vector3 targetVelocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5f);
    }

    // Verifica si el jugador está dentro del rango de ataque
    void CheckPlayerDistance()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!isTouchingPlayer)
                {
                    isTouchingPlayer = true;
                    playerHealth = hit.collider.GetComponent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                        ApplyPushEffect(hit.collider); // Aplica empuje una sola vez
                        InvokeRepeating("ApplyContinuousDamage", damageCooldown, damageCooldown);
                    }
                }
            }
        }
        else
        {
            isTouchingPlayer = false;
            CancelInvoke("ApplyContinuousDamage");
        }
    }

    // Empuja al jugador solo una vez por golpe
    void ApplyPushEffect(Collider playerCollider)
    {
        Rigidbody playerRb = playerCollider.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 pushDirection = (playerRb.transform.position - transform.position).normalized;
            pushDirection.y = 0.2f; // Pequeño impulso hacia arriba

            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }


    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log(enemyName + " recibió " + damageAmount + " de daño. Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(enemyName + " ha muerto.");
        Destroy(gameObject);
    }
}
