using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstructionSlot : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image SelectionIndicator;
    [SerializeField] private Image slotImage;
    [Space]
    [SerializeField] private GameEvent SomethingSelectedEvent;

    public static Action<ConstructionSlot> OnConstructionSlotSelected;
    public static Action OnConstructionSlotDeselected;

    private Building building;
    public Building Building { get => building; }

    private void Start()
    {
        DeselectSlot();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DeselectSlot();
    }

    public void OnSelect(BaseEventData eventData)
    {
        SelectSlot();
    }

    public void SelectSlot()
    {
        SomethingSelectedEvent.Raise();
        OnConstructionSlotSelected?.Invoke(this);
        SelectionIndicator.enabled = true;
    }

    public void DeselectSlot()
    {
        OnConstructionSlotDeselected?.Invoke();
        SelectionIndicator.enabled = false;
    }

    public void OccupySlot(Building building)
    {
        this.building = building;
        slotImage.enabled = false;
    }

    public void ClearSlot()
    {
        building = null;
        slotImage.enabled = true;
    }

    private void OnEnable()
    {
        ConstructionMenu.OnBuildingConstructed += DeselectSlot;
    }

    private void OnDisable()
    {
        ConstructionMenu.OnBuildingConstructed -= DeselectSlot;
    }
}
