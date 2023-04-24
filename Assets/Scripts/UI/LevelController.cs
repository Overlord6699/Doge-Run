using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _lvlText;
    

    public void DisplayLevel(int level)
    {
        _lvlText.text = level.ToString();
    }
}
