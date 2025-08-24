using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float time;
    private bool isGameStarted = false; 
    private static int levelNumberIndex;
    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [field: SerializeField] public int Coins { get; private set; } = 0;
    [field: SerializeField] public int Score { get; private set; } = 0;
    public static GameManager Instance {get; private set;}
    
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

        if (maxIndex < levelNumberIndex)
        {
            Debug.LogError("Invalid Level Index");
            return;
        }
        
        var gameLevel =  gameLevelList[levelNumberIndex];
        
        Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = gameLevel.GetLanderStartPosition();
        cinemachineCamera.Target.TrackingTarget = gameLevel.GetCameraTargetStart();
        CinemachineCameraZoom2D.Instance.SetTargetOrthographicSize(gameLevel.GetZoomedOutOrthographicSize());
    }

    public void GoToNextLevel()
    {
        levelNumberIndex++;
        if (levelNumberIndex >= gameLevelList.Count)
        {
            Debug.LogError("No more levels");
            return;
        }
        
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    public int GetCurrentLevel()
    {
        return levelNumberIndex + 1;
    }
}
