using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class BuildNode : MonoBehaviour {

    public Color hoverColor;
    private Color nodeColor;
    public Vector3 offset;
    private Renderer r;
    
    public GameObject turret;
    public float value;
    
    private BuildManager buildManager;

    public GameObject buildMenu;
    public GameObject upgradeSellMenu;

    private GameObject Menu;
    private Vector3 menuPos; 

    void Start()
    {
        r = GetComponent<Renderer>();
        nodeColor = r.material.color;
        buildManager = BuildManager.instance;
        
    }

    void OnMouseDown()
    {
		Vector3 loc = gameObject.transform.position;
		int arrx = (int)loc.z / (-5);
		int arry = (int)loc.x / 5;
		bool[][] record2 = new bool[5][];

		for (int i = 0; i < 5; i++) {
			record2 [i] = new bool[5];
			for (int j = 0; j < 5; j++) {
				record2 [i] [j] = Pathfinding.record [i] [j];
			}
		}
		record2 [arrx] [arry] = false;
		if (Pathfinding.bfsPath (record2) == null) {
			return;
		}

        if (buildManager._selected == this && MenuManager.instance.BuildShowing) 
        {
            MenuManager.instance.HideBuild();
            return;
        }
        else if (buildManager._selected == this && MenuManager.instance.UpgradeSellShowing)
        {
            MenuManager.instance.HideUpgradeSell();
            return;
        }

        buildManager._selected = this;
        
        if (turret != null)
        {
            MenuManager.instance.ShowUpgradeSell();
            return;
        }

        else
        {
            MenuManager.instance.ShowBuild();
        }
        
    }
    
    void OnMouseEnter()
    {
		//Debug.Log ("hover     " + gameObject.transform.position);
        r.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        r.material.color = nodeColor;
    }
}
