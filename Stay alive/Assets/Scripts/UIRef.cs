using UnityEngine;
using UnityEngine.SceneManagement;

public class UIRef : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void GameMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Shop()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
}
