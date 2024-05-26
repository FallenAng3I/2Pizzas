using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingInformation buildingInformation;
    public BuildingInformation BuildingInformation { get => buildingInformation; }

    [Header("Building UI")]
    [SerializeField] private GameObject SelectionIndicator;
    [SerializeField] private Button buildingButton;

    public static event Action<Building> OnBuildingSelected;

    private void Start()
    {
        buildingButton.onClick.AddListener(buildingInformation.BuildingClicked);

        IndicateSelection(false);
    }

    // ����� ������ � �������� ���� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SelectBuilding();
        }
    }

    private void SelectBuilding()
    {
        OnBuildingSelected?.Invoke(this);
        IndicateSelection(true);
    }

    private void DeselectBuilding<T>(T variable)
    {
        IndicateSelection(false);
    }

    // ����� �� ��������� ���� �� ������ �� ���, �� ������� ���� ������� ��������� ������ ����� ����
    // ����� ���������� ������� � ���������� ������, � ����� ������� ���� ������ �� ������������ ��������
    public void Demolish()
    {
        buildingInformation.DecreaseCurrentConstructionCost();

        foreach (Cost cost in buildingInformation.ConstructionCost)
        {
            Storage.Instance.AddResource(cost.Resource, cost.Quantity);
        }

        Destroy(gameObject);

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

    // ����� ���������� ������ ������ ��� ������������ ��������, ��������� ������ ����� ������ �����������
    private void OnEnable()
    {
        OnBuildingSelected += DeselectBuilding;
        ConstructionSlot.OnConstructionSlotSelected += DeselectBuilding;
        buildingInformation.BuildingConstructed();
    }

    private void OnDisable()
    {
        OnBuildingSelected -= DeselectBuilding;
        ConstructionSlot.OnConstructionSlotSelected -= DeselectBuilding;
        buildingInformation.BuildingDemolished();
    }
}
