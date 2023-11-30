using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIWindow : MonoBehaviour
{

    public void Show() {
        //if(EventSystem.current.IsPointerOverGameObject()) return;
        GetComponent<Canvas>().enabled = true;
    }

    public void Hide() {
        //if(EventSystem.current.IsPointerOverGameObject()) return;
        GetComponent<Canvas>().enabled = false;
    }
}
