using DefaultNamespace;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstructionSlot : MonoBehaviour//, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Button slotButton;
    [SerializeField] private GameObject SelectionIndicator;

    public static Action<ConstructionSlot> OnConstructionSlotSelected;

    [SerializeField] private VoidEvent ConstructionSlotSelectedEvent;
    [SerializeField] private VoidEvent ConstructionSlotDeselectedEvent;

    [HideInInspector] public Building building;

    private void Start()
    {
        slotButton.onClick.AddListener(SelectSlot);

        DeselectSlot();
    }

    /*
    public void OnDeselect(BaseEventData eventData)
    {
        DeselectSlot();
    }

    public void OnSelect(BaseEventData eventData)
    {
        SelectSlot();
    }
    */

    public void SelectSlot()
    {

        ConstructionSlotSelectedEvent.RaiseEvent();
        OnConstructionSlotSelected?.Invoke(this);
        SelectionIndicator.SetActive(true);
    }

    public void DeselectSlot()
    {
        ConstructionSlotDeselectedEvent.RaiseEvent();
        SelectionIndicator.SetActive(false);
    }
}
