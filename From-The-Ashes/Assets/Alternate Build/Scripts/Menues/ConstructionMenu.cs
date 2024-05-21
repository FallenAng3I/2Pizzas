using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

    [Header("Construction Buttons")]
    [SerializeField] private Button sawmillButton;
    [SerializeField] private Button ironMineButton;
    [SerializeField] private Button steelFactoryButton;
    [SerializeField] private Button oilWellButton;
    [SerializeField] private Button fuelFactoryButton;
    [SerializeField] private Button leadMineButton;
    [SerializeField] private Button leadFactoryButton;
    [SerializeField] private Button militaryFactoryButton;

    [Header("Building Prefabs")]
    [SerializeField] private Building sawmillPrefab;
    [SerializeField] private Building ironMinePrefab;
    [SerializeField] private Building steelFactoryPrefab;
    [SerializeField] private Building oilWellPrefab;
    [SerializeField] private Building fuelFactoryPrefab;
    [SerializeField] private Building leadMinePrefab;
    [SerializeField] private Building leadFactoryPrefab;
    [SerializeField] private Building militaryFactoryPrefab;

    private ConstructionSlot constructionSlot;

    private void Start()
    {
        ConstructionSlot.ConstructionSlotSelected += OpenMenu;

        sawmillButton.onClick.AddListener(() => ConstructBuilding(sawmillPrefab, sawmillButton));
        ironMineButton.onClick.AddListener(() => ConstructBuilding(ironMinePrefab, ironMineButton));
        steelFactoryButton.onClick.AddListener(() => ConstructBuilding(steelFactoryPrefab, steelFactoryButton));
        oilWellButton.onClick.AddListener(() => ConstructBuilding(oilWellPrefab, oilWellButton));
        fuelFactoryButton.onClick.AddListener(() => ConstructBuilding(fuelFactoryPrefab, fuelFactoryButton));
        leadMineButton.onClick.AddListener(() => ConstructBuilding(leadMinePrefab, leadMineButton));
        leadFactoryButton.onClick.AddListener(() => ConstructBuilding(leadFactoryPrefab, leadFactoryButton));
        militaryFactoryButton.onClick.AddListener(() => ConstructBuilding(militaryFactoryPrefab, militaryFactoryButton));

        UpdatePopUpWindow(sawmillButton, sawmillPrefab);
        UpdatePopUpWindow(ironMineButton, ironMinePrefab);
        UpdatePopUpWindow(steelFactoryButton, steelFactoryPrefab);
        UpdatePopUpWindow(oilWellButton, oilWellPrefab);
        UpdatePopUpWindow(fuelFactoryButton, fuelFactoryPrefab);
        UpdatePopUpWindow(leadMineButton, leadMinePrefab);
        UpdatePopUpWindow(leadFactoryButton, leadFactoryPrefab);
        UpdatePopUpWindow(militaryFactoryButton, militaryFactoryPrefab);

        CloseMenu();
    }

    public void OpenMenu(ConstructionSlot slot)
    {
        constructionSlot = slot;
        menuWindow.SetActive(true);
    }

    public void CloseMenu()
    {
        menuWindow.SetActive(false);
        constructionSlot = null;
    }
    
    // Проверяем, не занят ли слот строительства таким же зданием, проверяем, достаточно ли ресурсов, отнимаем ресурсы, строим здание, увеличиваем цену здания, закрываем меню 
    private void ConstructBuilding(Building buildingPrefab, Button constructionButton)
    {
        if (constructionSlot.building == null || constructionSlot.building.buildingInformation != buildingPrefab.buildingInformation)
        {
            bool enoughResources = NewResources.WoodNeeded(buildingPrefab.buildingInformation.CurrentConstructionCostInWood) 
                && NewResources.SteelNeeded(buildingPrefab.buildingInformation.CurrentConstructionCostInSteel)
                && NewResources.SteelNeeded(buildingPrefab.buildingInformation.CurrentConstructionCostInFuel)
                && NewResources.SteelNeeded(buildingPrefab.buildingInformation.CurrentConstructionCostInLead);

            if (enoughResources)
            {
                NewResources.WoodConsumed(buildingPrefab.buildingInformation.CurrentConstructionCostInWood);
                NewResources.SteelConsumed(buildingPrefab.buildingInformation.CurrentConstructionCostInSteel);
                NewResources.FuelConsumed(buildingPrefab.buildingInformation.CurrentConstructionCostInFuel);
                NewResources.LeadConsumed(buildingPrefab.buildingInformation.CurrentConstructionCostInLead);

                if (constructionSlot.building != null)
                {
                    constructionSlot.building.Demolish();
                }

                Building building = Instantiate(buildingPrefab, constructionSlot.transform);
                constructionSlot.building = building;

                buildingPrefab.buildingInformation.IncreaseCurrentConstructionCost();
                UpdatePopUpWindow(constructionButton, buildingPrefab);

                CloseMenu();
            }
        }
    }

    private void UpdatePopUpWindow(Button constructionButton, Building building)
    {
        int costInWood = building.buildingInformation.CurrentConstructionCostInWood;
        int costInSteel = building.buildingInformation.CurrentConstructionCostInSteel;
        int costInFuel = building.buildingInformation.CurrentConstructionCostInFuel;
        int costInLead = building.buildingInformation.CurrentConstructionCostInLead;

        constructionButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(costInWood, costInSteel, costInFuel, costInLead);
    }
}
