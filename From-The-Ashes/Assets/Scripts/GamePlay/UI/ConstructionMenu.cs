using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ConstructionModule
{
    [SerializeField] private Button constructionButton;
    [SerializeField] private BuildingInformation buildingInformation;

    public Button ConstructionButton { get => constructionButton; }
    public BuildingInformation BuildingInformation { get => buildingInformation; }
}

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;

    [SerializeField] private List<ConstructionModule> constructionModules;

    private ConstructionSlot constructionSlot;

    public static event Action OnBuildingConstructed;

    private void Awake()
    {
        // ��� ������ ������������ �������� ���� �����������, ��� ������ ������ - �����������
        ConstructionSlot.OnConstructionSlotSelected += OpenMenu;
        BuildingMenu.OnBuildingMenuOpened += CloseMenu;

        foreach (ConstructionModule constructionModule in constructionModules)
        {
            constructionModule.ConstructionButton.onClick.AddListener(() => ConstructBuilding(constructionModule.BuildingInformation));
            constructionModule.ConstructionButton.GetComponent<PopUpWindow>().buildingInformation = constructionModule.BuildingInformation;
        }

        CloseMenu();
    }

    public void OpenMenu(ConstructionSlot slot)
    {
        constructionSlot = slot;
        menuWindowObject.SetActive(true);
    }

    public void CloseMenu()
    {
        menuWindowObject.SetActive(false);
        constructionSlot = null;
    }

    // ���������, �� ����� �� ���� ������������� �������, ���������, ���������� �� ��������, �������� �������, ������ ������, ����������� ���� ������, ��������� ���� 
    private void ConstructBuilding(BuildingInformation buildingInformation)
    {
        if (constructionSlot.building == null)
        {
            bool enoughResources = true;

            foreach (Cost cost in buildingInformation.ConstructionCost)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
            }

            if (enoughResources)
            {
                foreach (Cost cost in buildingInformation.ConstructionCost)
                {
                    Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                }
                buildingInformation.IncreaseCurrentConstructionCost();

                GameObject buildingObject = Instantiate(buildingInformation.BuildingPrefab, constructionSlot.transform);
                OnBuildingConstructed?.Invoke();

                CloseMenu();
            }
        }
    }
}
