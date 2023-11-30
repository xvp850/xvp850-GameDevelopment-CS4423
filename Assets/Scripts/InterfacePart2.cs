using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfacePart2 : MonoBehaviour
{
    [Header("References")]
    //[SerializeField] TextMeshProUGUI PlayerHealth;
    [SerializeField] TextMeshProUGUI waveInformation;

    private void OnGUI() {
        //PlayerHealth.text = "Health: " + LevelManager.main.playerHealth.ToString() + "/3"
        //+ " | Press Escape to exit";

        waveInformation.text = "Current Wave: " + EnemySpawner.main.currentWave.ToString() + " | "
        + "Enemies: " + EnemySpawner.main.enemiesLeft.ToString() + "/"
        + EnemySpawner.main.totalEnemies.ToString();
    }
}
