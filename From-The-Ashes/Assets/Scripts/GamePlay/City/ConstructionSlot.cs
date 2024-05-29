using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionSlot : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject SelectionIndicator;
    [Space]
    [SerializeField] private GameEvent SomethingSelectedEvent;
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

    public void SelectSlot()
    {
        SomethingSelectedEvent.Raise();
        OnConstructionSlotSelected?.Invoke(this);
        SelectionIndicator.SetActive(true);
    }

    public void DeselectSlot()
    {
        OnConstructionSlotDeselected?.Invoke();
        SelectionIndicator.SetActive(false);
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
