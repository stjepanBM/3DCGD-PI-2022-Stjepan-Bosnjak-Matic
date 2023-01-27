using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private int playerLife = 1;

    UICanvasController healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<UICanvasController>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amountOfDamage)
    {
        AudioManager.instance.PlaySFX(4);
        currentHealth -= amountOfDamage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            AudioManager.instance.StopBackgroundMusic();
            AudioManager.instance.PlaySFX(3);
            playerLife--;
            FindObjectOfType<GameManager>().PlayerRespawn(playerLife);
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
}
