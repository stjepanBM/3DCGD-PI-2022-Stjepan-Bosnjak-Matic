using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int amountofHealing = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthSystem>().HealPlayer(amountofHealing);
            AudioManager.instance.PlaySFX(1);
            Destroy(gameObject);
        }
    }
}
