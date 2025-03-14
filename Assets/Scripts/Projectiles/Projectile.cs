using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected int damage;
    public float speed; 
    protected string targetTag; // Determina a quién afecta este proyectil

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag)) // Solo afecta al objetivo correcto
        {
            ApplyDamage(collision.gameObject);
        }

        Destroy(gameObject);
    }

    public virtual void Launch(Vector3 direction)
    {
        
        
    }

    protected virtual void ApplyDamage(GameObject target) // ABSTRACTION
    {
        // Implementación en clases derivadas
    }
}
