using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowWin : MonoBehaviour
{
    public void ShowWin() {
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;
    }
}
