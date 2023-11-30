using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int playerHealth;

    private Scene scene;

    //Menu variables
    public GameObject pauseMenuUI;
    public GameObject inGameMenuUI;
    public bool GameIsPaused;

    [SerializeField] TextMeshProUGUI errorMessage;

    // Observer Design Pattern
    public UnityEvent onPlayerLossEvent;

    public void Awake() => main  = this;

    private void Start() {
        currency = 400;
        playerHealth = 3;
        scene = SceneManager.GetActiveScene();
        GameIsPaused = false;
        errorMessage.text = "";
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M)) {
            if(GameIsPaused) {
                //UIWindowInGameMenu.ShowInGameMenu();
                inGameMenuUI.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1f;
                GameIsPaused = false;
            } else {
                //UIWindowInGameMenu.ShowInGameMenu();
                inGameMenuUI.GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause)) {
            if(GameIsPaused) {
                //UIWindowPause.ShowPause();
                pauseMenuUI.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1f;
                GameIsPaused = false;
            } else {
                //UIWindowPause.HidePause();
                pauseMenuUI.GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
        }
    }

    public void DecreasePlayerHealth(int amount) {
        playerHealth -= amount;
        if(playerHealth == 0) {
            //Observer Design Pattern for Player Loss
            //SceneManager.LoadScene("MainMenu");
            onPlayerLossEvent.Invoke();
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
            //Debug.Log("You do not have enough");
            ShowMessage("ERROR: You do not have enough currency.");
            return false;
        }
    }

    IEnumerator ShowMessage(string message) {
        Debug.Log("Please work");
	    errorMessage.text = message;
	    errorMessage.enabled = true;
	    yield return new WaitForSeconds(5f);
	    errorMessage.enabled = false;
    }
}
