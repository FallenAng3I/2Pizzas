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

    private void Awake()
    {
        SomethingSelectedEvent.OnEventRaised += () => SelectionIndicator.SetActive(false);
        ConstructionMenu.OnBuildingConstructed += () => SelectionIndicator.SetActive(false);
        SelectionIndicator.SetActive(false);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnConstructionSlotDeselected?.Invoke();
        SelectionIndicator.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        SomethingSelectedEvent.RaiseEvent();
        OnConstructionSlotSelected?.Invoke(this);
        SelectionIndicator.SetActive(true);
    }
}
