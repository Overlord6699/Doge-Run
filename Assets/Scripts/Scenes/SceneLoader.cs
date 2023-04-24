using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public delegate void SceneLoad();
    public static event SceneLoad OnSceneLoaded, OnSceneClosed;
    
    const string SCENE_CLOSE = "Start", SCENE_OPEN = "End"; 

    [SerializeField]
    private TextMeshProUGUI _loadingPercentage;
    [SerializeField]
    private Image _loadingProgressBar;
    [SerializeField]
    private Canvas _canvasContainer;
    

    private static SceneLoader _instance;
    private static bool _shouldPlayOpeningAnimation = false;

    private Animator _componentAnimator;
    private AsyncOperation _loadingSceneOperation;
    

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
    }

    public static void SwitchToScene(in string sceneName)
    {
        OnSceneClosed?.Invoke();

        _instance._componentAnimator.SetTrigger(SCENE_CLOSE);

        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        // Чтобы сцена не начала переключаться пока играет анимация closing:
        _instance._loadingSceneOperation.allowSceneActivation = false;

        _instance._loadingProgressBar.fillAmount = 0;
    }

    private void Start()
    {
        _instance = this;

        OnSceneLoaded?.Invoke();

        _componentAnimator = GetComponent<Animator>();

        if (_shouldPlayOpeningAnimation)
        {

            _componentAnimator.SetTrigger(SCENE_OPEN);
            _instance._loadingProgressBar.fillAmount = 1;

            // Чтобы если следующий переход будет обычным SceneManager.LoadScene, не проигрывать анимацию opening:
            _shouldPlayOpeningAnimation = false;
        }
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            _loadingPercentage.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";

            // Просто присвоить прогресс:
            //LoadingProgressBar.fillAmount = loadingSceneOperation.progress; 

            // Присвоить прогресс с быстрой анимацией, чтобы ощущалось плавнее:
            _loadingProgressBar.fillAmount = Mathf.Lerp(_loadingProgressBar.fillAmount, _loadingSceneOperation.progress,
                Time.deltaTime * 5);

            //должно сработать единожды
            //if (loadingSceneOperation.progress == 1)
            //    loadingSceneOperation.allowSceneActivation = true;
        }
    }

    private void DisableCanvas()
    {
        _canvasContainer.enabled = false;
    }

    private void EnableCanvas()
    {
        _canvasContainer.enabled = true;
    }
    

    public void OnAnimationOver()
    {
        // Чтобы при открытии сцены, куда мы переключаемся, проигралась анимация opening:
        _shouldPlayOpeningAnimation = true;

        _loadingSceneOperation.allowSceneActivation = true;
    }
}