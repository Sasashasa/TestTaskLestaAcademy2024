using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        LevelScene,
    }

    public static void LoadScene(Scene targetScene)
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}