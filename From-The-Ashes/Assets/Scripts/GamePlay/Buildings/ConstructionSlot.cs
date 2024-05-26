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
        // Индикатор выбора строительной площадки отключается, если выбрана другая площадка или здание, или если построено здание на выбраной площадке
        OnConstructionSlotSelected += (constructionSlot) => IndicateSelection(false);
        Building.OnBuildingSelected += (building) => IndicateSelection(false);
        ConstructionMenu.OnBuildingConstructed += () => IndicateSelection(false);

        IndicateSelection(false);
    }

    // По-сути этот метод урезано дублирует функционал кнопки, поэтому его можно убрать, повесив на объект компнент кнопки
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
