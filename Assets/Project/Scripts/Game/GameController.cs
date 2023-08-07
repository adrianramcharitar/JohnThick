using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameObject enemyContainer;

    [Header("UI")]
    public Text ammoText;
    public Text healthText;
    public Text enemyText;

    public Text infoText;


    void Start()
    {
        infoText.gameObject.SetActive(false);
    }

    void Update()
    {
        healthText.text = "Health: " + player.Health;
        ammoText.text = "Ammo: " + player.Ammo;

        int aliveEnemies = 0;

        foreach (Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>())
        {
            if (!enemy.Killed)
            {
                aliveEnemies++;
            }
        }
        enemyText.text = "Enemies " + aliveEnemies;

        if (aliveEnemies == 0)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "You Win!";
        }

        if (player.Killed)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "You Lose!";
        }

    }
}
