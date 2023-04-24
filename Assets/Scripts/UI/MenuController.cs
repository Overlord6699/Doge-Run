using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;
    
    public void OnStartPressed()
    {
        SceneLoader.SwitchToScene(_sceneToLoad);
    }

    public void OnExitPressed()
    {
        Application.Quit(0);
    }
}
