using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    //[SerializeField] private int baseEnemeies = 10;
    [SerializeField] private float enemiesPerSecond = 3f;
    [SerializeField] public float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    [Header("Information")]
    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //Enemies Per Second
    public bool isSpawning = false;

    public int enemiesLeft;
    public int totalEnemies;

    public bool waterElementUnlock1 = false;
    public bool waterElementUnlock2 = false;
    public bool fireElementUnlock1 = false;
    public bool fireElementUnlock2 = false;
    public bool earthElementUnlock1 = false;
    public bool earthElementUnlock2 = false;
    public bool airElementUnlock1 = false;
    public bool airElementUnlock2 = false;

    [SerializeField] TextMeshProUGUI elementUnlockInformation;
    [SerializeField] TextMeshProUGUI elementInformation;
    private string message = "";

    // Observer Design Pattern
    public UnityEvent onPlayerWinEvent;

    public void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main  = this;
    }

    private void Start() {
        enemiesLeft = EnemiesPerWave();
        totalEnemies = EnemiesPerWave();
        StartCoroutine(StartWave());
        //updateElementInformation();
        updateElementUnlock();
    }

    private void Update() {
        if(!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0) {
            EndWave();
        }
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
        enemiesLeft--;
    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        // Boss Level, only spawns 1
        if(currentWave == 6 || currentWave == 30 || currentWave == 12 || currentWave == 36
            || currentWave == 18 || currentWave == 42 || currentWave == 24 ||
            currentWave == 48 || currentWave == 54) {
            totalEnemies = 1;
            enemiesLeft = 1;
            enemiesLeftToSpawn = 1;
        } else {
            totalEnemies = EnemiesPerWave();
            enemiesLeft = EnemiesPerWave();
            enemiesLeftToSpawn = EnemiesPerWave();
            eps = EnemiesPerSecond();
        }
    }
    
    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        if(currentWave != 55) {
            if(currentWave == 7) { //After Boss Wave 6
                waterElementUnlock1 = true; //unlock tower
                message = "Water I";
                updateElementUnlock();
            }
            if(currentWave == 13) { //After Boss Wave 13
                earthElementUnlock1 = true; //unlock tower
                message = "Water I\nEarth I";
                updateElementUnlock();
            }
            if(currentWave == 19) { //After Boss Wave 19
                fireElementUnlock1 = true; //unlock tower
                message = "Water I\nEarth I\nFire I";
                updateElementUnlock();
            }
            if(currentWave == 25) { //After Boss Wave 25
                airElementUnlock1 = true; //unlock tower
                message = "Water I\nEarth I\nFire I\nAir I";
                updateElementUnlock();
            }
            if(currentWave == 31) { //After Boss II Wave 31
                waterElementUnlock2 = true; //unlock tower
                message = "Water II\nEarth I\nFire I\nAir I";
                updateElementUnlock();
            }
            if(currentWave == 37) { //After Boss II Wave 37
                earthElementUnlock2 = true; //unlock tower
                message = "Water II\nEarth II\nFire I\nAir I";
                updateElementUnlock();
            }
            if(currentWave == 43) { //After Boss II Wave 43
                fireElementUnlock2 = true; //unlock tower
                message = "Water II\nEarth II\nFire II\nAir I";
                updateElementUnlock();
            }
            if(currentWave == 49) { //After Boss II Wave 49
                airElementUnlock2 = true; //unlock tower
                message = "Water II\nEarth II\nFire II\nAir II";
                updateElementUnlock();
            }
            updateElementInformation();
            StartCoroutine(StartWave());
        } else {
            //Win condition!
            onPlayerWinEvent.Invoke();
        }
    }

    private void SpawnEnemy() {
        int index = 0;
        // Normal Elementals
        if(currentWave == 1 || currentWave == 7 || currentWave == 13 || currentWave == 19
            || currentWave == 25 || currentWave == 31 || currentWave == 37 || currentWave == 43 || currentWave == 49 ) {
                index = 0;
        }
        // Water Elementals
        if(currentWave == 2 || currentWave == 8 || currentWave == 14 || currentWave == 20
            || currentWave == 26 || currentWave == 32 || currentWave == 38 || currentWave == 44 || currentWave == 50 ) {
                index = 1;
        }
        // Earth Elementals
        if(currentWave == 3 || currentWave == 9 || currentWave == 15 || currentWave == 21
            || currentWave == 27 || currentWave == 33 || currentWave == 39 || currentWave == 45 || currentWave == 51 ) {
                index = 2;
        }
        // Fire Elementals
        if(currentWave == 4 || currentWave == 10 || currentWave == 16 || currentWave == 22
            || currentWave == 28 || currentWave == 34 || currentWave == 40 || currentWave == 46 || currentWave == 52 ) {
                index = 3;
        }
        // Air Elementals
        if(currentWave == 5 || currentWave == 11 || currentWave == 17 || currentWave == 23
            || currentWave == 29 || currentWave == 35 || currentWave == 41 || currentWave == 47 || currentWave == 53 ) {
                index = 4;
        }
        // Boss Water Elemental
        if(currentWave == 6 || currentWave == 30) {
            index = 5;
        }
        // Boss Earth Elemental
        if(currentWave == 12 || currentWave == 36) {
            index = 6;
        }
        // Boss Fire Elemental
        if(currentWave == 18 || currentWave == 42) {
            index = 7;
        }
        // Boss Air Elemental
        if(currentWave == 24 || currentWave == 48) {
            index = 8;
        }
        // Final Boss
        if(currentWave == 54) {
            index = 9;
        }
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave() {
        //return Mathf.RoundToInt(baseEnemeies * Mathf.Pow(currentWave, difficultyScalingFactor));
        return 10;
    }

    private float EnemiesPerSecond() {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor),
        0f, enemiesPerSecondCap);
    }

    public int getCurrentWave() {
        return currentWave;
    }

    //Water
    public bool getWaterElementUnlock1() {
        return waterElementUnlock1;
    }
    public bool getWaterElementUnlock2() {
        return waterElementUnlock2;
    }
    //Fire
    public bool getFireElementUnlock1() {
        return fireElementUnlock1;
    }
    public bool getFireElementUnlock2() {
        return fireElementUnlock2;
    }
    //Earth
    public bool getEarthElementUnlock1() {
        return earthElementUnlock1;
    }
    public bool getEarthElementUnlock2() {
        return earthElementUnlock2;
    }
    //Air
    public bool getAirElementUnlock1() {
        return airElementUnlock1;
    }
    public bool getAirElementUnlock2() {
        return airElementUnlock2;
    }

    private void updateElementUnlock() {
        elementUnlockInformation.text = message;
    }

    private void updateElementInformation() {
        if(currentWave == 1 || currentWave == 7 || currentWave == 13 || currentWave == 19
            || currentWave == 25 || currentWave == 31 || currentWave == 37 || currentWave == 43 || currentWave == 49 || currentWave == 54) {
            elementInformation.text = "Current element: NORMAL\nNo adverse effects.";
        }
        if(currentWave == 2 || currentWave == 6 || currentWave == 30 || currentWave == 8 || currentWave == 14 || currentWave == 20
            || currentWave == 26 || currentWave == 32 || currentWave == 38 || currentWave == 44 || currentWave == 50 ) {
            elementInformation.text = "Current element: WATER\nEarth is effective against water.\nFire is not effective against water.";
        }
        if(currentWave == 3 || currentWave == 12 || currentWave == 36 || currentWave == 9 || currentWave == 15 || currentWave == 21
            || currentWave == 27 || currentWave == 33 || currentWave == 39 || currentWave == 45 || currentWave == 51 ) {
            elementInformation.text = "Current element: EARTH\nAir is effective against earth.\nWater is not effective against earth.";
        }
        if(currentWave == 4 || currentWave == 18 || currentWave == 42 || currentWave == 10 || currentWave == 16 || currentWave == 22
            || currentWave == 28 || currentWave == 34 || currentWave == 40 || currentWave == 46 || currentWave == 52 ) {
            elementInformation.text = "Current element: FIRE\nWater is effective against fire.\nAir is not effective against fire.";
        }
        if(currentWave == 5 || currentWave == 24 || currentWave == 48 || currentWave == 11 || currentWave == 17 || currentWave == 23
            || currentWave == 29 || currentWave == 35 || currentWave == 41 || currentWave == 47 || currentWave == 53 ) {
            elementInformation.text = "Current element: AIR\nFire is effective against air.\nEarth is not effective against air.";
        }
        //Braindead way but it works
    }
}
