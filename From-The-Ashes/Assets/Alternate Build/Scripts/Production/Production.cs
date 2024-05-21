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
        Produce(building.buildingInformation.CurrentClickProductionQuantity);
    }

    protected IEnumerator PassiveProduction()
    {
        while (true)
        {
            yield return new WaitForSeconds(building.buildingInformation.PassiveProductionTime);
            if (building.buildingInformation.PassiveProductionUpgraded && passiveProductionEnabled) Produce(building.buildingInformation.CurrentPassiveProductionQuantity);
        }
    }

    protected abstract void Produce(int quantity);

    public void TogglePassiveProduction()
    {
        passiveProductionEnabled = !passiveProductionEnabled;
    }
}
