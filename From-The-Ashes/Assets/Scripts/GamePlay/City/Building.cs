using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingData buildingData;
    public BuildingData BuildingData { get => buildingData; }

    [SerializeField] private ParticleSystem productionParticles;

    [Header("Building UI")]
    [SerializeField] private Image SelectionIndicator;
    [SerializeField] private Button buildingButton;
    [Space]

    [SerializeField] private UnityEvent somethingSelectedEvent;

    public static event Action<Building> OnBuildingSelected;

    private void Awake()
    {
        buildingButton.onClick.AddListener(buildingData.BuildingClicked);
        DeselectBuilding();

        buildingData.BuildingConstructed();
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

    private void PlayParticleSystem()
    {
        productionParticles.Play();
    }

    // ����� �� ��������� ���� �� ������ �� ���, �� ������� ���� ������� ��������� ������ ����� ����
    // ����� ���������� ������� � ���������� ������, � ����� ������� ���� ������ �� ������������ ��������
    public void Demolish()
    {
        Destroy(gameObject);
        buildingData.BuildingDemolished();

        foreach (ResourceContainer cost in buildingData.ConstructionCost)
        {
            Storage.Instance.AddResource(cost.Resource, cost.Quantity);
        }

        GetComponentInParent<ConstructionSlot>().ClearSlot();
    }

    private void OnEnable()
    {
        buildingData.OnBuildingProduced += PlayParticleSystem;
    }

    private void OnDisable()
    {
        buildingData.OnBuildingProduced -= PlayParticleSystem;
    }
}
