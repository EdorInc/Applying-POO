using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); // Llama al Start() de EnemyBase
        enemyName = "Blue Enemy";
        health = 50;
        speed = 5f;
        damage = 10;
    }

    public override void MoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
    }
}
