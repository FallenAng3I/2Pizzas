using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionSlot : MonoBehaviour
{
    [SerializeField] private BuildingMenu buildingMenu;

    [SerializeField] private Button button;

    public static Action<ConstructionSlot> ConstructionSlotSelected;

    public readonly ProductionBuilding productionBuilding;

    private void Start()
    {
        buildingMenu = FindAnyObjectByType<BuildingMenu>();

        button.onClick.AddListener(SelectSlot);
    }

    private void SelectSlot()
    {
        buildingMenu.CloseMenu();
        ConstructionSlotSelected?.Invoke(this);
    }
}
