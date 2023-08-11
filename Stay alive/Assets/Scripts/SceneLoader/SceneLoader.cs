using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public async Task Menu()
    {
        await LoadSceneWithFade(Constants.Scene.GAME);
    }
    
    public async Task Shop()
    {
        await LoadSceneWithFade(Constants.Scene.SHOP);
    }
    
    public async Task RestartGame()
    {
        var waitFading = true;
        Fader.Instance.FadeInScreen(() => waitFading = false);

        while (waitFading)
        {
            await Task.Delay(1);
        }
        
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
        
        waitFading = true;
        Fader.Instance.FadeOutScreen(() => waitFading = false);
    }
    
    private async Task LoadSceneWithFade(string scene)
    {
        var waitFading = true;
        Fader.Instance.FadeInScreen(() => waitFading = false);

        while (waitFading)
        {
            await Task.Delay(1);
        }
        
        var loadScene = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        
        while (loadScene.isDone == false)
        {
            await Task.Delay(1);
        }

        waitFading = true;
        Fader.Instance.FadeOutScreen(() => waitFading = false);
    }
}
