using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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

    public GameObject TopMenu;
    private TopMenu topMenu;
    private int credits;
    private int health;
    private int currentWave;

    private bool waveActive = false;

    public void StartWave()
    {
        // increase the value of currentWave 
        // change the label for the current wave in topMenu 
        // change waveActive to true
        currentWave++;
        topMenu.SetWaveLabel("Wave: " + currentWave);
        waveActive = true;
    }
    public void EndWave()
    {
        // change waveActive to false
        waveActive = false;
    }
    public void StartGame()
    {
        credits = 200;
        health = 10;
        currentWave = 0;
        topMenu.SetCreditsLabel("Credits: " + credits);
        topMenu.SetGateHealthLabel("Gate Health: " + health);
        topMenu.SetWaveLabel("Wave: " + currentWave);
    }
    public void AttackGate()
    {
        // reduce health with 1
        // update the label in topmenu
        health--;
        topMenu.SetGateHealthLabel("Gate Health: " + health);
    }
    public void AddCredits(int amount)
    {
        // update credits 
        // update the label in topMenu
        credits += amount;
        topMenu.SetCreditsLabel("Credits: " + credits);

        // evaluate the tower menu. This does nothing for now, 
        // but we will soon add code over there to check for credits
        towerMenu.EvaluateMenu();
    }
    public void RemoveCredits(int amount)
    {
        // similar to the previous function
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
    public int GetCredits()
    {
        // you can figure this out
        return credits;
    }
    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        int cost = 0;
        // return the cost for every type of tower 
        // the return should be lower if you are selling
        switch(type)
        {
            case TowerType.Archer: cost = 100; break;
            case TowerType.Sword: cost = 150; break;
            case TowerType.Wizard: cost = 200; break;
        }
        switch(level)
        {
            case SiteLevel.level1: cost += 0; break;
            case SiteLevel.level2: cost += 100; break;
            case SiteLevel.level3: cost += 200; break;
        }
        if(selling == true)
        {
            cost -= 50;
        }
        return cost;
    }
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
        if (level == SiteLevel.Onbebouwd)
        {
            AddCredits(GetCost(type, level, true));
        }
        else
        {
            credits -= GetCost(type, level, false);
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
        Instantiate(tower);

        // pass null to the SetSite function in towerMenu to
        // hide the menu
        towerMenu.SetSite(null);
    }
    // Start is called before the first frame update 
    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
        StartGame();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
