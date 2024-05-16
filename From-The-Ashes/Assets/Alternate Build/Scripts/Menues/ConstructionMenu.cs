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
        UpdatePopUpWindow(ironMineButton, leadMinePrefab);
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
    
    // Проверяем, не занят ли слот строительства, проверяем, достаточно ли ресурсов, отнимаем ресурсы, строим здание, увеличиваем цену здания, закрываем меню 
    private void ConstructBuilding(Building buildingPrefab, Button constructionButton)
    {
        if (constructionSlot.productionBuilding == null)
        {
            bool enoughResources = NewResources.WoodNeeded(buildingPrefab.buildingInformation.CurrentCostInWood);

            if (enoughResources)
            {
                NewResources.WoodConsumed(buildingPrefab.buildingInformation.CurrentCostInWood);

                Building building = Instantiate(buildingPrefab, constructionSlot.transform);

                buildingPrefab.buildingInformation.IncreaseCost();
                UpdatePopUpWindow(constructionButton, buildingPrefab);

                CloseMenu();
            }
        }
    }

    private void UpdatePopUpWindow(Button constructionButton, Building building)
    {
        int costInWood = building.buildingInformation.CurrentCostInWood;
        int costInSteel = building.buildingInformation.CurrentCostInSteel;
        int costInFuel = building.buildingInformation.CurrentCostInFuel;
        int costInLead = building.buildingInformation.CurrentCostInLead;

        constructionButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(costInWood, costInSteel, costInFuel, costInLead);
    }
}
