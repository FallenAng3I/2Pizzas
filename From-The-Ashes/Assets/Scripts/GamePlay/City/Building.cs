using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingData buildingInformation;
    public BuildingData BuildingInformation { get => buildingInformation; }

    [Header("Building UI")]
    [SerializeField] private Image SelectionIndicator;
    [SerializeField] private Button buildingButton;
    [Space]
    [SerializeField] private GameEvent somethingSelectedEvent;
    public static event Action<Building> OnBuildingSelected;

    private void Awake()
    {
        buildingButton.onClick.AddListener(buildingInformation.BuildingClicked);
        DeselectBuilding();

        buildingInformation.BuildingConstructed();
    }

    // Выбор здания и открытие меню здания
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            somethingSelectedEvent.Raise();
            SelectBuilding();
        }
    }

    public void SelectBuilding()
    {
        somethingSelectedEvent.Raise();
        OnBuildingSelected?.Invoke(this);
        SelectionIndicator.enabled = true;
    }

    public void DeselectBuilding()
    {
        SelectionIndicator.enabled = false;
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

    private void OnEnable()
    {
        BuildingMenu.OnBuildingMenuClosed += DeselectBuilding;
    }

    private void OnDisable()
    {
        BuildingMenu.OnBuildingMenuClosed -= DeselectBuilding;
    }
}
