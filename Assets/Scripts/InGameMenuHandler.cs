using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuHandler : MonoBehaviour
{
    private Scene scene;

    public void ResumeGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitToMainMenu(){
        if(scene.name != "MainMenu") {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
