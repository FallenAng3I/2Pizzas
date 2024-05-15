using System;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string buildingName;
    public string buildingDescription;

    [SerializeField] private Button buildingButton;

    public ProductionBuilding productionBuilding;

    public static Action<Building> BuildingButtonClicked;

    private void Awake()
    {
        productionBuilding = GetComponent<ProductionBuilding>();

        buildingButton.onClick.AddListener(() => BuildingButtonClicked(this));
    }
}
