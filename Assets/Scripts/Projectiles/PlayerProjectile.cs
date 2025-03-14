using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    void Start()
    {
        speed = 3;
        damage = 50; 
        targetTag = "Enemy"; 
    }

    protected override void ApplyDamage(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}

