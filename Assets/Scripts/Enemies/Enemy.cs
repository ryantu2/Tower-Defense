using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

// Attribute
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    public Transform target;

    private NavMeshAgent agent;
    private int health = 0;

    // Use this for initialization
    void Start()
    {
        // Set to max health at start
        health = maxHealth;
        // Get NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // Follow destination
        agent.SetDestination(target.position);
    }

    // Call this to deal damage to enemy
    public void TakeDamage(int damage)
    {
        // Reduce health with damage
        health -= damage;
        // If health reaches zero
        if (health <= 0)
        {
            // Destroy the game object
            Destroy(gameObject);
        }
    }
}
   



