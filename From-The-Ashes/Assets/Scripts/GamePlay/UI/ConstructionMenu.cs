using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ConstructionModule
{
    [SerializeField] private Button constructionButton;
    [SerializeField] private BuildingData buildingInformation;

    public Button ConstructionButton { get => constructionButton; }
    public BuildingData BuildingInformation { get => buildingInformation; }
}

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;
    [SerializeField] private float menuClosingDelay = 0.1f;

    [SerializeField] private List<ConstructionModule> constructionModules;

    private ConstructionSlot constructionSlot;

    public static event Action OnBuildingConstructed;

    private void Awake()
    {
        foreach (ConstructionModule constructionModule in constructionModules)
        {
            constructionModule.ConstructionButton.onClick.AddListener(() => ConstructBuilding(constructionModule.BuildingInformation));
            constructionModule.ConstructionButton.GetComponent<PopUpWindow>().buildingData = constructionModule.BuildingInformation;
        }

        CloseMenu();
    }

    private void OpenMenu(ConstructionSlot newConstructionSlot)
    {
        StopAllCoroutines();
        constructionSlot = newConstructionSlot;
        menuWindowObject.SetActive(true);
    }

    private void CloseMenu()
    {
        constructionSlot = null;
        menuWindowObject.SetActive(false);
    }

    private void CloseMenuWithDelay()
    {
        StartCoroutine(ClosingDelay());
    }

    private IEnumerator ClosingDelay()
    {
        yield return new WaitForSeconds(menuClosingDelay);
        CloseMenu();
    }

    // Проверяем, не занят ли слот строительства зданием, проверяем, достаточно ли ресурсов, отнимаем ресурсы, строим здание, увеличиваем цену здания, закрываем меню 
    private void ConstructBuilding(BuildingData buildingInformation)
    {
        if (constructionSlot.Building == null)
        {
            bool enoughResources = true;

            foreach (ResourceContainer cost in buildingInformation.ConstructionCost)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
            }

            if (enoughResources)
            {
                foreach (ResourceContainer  cost in buildingInformation.ConstructionCost)
                {
                    Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                }

                GameObject buildingObject = Instantiate(buildingInformation.BuildingPrefab, constructionSlot.transform);
                OnBuildingConstructed?.Invoke();
                constructionSlot.OccupySlot(buildingObject.GetComponent<Building>());

                CloseMenu();
            }
        }
    }

    private void OnEnable()
    {
        ConstructionSlot.OnConstructionSlotSelected += OpenMenu;
        ConstructionSlot.OnConstructionSlotDeselected += CloseMenuWithDelay;
    }

    private void OnDisable()
    {
        ConstructionSlot.OnConstructionSlotSelected -= OpenMenu;
        ConstructionSlot.OnConstructionSlotDeselected -= CloseMenuWithDelay;
    }
}
