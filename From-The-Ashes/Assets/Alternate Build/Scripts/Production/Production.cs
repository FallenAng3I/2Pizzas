using System.Collections;
using UnityEngine;

public abstract class Production : MonoBehaviour
{
    protected Building building;

    [HideInInspector] public bool passiveProductionEnabled = true;

    protected void Start()
    {
        building = GetComponent<Building>();

        StartCoroutine(PassiveProduction());
    }

    public void ClickProduction()
    {
        Produce(building.BuildingInformation.CurrentClickProductionQuantity);
    }

    protected IEnumerator PassiveProduction()
    {
        while (true)
        {
            yield return new WaitForSeconds(building.BuildingInformation.PassiveProductionTime);
            if (building.BuildingInformation.PassiveProductionUpgraded && passiveProductionEnabled) Produce(building.BuildingInformation.CurrentPassiveProductionQuantity);
        }
    }

    protected abstract void Produce(int quantity);

    public void TogglePassiveProduction()
    {
        passiveProductionEnabled = !passiveProductionEnabled;
    }
}
