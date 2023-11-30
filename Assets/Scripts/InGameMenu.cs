using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu main;

    public static bool GameIsPausedButton = false;
    public GameObject inGameMenuUIButton;

    public void Awake() => main  = this;

    public void openInGameMenu() {
        //Menu open
        if(GameIsPausedButton) {
                inGameMenuUIButton.SetActive(false);
                Time.timeScale = 1f;
                GameIsPausedButton = false;
            } else {
                inGameMenuUIButton.SetActive(true);
                Time.timeScale = 0f;
                GameIsPausedButton = true;
        }
    }
}
