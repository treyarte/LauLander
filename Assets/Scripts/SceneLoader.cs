using UnityEngine.SceneManagement;

public class SceneLoader
{
    public enum Scene
    {
        GameScene,
        MainMenuScene,
        GameOverScene
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());   
    }
}