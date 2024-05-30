using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingData buildingInformation;
    public BuildingData BuildingData { get => buildingInformation; }

    [Header("Building UI")]
    [SerializeField] private Image SelectionIndicator;
    [SerializeField] private Button buildingButton;
    [Space]

    [SerializeField] private UnityEvent somethingSelectedEvent;

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
            SelectBuilding();
        }
    }

    public void SelectBuilding()
    {
        somethingSelectedEvent.Invoke();
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
        Destroy(gameObject);
        buildingInformation.BuildingDemolished();

        foreach (ResourceContainer cost in buildingInformation.ConstructionCost)
        {
            Storage.Instance.AddResource(cost.Resource, cost.Quantity);
        }

        GetComponentInParent<ConstructionSlot>().building = null;
    }
}
