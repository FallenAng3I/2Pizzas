using System.Collections;
using UnityEngine;

public class Production : MonoBehaviour
{
    [SerializeField] private BuildingInformation buildingInformation;

    private int amountOfBuildings;

    private void Start()
    {
        // Подписываем скрипт на события, которые вызывает здание: клик по зданию, строительство нового здания и уничтожение здания данного типа
        buildingInformation.OnBuildingClicked += () => Produce(buildingInformation.CurrentClickProductionQuantity);
        buildingInformation.OnBuildingConstructed += () => amountOfBuildings++;
        buildingInformation.OnBuildingDemolished += () => amountOfBuildings--;

        StartCoroutine(ProductionCycle());
    }

    private IEnumerator ProductionCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(buildingInformation.PassiveProductionTime);
            if (buildingInformation.PassiveProductionUpgraded)
            {
                int quantity = buildingInformation.CurrentClickProductionQuantity * amountOfBuildings;
                Produce(quantity);
            }
        }
    }

    // Благодаря использованию цикла for, производство не будет остановлено даже если ресурсов не хватает на все здания
    // Внутри цикла: проверка, хватает ли сырья и если хватает, потребление этого сырья и производство новых ресурсов
    private void Produce(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            bool enoughResources = true;

            foreach (ResourceContainer container in buildingInformation.ProductionInput)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(container.Resource) >= container.Quantity;
            }

            if (enoughResources)
            {
                foreach (ResourceContainer container in buildingInformation.ProductionInput)
                {
                    Storage.Instance.SubtractResource(container.Resource, container.Quantity);
                }

                foreach (ResourceContainer container in buildingInformation.ProductionOutput)
                {
                    Storage.Instance.AddResource(container.Resource, container.Quantity);
                }
            }
        }
    }
}
