
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour
{
    private Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();
    }

    private void Update()
    {
        moneyText.text = "MONEY: " + GameMaster.Money;
    }
}
