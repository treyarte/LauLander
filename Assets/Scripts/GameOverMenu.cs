using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private TextMeshProUGUI ScoreText;

    private void Awake()
    {
        MainMenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        MainMenuButton.Select();
        ScoreText.text = "Final Score: " + GameManager.Instance.GetTotalScore().ToString();
    }
}
