using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI PlayerHealth;

    private void OnGUI() {
        PlayerHealth.text = "Current Wave: " + EnemySpawner.main.currentWave.ToString() + " | "
        + "Enemies: " + EnemySpawner.main.enemiesLeft.ToString() + "/"
        + EnemySpawner.main.totalEnemies.ToString() + " "
        + "Health: " + LevelManager.main.playerHealth.ToString() + "/3"
        + " | Press Escape to exit";
    }
}
