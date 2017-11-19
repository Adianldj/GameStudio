using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class BuildNode : MonoBehaviour {

    public Color hoverColor;
    private Color nodeColor;
    public Vector3 offset;
    private Renderer r;
    
    public GameObject turret;
    
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
        r.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        r.material.color = nodeColor;
    }
}
