using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionSlot : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject SelectionIndicator;
    [Space]
    [SerializeField] private VoidEvent SomethingSelectedEvent;
    public static Action<ConstructionSlot> OnConstructionSlotSelected;
    public static Action OnConstructionSlotDeselected;

    [HideInInspector] public Building building;

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

    private void SelectSlot()
    {
        SomethingSelectedEvent.RaiseEvent();
        OnConstructionSlotSelected?.Invoke(this);
        SelectionIndicator.SetActive(true);
    }

    private void DeselectSlot()
    {
        OnConstructionSlotDeselected?.Invoke();
        SelectionIndicator.SetActive(false);
    }

    private void OnEnable()
    {
        SomethingSelectedEvent.OnEventRaised += DeselectSlot;
        ConstructionMenu.OnBuildingConstructed += DeselectSlot;
        Pause_ESC.OnGamePaused += DeselectSlot;
    }

    private void OnDisable()
    {
        SomethingSelectedEvent.OnEventRaised -= DeselectSlot;
        ConstructionMenu.OnBuildingConstructed -= DeselectSlot;
        Pause_ESC.OnGamePaused -= DeselectSlot;
    }
}
