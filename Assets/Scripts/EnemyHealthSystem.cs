using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{

    public int health = 5;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        health-=damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
            if (gameObject.name.Contains("Caco"))
            {

                FindObjectOfType<PlayerHealthSystem>().HealPlayer(5);
            }
        }
    }
}
