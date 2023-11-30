using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip playerWinSound;
    [SerializeField] AudioClip playerLossSound;

    public void onPlayerWinSound() {
        //play audio, playerWinSound
    }

    public void onPlayerLossSound() {
        //play audio, playerLossSound
    }

}
