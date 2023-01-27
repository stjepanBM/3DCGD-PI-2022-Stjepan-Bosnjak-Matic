using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Respawning
    //Scene manipulation

    private float timeUntilPlayerDies = 3f;

    public void PlayerRespawn(int playerLife)
    {
        if (playerLife <= 0)
        {
            GameToMainMenu();
        }
        else
        {
            StartCoroutine(PlayerRespawnCo());
        }
    }

    public void GameToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator PlayerRespawnCo()
    {
        yield return new WaitForSeconds(timeUntilPlayerDies);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
