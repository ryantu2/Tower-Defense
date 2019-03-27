using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    public int damage = 10; // Damage of the tower
    public float attackRate = 1f; // How fast the tower attacks
    public float attackRange = 2f; // How far the tower attacks

    protected Enemy currentEnemy; // Current target to shoot at

    private float attackTimer = 0f; // Time elapsed for attacking

    void OnDrawGizmosSelected()
    {
        // Draw the attack sphere around Tower
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Aims at a given enemy every frame
    public virtual void Aim(Enemy e)
    {
        print("I am aiming at '" = e.name + "'");
    }

    // Atacks given enemy only when 'attacking'
    public virtual void Attack(Enemy e)
    {
        print("I am attacking '" + e.name + "'");
    }

    protected virtual void DetectEnemy()
    {
        // Reset currentEnemy (to null)
        currentEnemy = null;
        // Get hit colliders from OverlapSphere
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        // Loop through all hit colliders
        foreach (var hit in hits)
        {
            // If we hit an enemy
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                // Set currentEnemy to enemy
                currentEnemy = enemy;
            }
        }
    }

    // Protected - Accessible to Cannon / Other Tower Classes (Derivatives)
    // Virtual - Able to change what this function does in derived classes
    protected virtual void Update()
    {
        // Detect enemies before performing attack logic
        DetectEnemy();
        // Count up the timer
        attackTimer += Time.deltaTime;
        // If there is a current enemy
        if (currentEnemy)
        {
            // Aim at the enemy
            Aim(currentEnemy);
            // Is attack timer ready?
            if (attackTimer >= attackRate)
            {
                // Attack the enemy!
                Attack(currentEnemy);
                // Reset the timer
                attackTimer = 0;
            }
        }
    }
}

