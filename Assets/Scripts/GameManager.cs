using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int levelNumberIndex = 0;
    private static int totalScore = 0;
    private float time;
    private bool isGameStarted = false; 
    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [field: SerializeField] public int Coins { get; private set; } = 0;
    [field: SerializeField] public int Score { get; private set; } = 0;
    public static GameManager Instance {get; private set;}
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    
    private void Awake()
    {
        Coins = 0;
        Score = 0;
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickup += AddCoins;
        Lander.Instance.OnLanded += AddScore;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;
        GameInput.Instance.OnMenuButtonPress += TogglePauseMenu;
        LoadCurrentLevel();
    }
    

    private void Lander_OnStateChanged(object sender, LanderState state)
    {
        if (state == LanderState.Normal)
        {
            isGameStarted = true;
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetNormalOrthographicSize();
            return;
        }
        
        isGameStarted = false;
    }

    private void Update()
    {
        if (isGameStarted)
        {
            time += Time.deltaTime;
        }
    }

    private void AddCoins(object sender, int amount)
    {
        Coins += amount;
        Debug.Log("Total Coins" + Coins);
    }

    private void AddScore(object sender, LandingEventArgs args)
    {
        Score += args.Score;
        Debug.Log("Total Score" + Score);
    }
    
    public float GetTime() { return Mathf.Round(time); }

    private void LoadCurrentLevel()
    {
        int maxIndex = gameLevelList.Count;

        if (levelNumberIndex >= 0 && levelNumberIndex < maxIndex)
        {
            var gameLevel =  gameLevelList[levelNumberIndex];
            Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
            Lander.Instance.transform.position = gameLevel.GetLanderStartPosition();
            cinemachineCamera.Target.TrackingTarget = gameLevel.GetCameraTargetStart();
            CinemachineCameraZoom2D.Instance.SetTargetOrthographicSize(gameLevel.GetZoomedOutOrthographicSize());
            return;
        }
        
        
        SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
    }

    public void GoToNextLevel()
    {
        levelNumberIndex++;
        totalScore += Score;
        // if (levelNumberIndex >= gameLevelList.Count)
        // {
        //     Debug.LogError("No more levels");
        //     return;
        // }
        
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }

    public void RetryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }

    public void ResetStaticData()
    {
        totalScore = 0;
        levelNumberIndex = 0;
    }
    
    public int GetCurrentLevel()
    {
        return levelNumberIndex + 1;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
     
    private void TogglePauseMenu(object sender, EventArgs e)
    {
        if (Time.timeScale == 0)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }    
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }
}
