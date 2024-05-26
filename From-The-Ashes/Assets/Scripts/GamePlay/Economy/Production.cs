using System.Collections;
using UnityEngine;

public class Production : MonoBehaviour
{
    [SerializeField] private BuildingInformation buildingInformation;

    private int amountOfBuildings;

    private void Start()
    {
        // ����������� ������ �� �������, ������� �������� ������: ���� �� ������, ������������� ������ ������ � ����������� ������ ������� ����
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

    // ��������� ������������� ����� for, ������������ �� ����� ����������� ���� ���� �������� �� ������� �� ��� ������
    // ������ �����: ��������, ������� �� ����� � ���� �������, ����������� ����� ����� � ������������ ����� ��������
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
