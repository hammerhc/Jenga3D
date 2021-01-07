using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float highScore;
    public float soundVolume;
    public float musicVolume;

    public GameData(GameManager gameManager, AudioManager audioManager)
    {
        highScore = gameManager.GetHighScore();
        soundVolume = audioManager.GetVolume("laserShot");
        musicVolume = audioManager.GetVolume("backgroundMusic");
    }
}
