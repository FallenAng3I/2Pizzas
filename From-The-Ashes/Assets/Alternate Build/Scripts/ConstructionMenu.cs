using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

    [SerializeField] private Button sawmillButton;
    [SerializeField] private Button ironMineButton;
    [SerializeField] private Button steelFactoryButton;
    [SerializeField] private Button oilWellButton;
    [SerializeField] private Button fuelFactoryButton;
    [SerializeField] private Button leadMineButton;
    [SerializeField] private Button leadFactoryButton;
    [SerializeField] private Button militaryFactoryButton;

    [SerializeField] private ProductionBuilding sawmillPrefab;
    [SerializeField] private ProductionBuilding ironMinePrefab;
    [SerializeField] private ProductionBuilding steelFactoryPrefab;
    [SerializeField] private ProductionBuilding oilWellPrefab;
    [SerializeField] private ProductionBuilding fuelFactoryPrefab;
    [SerializeField] private ProductionBuilding leadMinePrefab;
    [SerializeField] private ProductionBuilding leadFactoryPrefab;
    [SerializeField] private ProductionBuilding militaryFactoryPrefab;

    [SerializeField] private int woodForSawmill;
    [SerializeField] private int woodForSawmillIncrease;
    [SerializeField] private int woodForIronMine;
    [SerializeField] private int woodForIronMineIncrease;
    [SerializeField] private int woodForSteelFactory;
    [SerializeField] private int woodForSteelFactoryIncrease;
    [SerializeField] private int woodForOilWell;
    [SerializeField] private int woodForOilWellIncrease;
    [SerializeField] private int woodForFuelFactory;
    [SerializeField] private int woodForFuelFactoryIncrease;
    [SerializeField] private int woodForLeadMine;
    [SerializeField] private int woodForLeadMineIncrease;
    [SerializeField] private int woodForLeadFactory;
    [SerializeField] private int woodForLeadFactoryIncrease;
    [SerializeField] private int woodForAmmunitionFactory;
    [SerializeField] private int woodForAmmunitionFactoryIncrease;

    private ConstructionSlot constructionSlot;

    private void Start()
    {
        CloseMenu();

        ConstructionSlot.ConstructionSlotSelected += OpenMenu;

        sawmillButton.onClick.AddListener(() => ConstructBuilding(sawmillPrefab, ref woodForSawmill, woodForSawmillIncrease, sawmillButton));
        ironMineButton.onClick.AddListener(() => ConstructBuilding(ironMinePrefab, ref woodForIronMine, woodForIronMineIncrease, ironMineButton));
        steelFactoryButton.onClick.AddListener(() => ConstructBuilding(steelFactoryPrefab, ref woodForSteelFactory, woodForSteelFactoryIncrease, steelFactoryButton));
        oilWellButton.onClick.AddListener(() => ConstructBuilding(oilWellPrefab, ref woodForOilWell, woodForOilWellIncrease, oilWellButton));
        fuelFactoryButton.onClick.AddListener(() => ConstructBuilding(fuelFactoryPrefab, ref woodForFuelFactory, woodForFuelFactoryIncrease, fuelFactoryButton));
        leadMineButton.onClick.AddListener(() => ConstructBuilding(leadMinePrefab, ref woodForLeadMine, woodForLeadMineIncrease, leadMineButton));
        leadFactoryButton.onClick.AddListener(() => ConstructBuilding(leadFactoryPrefab, ref woodForLeadFactory, woodForLeadFactoryIncrease, leadFactoryButton));
        militaryFactoryButton.onClick.AddListener(() => ConstructBuilding(militaryFactoryPrefab, ref woodForAmmunitionFactory, woodForAmmunitionFactoryIncrease, militaryFactoryButton));

        sawmillButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(0, 0, 0, 0);
        ironMineButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForIronMine, 0, 0, 0);
        steelFactoryButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForSteelFactory, 0, 0, 0);
        oilWellButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForOilWell, 0, 0, 0);
        fuelFactoryButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForFuelFactory, 0, 0, 0);
        leadMineButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForLeadMine, 0, 0, 0);
        leadFactoryButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForLeadFactory, 0, 0, 0);
        militaryFactoryButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForAmmunitionFactory, 0, 0, 0);
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
    
    private void ConstructBuilding(ProductionBuilding buildingPrefab, ref int woodForBuilding, int woodForBuildingIncrease, Button buildingButton)
    {
        if (constructionSlot.productionBuilding == null)
        {
            bool enoughResources = NewResources.WoodNeeded(woodForBuilding);

            if (enoughResources)
            {
                NewResources.WoodConsumed(woodForBuilding);

                ProductionBuilding building = Instantiate(buildingPrefab, constructionSlot.transform);

                CloseMenu();

                woodForBuilding += woodForBuildingIncrease;
                buildingButton.gameObject.GetComponent<PopUpWindow>().UpdateCostText(woodForBuilding, 0, 0, 0);
            }
        }
    }
}
