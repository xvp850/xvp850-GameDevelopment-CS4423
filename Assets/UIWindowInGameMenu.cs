using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIWindowInGameMenu : MonoBehaviour
{
    public static UIWindowInGameMenu main;

    public void Awake() => main  = this;

    public void ShowInGameMenu() {
        //if(EventSystem.current.IsPointerOverGameObject()) return;
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;
    }

    public void HideInGameMenu() {
        //if(EventSystem.current.IsPointerOverGameObject()) return;
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
    }
}
