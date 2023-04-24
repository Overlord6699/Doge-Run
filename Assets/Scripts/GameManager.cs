using System;
using Save;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField][Header("Scene")]
    private string _homeScene = "Menu";

    public static GameManager Instance => _instance;
    private static GameManager _instance;
    
    public Player Player
    {
        get => _player;
    }

    //private ISaveFileSystem _saveFileSystem;

    [SerializeField]
    private Score _scoreController;

    [SerializeField]
    private DeatgPanelController _deatgPanelController;

    [SerializeField] private LevelController _levelController;

    [SerializeField] private PlayerSpawner _spawner;

    [SerializeField] private TileManager _tileManager;
    [SerializeField] private MoneyController _moneyController;
    [SerializeField] private PoisonController _poisonController;
    
    private CameraMotor _cameraMotor;
    private Player _player;

    private int _money, _totalMoney;

    private bool _homeButtonPressed = false;
    
    private void Start()
    {
        _instance = this;
        _homeButtonPressed = false;
        
        /*_saveFileSystem = new JsonFileFileSystem();
        var loader = (ILoadFileSystem) _saveFileSystem;
        var config = loader.Load();
        
        if (config != null)
        {
            _score.Init(config.Highscore);
        }*/
        SaveManager.Instance.OnLoad += GetSaveData;
        SaveManager.Instance.OnSave += OnSceneSaved;
        SaveManager.Instance.Load();

        
        _cameraMotor = Camera.main.GetComponent<CameraMotor>();
        _scoreController.OnLevelUp += OnLevelUp;

        
        StartGame();
    }

    public void ReceivePoison(int value)
    {
        _poisonController.ReceivePoison(value);
    }
    
    public void ReceiveMoney(int value)
    {
        _moneyController.ReceiveMoney(value);
    }
    
    private void GetSaveData(SaveData data)
    {
        //_score.Init(data.Highscore);
        if (data != null)
        {
            if (data.Highscore > 0)
                _scoreController.Init(data.Highscore);
            else
            {
                _scoreController.Init(10);
            }

            _moneyController.Init(data.Money);

        }
        else
        {
            _scoreController.Init(10);
            _moneyController.Init(0);
        }

    }
    
    private void StartGame()
    {
        _poisonController.Init(0);
        _levelController.DisplayLevel(1);
        _player = _spawner.Spawn();
        _player.OnPlayerDied += OnPlayerDied;

        _scoreController.StartGame();
        _scoreController.enabled = true;
        
        _cameraMotor.Init(_player.transform);
        _tileManager.Init(_player.transform);
    }
    
    private void OnLevelUp(int level)
    {
        _player.OnLevelUp(level);
        _levelController.DisplayLevel(level);
    }
    
    private void OnPlayerDied()
    {
        _deatgPanelController.Show();
        _scoreController.OnPlayerDied();
        _scoreController.enabled = false;
    }

    public void OnRetryPressed()
    {
        _player.OnPlayerDied -= OnPlayerDied;
        Destroy(_player.gameObject);
        
        
        _deatgPanelController.Hide();
        _tileManager.Clear();
        
        StartGame();
    }

    private void OnSceneSaved()
    {
        if(_homeButtonPressed)        
            SceneLoader.SwitchToScene(_homeScene);
    }
    
    public void OnHomePressed()
    {
        _homeButtonPressed = true;
        SaveManager.Instance.CreateSaveData(_scoreController.Highscore, _moneyController.Money);
        SaveManager.Instance.Save();
        
    }
}
