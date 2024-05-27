using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingInformation buildingInformation;
    public BuildingInformation BuildingInformation { get => buildingInformation; }

    [SerializeField] private VoidEvent buildingSelectedEvent;

    [Header("Building UI")]
    [SerializeField] private GameObject SelectionIndicator;
    [SerializeField] private Button buildingButton;

    public static event Action<Building> OnBuildingSelected;

    private void Start()
    {
        buildingButton.onClick.AddListener(buildingInformation.BuildingClicked);

        IndicateSelection(false);

        buildingInformation.BuildingConstructed();
    }

    // Выбор здания и открытие меню здания
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SelectBuilding();
        }
    }

    public void SelectBuilding()
    {
        OnBuildingSelected?.Invoke(this);
        buildingSelectedEvent.RaiseEvent();
        IndicateSelection(true);
    }

    public void DeselectBuilding()
    {
        IndicateSelection(false);
    }

    // Здесь мы уменьшаем цену на здание до той, за которую было куплено последнее здание этого типа
    // Затем возвращаем ресурсы и уничтожаем здание, а также очищаем поле здания на строительной площадке
    public void Demolish()
    {
        buildingInformation.DecreaseCurrentConstructionCost();

        foreach (Cost cost in buildingInformation.ConstructionCost)
        {
            Storage.Instance.AddResource(cost.Resource, cost.Quantity);
        }

        Destroy(gameObject);
        buildingInformation.BuildingDemolished();

        GetComponentInParent<ConstructionSlot>().building = null;
    }

    private void IndicateSelection(bool isSelected)
    {
        if (isSelected)
        {
            SelectionIndicator?.SetActive(true);
        }
        else
        {
            SelectionIndicator?.SetActive(false);
        }
    }
}
