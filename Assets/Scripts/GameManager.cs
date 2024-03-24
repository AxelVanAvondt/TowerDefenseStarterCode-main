using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public ConstructionSite S;
    public void SelectSite(ConstructionSite site)
    {
        // remember the selected site
        S = site;
        // pass the selected site to the towerMenu
        // by calling SetSite
        towerMenu.SetSite(site);
    }
    public List<GameObject> Archer;
    public List<GameObject> Sword;
    public List<GameObject> Wizard;
    public void Build(TowerType type, SiteLevel level)
    {
        // you cannot build anything if there is no site selected
        // if so, return
        if (S == null)
        {
            return;
        }
        // use switch with the towertype to select the correct list
        List<GameObject> towerpref = new List<GameObject>();
        switch (type)
        {
            case TowerType.Archer:
                {
                    towerpref = Archer;
                    break;
                }
            case TowerType.Sword:
                {
                    towerpref = Sword;
                    break;
                }
            case TowerType.Wizard:
                {
                    towerpref = Archer;
                    break;
                }
        }

        // use switch with the level to create a GameObject tower
        GameObject tower = null;
        switch (level)
        {
            case SiteLevel.level1:
                {
                    tower = towerpref[0];
                    break;
                }
            case SiteLevel.level2:
                {
                    tower = towerpref[1];
                    break;
                }
            case SiteLevel.level3:
                {
                    tower = towerpref[2];
                    break;
                }
        }
        // configure the SelectedSite to set the tower
        if (S != null)
        {
            Instantiate(tower);
        }
        // pass null to the SetSite function in towerMenu to
        // hide the menu
        towerMenu.SetSite(null);
    }
    // Start is called before the first frame update 
    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
