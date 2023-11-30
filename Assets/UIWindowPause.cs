using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIWindowPause : MonoBehaviour
{
    public static UIWindowPause main;

    public void Awake() => main  = this;

    public void ShowPause() {
        //if(EventSystem.current.IsPointerOverGameObject()) return;
        GetComponent<Canvas>().enabled = true;
    }

    public void HidePause() {
        //if(EventSystem.current.IsPointerOverGameObject()) return;
        GetComponent<Canvas>().enabled = false;
    }
}
