using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionSlot : MonoBehaviour
{
    [SerializeField] private Button slotButton;
    [SerializeField] private GameObject SelectionIndicator;

    public static event Action<ConstructionSlot> OnConstructionSlotSelected;
    public static event Action OnConstructionSlotSelectionIndicated;

    [HideInInspector] public Building building;

    private void Start()
    {
        IndicateSelection(false);

        slotButton.onClick.AddListener(SelectSlot);

        OnConstructionSlotSelectionIndicated += () => IndicateSelection(false);
    }

    private void SelectSlot()
    {
        OnConstructionSlotSelected?.Invoke(this);
        IndicateSelection(true);
    }

    public void IndicateSelection(bool isSelected)
    {
        if (isSelected)
        {
            OnConstructionSlotSelectionIndicated?.Invoke();
            SelectionIndicator.SetActive(true);
        }
        else
        {
            SelectionIndicator.SetActive(false);
        }
    }
}
