using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class TowerMenu : MonoBehaviour
{
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;
    private VisualElement root;
    private ConstructionSite selectedSite;
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        archerButton = root.Q<Button>("archer-button");
        swordButton = root.Q<Button>("sword-button");
        wizardButton = root.Q<Button>("wizard-button");
        updateButton = root.Q<Button>("button-upgrade");
        destroyButton = root.Q<Button>("button-destroy");
        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }
        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }
        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }
        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }
        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }
        root.visible = false;
    }
    private void OnArcherButtonClicked()
    {
        GameManager.instance.Build(TowerType.Archer, SiteLevel.level1);
    }
    private void OnSwordButtonClicked()
    {
        GameManager.instance.Build(TowerType.Sword, SiteLevel.level1);
    }
    private void OnWizardButtonClicked()
    {
        GameManager.instance.Build(TowerType.Wizard, SiteLevel.level1);
    }
    private void OnUpdateButtonClicked()
    {
        GameManager.instance.Build(selectedSite.TowerType, selectedSite.Level + 1);
    }
    private void OnDestroyButtonClicked()
    {
        GameManager.instance.Build(TowerType.Wizard, SiteLevel.Onbebouwd);
    }
    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }
        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }
        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }
        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }
        if (destroyButton != null)
        {
            destroyButton.clicked -= OnArcherButtonClicked;
        }
    }
    public void SetSite(ConstructionSite site)
    {
        // assign the site to a variable selectedSite
        selectedSite = site;
        // check if the selected site is equal to null
        // if so, hide the menu by changing root.visible
        // and return.
        if(selectedSite == null)
        {
            root.visible = false;
            return;
        }
        // if not, make sure the menu is visible
        // and call evaluate menu
        else
        {
            root.visible = true;
            EvaluateMenu();
        }
    }

    public void EvaluateMenu()
    {
        int credits = GameManager.instance.GetCredits();
        // return if selectedSite equals null
        if(selectedSite == null)
        {
            return;
        }
        // use the SetEnabled() function on every button
        // If the sitelevel for the selectedSite is zero, only the
        // archerButton, wizardButton and swordButton should
        // be enabled.
        archerButton.SetEnabled(true);
        swordButton.SetEnabled(true);
        wizardButton.SetEnabled(true);
        updateButton.SetEnabled(true);
        destroyButton.SetEnabled(true);
        // If the sitelevel is 1 or 2, only the
        // update and destroybutton should work
        // If the siteLevel is 3, only the destroyButton is on.

        switch (selectedSite.Level)
        {
            case 0:
                {
                    if (credits <= GameManager.instance.GetCost(TowerType.Archer, SiteLevel.level1))
                    {
                        archerButton.SetEnabled(false);
                    }
                    if (credits <= GameManager.instance.GetCost(TowerType.Sword, SiteLevel.level1))
                    {
                        swordButton.SetEnabled(false);
                    }
                    if (credits <= GameManager.instance.GetCost(TowerType.Wizard, SiteLevel.level1))
                    {
                        wizardButton.SetEnabled(false);
                    }
                    updateButton.SetEnabled(false);
                    destroyButton.SetEnabled(false);
                    break;
                }
            case SiteLevel.level1:
                {
                    archerButton.SetEnabled(false);
                    swordButton.SetEnabled(false);
                    wizardButton.SetEnabled(false);
                    if (credits <= GameManager.instance.GetCost(selectedSite.TowerType, SiteLevel.level2))
                    {
                        updateButton.SetEnabled(false);
                    }
                    break;
                }
            case SiteLevel.level2:
                {
                    archerButton.SetEnabled(false);
                    swordButton.SetEnabled(false);
                    wizardButton.SetEnabled(false);
                    if (credits <= GameManager.instance.GetCost(selectedSite.TowerType, SiteLevel.level3))
                    {
                        updateButton.SetEnabled(false);
                    }
                    break;
                }
            case SiteLevel.level3:
                {
                    archerButton.SetEnabled(false);
                    swordButton.SetEnabled(false);
                    wizardButton.SetEnabled(false);
                    updateButton.SetEnabled(false);
                    break;
                }
        }

        // Hint: use a switch for this logic.
    }
}