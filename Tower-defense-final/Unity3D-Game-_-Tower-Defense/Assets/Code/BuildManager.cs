using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public Vector3 offset = new Vector3(0f, 0.5f, 0f);
    public BuildNode _selected;

    public GameObject basicTurretPrefab;
    public GameObject iceTurretPrefab;
    public GameObject shockTurretPrefab;

    private GameObject turretToBuild;
    private int value;

    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectStandardTurret()
    {
        turretToBuild = basicTurretPrefab;
        value = 500;
        BuildTurret();
    }

    public void SelectIceTurret()
    {
        turretToBuild = iceTurretPrefab;
        value = 750;
        BuildTurret();
    }

    public void SelectShockTurret()
    {
        turretToBuild = shockTurretPrefab;
        value = 1000;
        BuildTurret();
    }

	//This function is the build function, should recalculate the path once building a new
    public void BuildTurret()
    {
        MoneyManager.M.AddMoney(-value);
        turretToBuild = Instantiate(turretToBuild, _selected.transform.position + offset, _selected.transform.rotation);
        _selected.turret = turretToBuild;
        _selected.value = value; 
        MenuManager.instance.HideBuild();
        
		Vector3 newBuild = _selected.transform.position;
		//Debug.Log (newBuild);

		int newy = (int)newBuild.x / 5;
		int newx = (int)newBuild.z / (-5);

		Pathfinding.record [newx] [newy] = false;
		Pathfinding.renew ();
    }
    
    public void SellTurret()
    {
        Destroy(_selected.turret);
        turretToBuild = basicTurretPrefab;
        MoneyManager.M.AddMoney(_selected.value * 0.9f);
        MenuManager.instance.HideUpgradeSell();
        
		Vector3 newSell = _selected.transform.position;
		Debug.Log (newSell);

		int newy = (int)newSell.x / 5;
		int newx = (int)newSell.z / (-5);

		Pathfinding.record [newx] [newy] = true;
		Pathfinding.renew ();
    }
    
}
