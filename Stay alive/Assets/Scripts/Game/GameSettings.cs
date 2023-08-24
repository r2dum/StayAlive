using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private SettingsPanel _settingsPanel;
    
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _shadowButton;
    [SerializeField] private Button _gameFpsButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _exitButton;
    
    [SerializeField] private Text _gameFpsText;
    [SerializeField] private int[] _gameFps;
    
    [SerializeField] private Light _light;
    [SerializeField] private Color _disabledColorButton;
    
    private GameData _gameData;
    private ISaveSystem _saveSystem;
    private int _currentGameFps;
    
    public void Initialize(GameData gameData, ISaveSystem saveSystem)
    {
        _gameData = gameData;
        _saveSystem = saveSystem;
        LoadSettings();
        
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _shadowButton.onClick.AddListener(() =>
            ChangeLightShadows(_light.shadows == LightShadows.None ? LightShadows.Hard : LightShadows.None));
        _gameFpsButton.onClick.AddListener(OnFpsButtonClicked);
        _soundButton.onClick.AddListener(() => ChangeSoundVolume(AudioListener.volume == 0f ? 1f : 0f));
        _exitButton.onClick.AddListener(_settingsPanel.Hide);
    }
    
    private void LoadSettings()
    {
        ChangeLightShadows(_gameData.LightShadow);
        
        while (_gameFps[_currentGameFps] != _gameData.GameFps)
            _currentGameFps++;
        
        ChangeGameFps(_gameData.GameFps);
        ChangeSoundVolume(_gameData.SoundVolume);
    }
    
    private void ChangeGameFps(int fps)
    {
        _gameData.GameFps = fps;
        Application.targetFrameRate = _gameData.GameFps;
        _gameFpsText.text = $"Fps: {_gameFps[_currentGameFps]}";
    }
    
    private void ChangeLightShadows(LightShadows lightShadow)
    {
        _light.shadows = lightShadow;
        _gameData.LightShadow = lightShadow;
        _shadowButton.image.color = _light.shadows == LightShadows.Hard ? _gameData.UIColor : _disabledColorButton;
        _saveSystem.Save(_gameData);
    }
    
    private void ChangeSoundVolume(float volume)
    {
        AudioListener.volume = volume;
        _gameData.SoundVolume = volume;
        _soundButton.image.color = AudioListener.volume == 0f ? _disabledColorButton : _gameData.UIColor;
        _saveSystem.Save(_gameData);
    }
    
    private void OnFpsButtonClicked()
    {
        if (_currentGameFps + 1 == _gameFps.Length)
            _currentGameFps = -1;

        _currentGameFps++;
        ChangeGameFps(_gameFps[_currentGameFps]);
        _saveSystem.Save(_gameData);
    }
    
    private void OnSettingsButtonClicked()
    {
        _settingsPanel.Show();
    }
}
