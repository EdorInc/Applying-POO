using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BlueEnemy : Enemy
{
    public override void Start()
    {
        base.Start(); 

        enemyName = "Blue Enemy";
        GetComponent<EnemyHealth>().InitializeHealth(150);
        speed = 10f;
        damage = 10;
    }

    // POLYMORPHISM
    public override void MoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
    }
}
