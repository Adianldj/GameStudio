using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    protected GameObject Build;
    protected GameObject UpgradeSell;
    protected GameObject WinLose;
    public bool BuildShowing;
    public bool UpgradeSellShowing;
    public bool WinLoseShowing;
    private Transform _menuPos;
    private Transform _winLosePos;

    public GameObject _sTurret;
    public GameObject _iceTurret;
    public GameObject _shockTurret;

    public void Awake()
    {
        instance = this;
        _menuPos = GameObject.Find("MenuPos").transform;
        _winLosePos = GameObject.Find("WinLosePos").transform;
    }

    public void ShowBuild()
    {
        if (WinLoseShowing)
            return;

        if (BuildShowing)
        {
            HideBuild();
        }

        if (UpgradeSellShowing)
        {
            HideUpgradeSell();
        }
        
        Build = (GameObject)Instantiate(Resources.Load("Build"), _menuPos);

        Build.SetActive(true);
        BuildShowing = true;
        InitializeBuildButtons();
        
    }

    public void HideBuild()
    {
        Destroy(Build.gameObject);
        BuildShowing = false;
    }

    public void ShowUpgradeSell()
    {
        if (WinLoseShowing)
            return;

        if (BuildShowing)
        {
            HideBuild();
        }

        if (UpgradeSellShowing)
        {
            HideUpgradeSell();
        }
        
        UpgradeSell = (GameObject) Instantiate(Resources.Load("UpgradeSell"), _menuPos);

        UpgradeSell.SetActive(true);
        UpgradeSellShowing = true;
        InitializeUpgradeSellButtons();
    }

    public void HideUpgradeSell()
    {
        Destroy(UpgradeSell.gameObject);
        UpgradeSellShowing = false;
    }

    public void InitializeBuildButtons()
    {
        Button buildStandardTurret = GameObject.Find("Build S. Turret").GetComponent<Button>();
        buildStandardTurret.onClick.AddListener(BuildManager.instance.SelectStandardTurret);
        Button buildIceTurret = GameObject.Find("Build Ice Turret").GetComponent<Button>();
        buildIceTurret.onClick.AddListener(BuildManager.instance.SelectIceTurret);
        Button buildShockTurret = GameObject.Find("Build Shock Turret").GetComponent<Button>();
        buildShockTurret.onClick.AddListener(BuildManager.instance.SelectShockTurret);
        if (!MoneyManager.M._enough4BTurret)
        {
            buildStandardTurret.interactable = false;
        }

        if (!MoneyManager.M._enough4IceTurret)
        {
            buildIceTurret.interactable = false;
        }

        if (!MoneyManager.M._enough4ShockTurret)
        {
            buildShockTurret.interactable = false;
        }
    }

    public void InitializeUpgradeSellButtons()
    {
        Button sell = GameObject.Find("Sell Turret").GetComponent<Button>();
        sell.onClick.AddListener(BuildManager.instance.SellTurret);
    }

    public void PlayerWins()
    {
        WinLose = (GameObject) Instantiate(Resources.Load("WinLose"), _winLosePos);
        WinLose.GetComponentInChildren<Text>().text = "YOU WON! :)";
        WinLoseShowing = true;
    }

    public void PlayerLoses()
    {
        WinLose = (GameObject)Instantiate(Resources.Load("WinLose"), _winLosePos);
        WinLose.GetComponentInChildren<Text>().text = "YOU LOST! :(";
        WinLoseShowing = true;
    }
}

