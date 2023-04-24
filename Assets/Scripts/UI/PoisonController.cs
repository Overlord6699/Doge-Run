using TMPro;
using UnityEngine;

public class PoisonController : MonoBehaviour
{
    public int Poison => _poisonCounter;
    
    [SerializeField]
    private TextMeshProUGUI _poisonCounterText;

    private int _poisonCounter;

    public void Init(int value)
    {
        _poisonCounter = value;
        DisplayPoison(_poisonCounter.ToString());
    }
    
    public void ReceivePoison(int value)
    {
        _poisonCounter += value;
        DisplayPoison(_poisonCounter.ToString());
    }

    private void DisplayPoison(string value)
    {
        _poisonCounterText.text = value;
    }
}
