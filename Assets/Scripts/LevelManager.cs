using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int playerHealth;

    private Scene scene;

    public void Awake() => main  = this;

    private void Start() {
        currency = 100;
        playerHealth = 3;
        scene = SceneManager.GetActiveScene();
    }

    private void Update() {
        if(scene.name != "MainMenu") {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void DecreasePlayerHealth(int amount) {
        playerHealth -= amount;
        if(playerHealth == 0) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if(amount <= currency) {
            //buy item
            currency -= amount;
            return true;
        } else {
            Debug.Log("You do not have enoough");
            return false;
        }
    }
}
