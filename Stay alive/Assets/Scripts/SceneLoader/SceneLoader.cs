using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string GAME = "Game";
    private const string SHOP = "Shop";
    
    public void Game()
    {
        SceneManager.LoadScene(GAME);
        Time.timeScale = 1f;
    }

    public void Shop()
    {
        SceneManager.LoadScene(SHOP);
        Time.timeScale = 1f;
    }
}
