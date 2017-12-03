using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager M;
    private float startingAmount;
    private float currentAmount;
    public bool _enough4BTurret;
    public bool _enough4IceTurret;
    public bool _enough4ShockTurret;

    private static Text moneyText;

    void Start()
    {
        M = this;
        startingAmount = 2000f;
        currentAmount = startingAmount;
        moneyText = GetComponent<Text>();
        _enough4BTurret = true;
        _enough4IceTurret = true;
        _enough4ShockTurret = true;
        UpdateMoney();
    }

    void UpdateMoney()
    {
        if (currentAmount < 1000)
            _enough4ShockTurret = false;

        if (currentAmount < 750)
            _enough4IceTurret = false;

        if (currentAmount < 500)
            _enough4BTurret = false;

        else
        {
            _enough4BTurret = true;
            _enough4ShockTurret = true;
            _enough4IceTurret = true;
        }

        moneyText.text = "Money: " + "$" + currentAmount.ToString();
    }

    public void AddMoney(float value)
    {
        currentAmount = Mathf.Max(0, currentAmount + value);
        UpdateMoney();
    }
    

}
