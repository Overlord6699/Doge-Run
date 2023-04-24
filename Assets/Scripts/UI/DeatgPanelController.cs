using UnityEngine;
using UnityEngine.UI;

public class DeatgPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject _deathPanel;
    
    public void Show()
    {
        _deathPanel.SetActive(true);
    }

    public void Hide()
    {
        _deathPanel.SetActive(false);
    }
}
