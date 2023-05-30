using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void Menu()
    {
        SceneManager.LoadScene(Constants.Scene.GAME);
    }
    
    public async Task RestartGame()
    {
        var loadScene = SceneManager.LoadSceneAsync(Constants.Scene.GAME, LoadSceneMode.Single);
        
        while (loadScene.isDone == false)
        {
            await Task.Delay(1);
        }
        
        var scene = SceneManager.GetSceneByName(Constants.Scene.GAME);
        var mainMenu = scene.GetRoot<MainMenu>();
        var game = scene.GetRoot<Game>();
        mainMenu.Disable();
        game.BeginGame();
    }
    
    public void Shop()
    {
        SceneManager.LoadScene(Constants.Scene.SHOP);
    }
}
