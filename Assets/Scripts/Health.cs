using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health main;

    [Header("Attributes")] 
    [SerializeField] private float hitPoints = 2f;
    [SerializeField] private int currencyWorth = 1;
    [SerializeField] public string elementType = "normal";

    private bool isDestroyed = false;

    public void Awake() {
        main  = this;
    }

    private void Start() {
        hitPoints = hitPoints * EnemySpawner.main.getCurrentWave();
    }

    /*
    water against fire = 1.5 *
    fire against air = 1.5
    earth against water = 1.5 *
    air against earth = 1.5 *

    water against earth = .5 *
    air against fire = .5 *
    fire against water = .5 *
    earth against air = .5 *
    */
    public void TakeDamage(float damage, string elementBulletType) {
        if(elementType == "normal") {
            hitPoints -= damage;
        }
        if(elementType == "water") {
            if(elementBulletType == "earth") {
                hitPoints -= damage * 1.5f;
            }
            else if(elementBulletType == "fire") {
                hitPoints -= damage * 0.5f;
            } else {
                hitPoints -= damage;
            }
        }
        if(elementType == "fire") {
            if(elementBulletType == "water") {
                hitPoints -= damage * 1.5f;
            }
            else if(elementBulletType == "air") {
                hitPoints -= damage * 0.5f;
            } else {
                hitPoints -= damage;
            }
        }
        if(elementType == "earth") {
            if(elementBulletType == "air") {
                hitPoints -= damage * 1.5f;
            }
            else if(elementBulletType == "water") {
                hitPoints -= damage * 0.5f;
            } else {
                hitPoints -= damage;
            }
        }
        if(elementType == "air") {
            if(elementBulletType == "fire") {
                hitPoints -= damage * 1.5f;
            }
            else if(elementBulletType == "earth") {
                hitPoints -= damage * 0.5f;
            } else {
                hitPoints -= damage;
            }
        }
        if(hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth*EnemySpawner.main.currentWave);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
