using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// INHERITANCE
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float attackRange = 1f;
    public Slider healthBar;
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public int maxHealth;

    private float damageCooldown = 1.5f;
    private float pushForce = 5f;
    private bool isTouchingPlayer = false;
    private PlayerHealth playerHealth;
    protected Rigidbody rb;
    protected Transform player;
    private EnemyHealth enemyHealth; 

    protected float speed;
    public float Speed  // ENCAPSULATION
    {
        get { return speed; }
        protected set { speed = Mathf.Max(0, value); }
    }

    protected int damage;
    public int Damage  // ENCAPSULATION
    {
        get { return damage; }
        protected set { damage = Mathf.Max(0, value); }
    }

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        enemyHealth = GetComponent<EnemyHealth>();

        InitializeHealth();
    }

    protected virtual void InitializeHealth()
    {
        // Esto se sobrescribe en las subclases
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
        CheckPlayerDistance();
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                ApplyPushEffect(collision.collider);

                if (!isTouchingPlayer)
                {
                    isTouchingPlayer = true;
                    InvokeRepeating("ApplyContinuousDamage", damageCooldown, damageCooldown);
                }
            }
        }
    }


    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            CancelInvoke("ApplyContinuousDamage");
        }
    }


    void ApplyContinuousDamage()
    {
        if (isTouchingPlayer && playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public virtual void MoveTowardsPlayer()
    {
        if (player == null || isTouchingPlayer) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        Vector3 targetVelocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5f);
    }

    public virtual void CheckPlayerDistance()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {

                // Si el enemigo es BlackEnemy, solo detecta al jugador y NO aplica daño.
                if (this is BlackEnemy)
                {
                    return;
                }

                // Si el enemigo es BlueEnemy, aplica daño si hay colisión física.
                if (!isTouchingPlayer)
                {
                    isTouchingPlayer = true;
                    playerHealth = hit.collider.GetComponent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                        ApplyPushEffect(hit.collider);
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


    void ApplyPushEffect(Collider playerCollider)
    {
        Rigidbody playerRb = playerCollider.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 pushDirection = (playerRb.transform.position - transform.position).normalized;
            pushDirection.y = 0.2f;

            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount);
        }
    }

    public virtual void Die()
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
        tempAudioSource.clip = explosionSound;
        tempAudioSource.Play();
        Destroy(tempAudio, explosionSound.length);
        Destroy(gameObject);
    }
}
