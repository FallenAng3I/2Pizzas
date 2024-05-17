using System;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public BuildingInformation buildingInformation;
    public ProductionBuilding productionBuilding;

    [SerializeField] private Button buildingButton;
    public static Action<Building> BuildingButtonClicked;

    private void Start()
    {
        productionBuilding = GetComponent<ProductionBuilding>();

        buildingButton.onClick.AddListener(() => BuildingButtonClicked(this));
    }

    public void Demolish()
    {
        buildingInformation.DecreaseCost();
        Destroy(gameObject);
    }
}
