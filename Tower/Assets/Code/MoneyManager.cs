using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager M;
    private float startingAmount;
    private float currentAmount;
    public bool _enoughMoney;

    private static Text moneyText;

    void Start()
    {
        M = this;
        startingAmount = 1000f;
        currentAmount = startingAmount;
        moneyText = GetComponent<Text>();
        _enoughMoney = true;
        UpdateMoney();
    }

    void UpdateMoney()
    {
        if (currentAmount < 500)
            _enoughMoney = false;
        else
            _enoughMoney = true;

        moneyText.text = "Money: " + "$" + currentAmount.ToString();
    }

    public void AddMoney(float value)
    {
        currentAmount = Mathf.Max(0, currentAmount + value);
        UpdateMoney();
    }
    

}
