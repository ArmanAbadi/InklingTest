using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public delegate void GameInitialized();
    public GameInitialized gameInitialized;

    public GameObject GameOverScreen;

    public Text GameTimeText;
    float Timer = 0;
    float TimeMarker = 0;

    bool GameIsPlaying = false;

    public Text HighScore;

    public enum GameState
    {
        StartMenu,
        Playing,
        Paused
    }

    public GameState gameState = GameState.StartMenu;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        gameInitialized += Initialized;
        UpdateHighscoreText();
    }
    private void Initialized()
    {
        GameIsPlaying = false;
        GameTimeText.text = "";
    }
    private void Update()
    {
        if (gameState != GameState.Playing) return;
        if (!GameIsPlaying)
        {
            GameIsPlaying = true;
            TimeMarker = Time.time;
        }
        Timer = Time.time - TimeMarker;
        GameTimeText.text = Timer.ToString("F1") +" s";
    }

    public void PlayGame()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1;
        Physics.gravity = new Vector3(0,-20,0);
    }

    public void PauseGame()
    {
        gameState = GameState.Paused;
        Time.timeScale = 0;
    }
    public void ReturnToStartMenu()
    {
        gameState = GameState.StartMenu;
        gameInitialized();
    }
    public void PlayAgain()
    {
        gameInitialized();
        PlayGame();
    }
    public void GameOver()
    {
        PauseGame();
        GameOverScreen.SetActive(true);
        if (PlayerPrefs.GetFloat("Highscore") < Time.time - TimeMarker)
        {
            PlayerPrefs.SetFloat("Highscore", Time.time - TimeMarker);
        }
        UpdateHighscoreText();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void UpdateHighscoreText()
    {
        HighScore.text = "Highscore : " + PlayerPrefs.GetFloat("Highscore").ToString("F1") + " s";
    }


}
