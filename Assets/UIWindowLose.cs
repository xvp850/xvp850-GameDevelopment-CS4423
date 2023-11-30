using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowLose : MonoBehaviour
{
    public void ShowLose() {
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;
    }
}
