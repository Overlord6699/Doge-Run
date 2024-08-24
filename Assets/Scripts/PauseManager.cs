using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public delegate void SetGameState();

    public event SetGameState OnGamePaused, OnGameResumed;
    

    [SerializeField]
    private KeyCode _keyCode = KeyCode.Escape;
    
    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        OnGameResumed?.Invoke();
    }
    
}
