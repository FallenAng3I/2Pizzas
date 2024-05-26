using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject SelectionIndicator;

    public static event Action<ConstructionSlot> OnConstructionSlotSelected;

    [HideInInspector] public Building building;

    private void Start()
    {
        // ��������� ������ ������������ �������� �����������, ���� ������� ������ �������� ��� ������, ��� ���� ��������� ������ �� �������� ��������
        OnConstructionSlotSelected += (constructionSlot) => IndicateSelection(false);
        Building.OnBuildingSelected += (building) => IndicateSelection(false);
        ConstructionMenu.OnBuildingConstructed += () => IndicateSelection(false);

        IndicateSelection(false);
    }

    // ��-���� ���� ����� ������� ��������� ���������� ������, ������� ��� ����� ������, ������� �� ������ �������� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SelectSlot();
        }
    }

    private void SelectSlot()
    {
        OnConstructionSlotSelected?.Invoke(this);
        IndicateSelection(true);
    }

    private void IndicateSelection(bool isSelected)
    {
        if (isSelected)
        {
            SelectionIndicator.SetActive(true);
        }
        else
        {
            SelectionIndicator.SetActive(false);
        }
    }
}
