using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bannerTitle;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI nextButtonText;
    private Action nextBtnClickAction;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            nextBtnClickAction();
        }); 
    }

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }

    private void Lander_OnLanded(object sender, LandingEventArgs eventArgs)
    {
        if (eventArgs.Type == LandingType.Crash)
        {
            bannerTitle.text = "<Color=#ff0000>Crash!</color>";
            nextButtonText.text = "Retry";
            nextBtnClickAction = () => { GameManager.Instance.RetryLevel(); };
        }
        else
        {
            bannerTitle.text = "SUCCESSFUL LANDING!";
            nextButtonText.text = "Continue";
            nextBtnClickAction = () => { GameManager.Instance.GoToNextLevel(); };
        }
        
        statusText.text = Mathf.Round(eventArgs.LandingSpeed * 2f) + "\n" +
                          Mathf.Round(eventArgs.LandingAngle * 100f) + "\n" +
                          "x" + eventArgs.ScoreMultiplier + "\n" +
                          GameManager.Instance.Coins + "\n" +
                          eventArgs.Score; 
        
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
