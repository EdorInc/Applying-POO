using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : Enemy
{
    public override void Start()
    {
        base.Start(); // Llama al Start() de EnemyBase
        enemyName = "Enemigo Negro";
        health = 100;
        speed = 1.5f;
        damage = 20;
    }

    public override void MoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
    }

    public override void TakeDamage(int damageAmount)
    {
        Debug.Log(enemyName + " resiste el golpe con su armadura.");
        base.TakeDamage(damageAmount / 2); // Recibe la mitad del daño
    }
}
