using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public Vector3 offset = new Vector3(0f, 0.5f, 0f);
    public BuildNode _selected;

    public GameObject basicTurretPrefab;

    private GameObject turretToBuild;

    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        turretToBuild = basicTurretPrefab;
    }
    
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void BuildStandardTurret()
    {
        MoneyManager.M.AddMoney(-500);
        turretToBuild = Instantiate(turretToBuild, _selected.transform.position + offset, _selected.transform.rotation);
        _selected.turret = turretToBuild;
        MenuManager.instance.HideBuild();
    }

    public void SellTurret()
    {
        Destroy(_selected.turret);
        turretToBuild = basicTurretPrefab;
        MoneyManager.M.AddMoney(450);
        MenuManager.instance.HideUpgradeSell();
    }
    
}
