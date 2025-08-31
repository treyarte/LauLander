using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        StartButton.Select();
        StartButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });
        
        QuitButton.onClick.AddListener(Application.Quit);
    }

    
}
