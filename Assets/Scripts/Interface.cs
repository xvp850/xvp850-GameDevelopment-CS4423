using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI PlayerHealth;
    [SerializeField] TextMeshProUGUI waveInformation;

    private float currentTimer = 0f;

    void Start() {
        currentTimer = EnemySpawner.main.timeBetweenWaves;
        currentTimer *= 100;
        currentTimer = Mathf.Floor(currentTimer);
        currentTimer /= 100;
    }

    void Update() {
        currentTimer -= Time.deltaTime * 1;
        if(EnemySpawner.main.isSpawning == true) {
            currentTimer = EnemySpawner.main.timeBetweenWaves; //reset
        }
    }

    private void OnGUI() {
        PlayerHealth.text = "Health: " + LevelManager.main.playerHealth.ToString() + "/3";
        //+ " | Press Escape to exit";

        if(EnemySpawner.main.isSpawning == false) {
            waveInformation.text = "Current Wave spawning in: " + currentTimer;
        } else {
            waveInformation.text = "Current Wave: " + EnemySpawner.main.currentWave.ToString() + " | "
            + "Enemies: " + EnemySpawner.main.enemiesLeft.ToString() + "/"
            + EnemySpawner.main.totalEnemies.ToString();
        }
    }
}
