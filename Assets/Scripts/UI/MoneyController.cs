using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{

    public int Money => _money;

    [SerializeField]
    private TextMeshProUGUI _coinsText;

    private int _money, _totalMoney;

    public void Init(int value)
    {
        _money = 0;
        _totalMoney = value;
        DisplayCoins(_totalMoney.ToString());
    }
    
    public void ReceiveMoney(int value)
    {
        _money += value;
        _totalMoney += value;
        DisplayCoins(_totalMoney.ToString());
    }
    
    private void DisplayCoins(string value)
    {
        _coinsText.text = value;
    }
}
