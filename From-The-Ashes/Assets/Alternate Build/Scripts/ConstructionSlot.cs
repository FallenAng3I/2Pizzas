using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionSlot : MonoBehaviour
{
    [SerializeField] private Button slotButton;
    [SerializeField] private GameObject SelectionIndicator;
    public static Action<ConstructionSlot> ConstructionSlotSelected;

    [HideInInspector] public Building building;

    private void Start()
    {
        DeSelectSlot();

        slotButton.onClick.AddListener(() => ConstructionSlotSelected(this));

        ConstructionSlotSelected += CheckIfSelected;
        Building.BuildingButtonClicked += (clickedBuilding) => CheckIfSelected(clickedBuilding.GetComponentInParent<ConstructionSlot>());
    }

    private void CheckIfSelected(ConstructionSlot slot)
    {
        if (slot == this)
        {
            SelectSlot();
            return;
        }

        DeSelectSlot();
    }

    private void SelectSlot()
    {
        SelectionIndicator.SetActive(true);
    }

    private void DeSelectSlot()
    {
        SelectionIndicator.SetActive(false);
    }
}
