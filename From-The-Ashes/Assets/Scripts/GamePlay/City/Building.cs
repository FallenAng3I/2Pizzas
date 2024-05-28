using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingInformation buildingInformation;
    public BuildingInformation BuildingInformation { get => buildingInformation; }

    [Header("Building UI")]
    [SerializeField] private Image SelectionIndicator;
    [SerializeField] private Button buildingButton;
    [Space]
    [SerializeField] private VoidEvent somethingSelectedEvent;
    public static event Action<Building> OnBuildingSelected;

    private void Awake()
    {
        buildingButton.onClick.AddListener(buildingInformation.BuildingClicked);
        DeselectBuilding();

        buildingInformation.BuildingConstructed();
    }

    // ����� ������ � �������� ���� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            somethingSelectedEvent.RaiseEvent();
            SelectBuilding();
        }
    }

    public void SelectBuilding()
    {
        somethingSelectedEvent.RaiseEvent();
        OnBuildingSelected?.Invoke(this);
        SelectionIndicator.enabled = true;
    }

    public void DeselectBuilding()
    {
        SelectionIndicator.enabled = false;
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
        buildingInformation.BuildingDemolished();

        GetComponentInParent<ConstructionSlot>().building = null;
    }

    private void OnEnable()
    {
        somethingSelectedEvent.OnEventRaised += DeselectBuilding;
    }

    private void OnDisable()
    {
        somethingSelectedEvent.OnEventRaised -= DeselectBuilding;
    }
}
