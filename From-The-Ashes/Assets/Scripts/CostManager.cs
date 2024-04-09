using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // ��� ����������� ����, ������� �������� ������ ������� ���������� � ResourceManager ����� ��� ���������
    public static ResourceManager instance;

    // ���� �������
    public int wood = 0;

    private void Awake()
    {
        // ����������, ��� instance �� ������ � ������������� ���, ���� ��� ���
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("��� ���������� ��������� ResourceManager");
        }
    }

    // ������ ��� ��������� � ����� ��������
    public int GetResourceAmount(ResourceType resourceType)
    {
        // ���������� ������ ��������� ���������� ��������
        // � ���� ������� �� ������ ������ ���������� ������
        return wood;
    }

    public void SpendResource(ResourceType resourceType, int amount)
    {
        // ���������� ������ ����� ��������
        // � ���� ������� �� ������ ������ ���������� ������
        wood -= amount;
    }
}

// �����������, ��� � ��� ���� enum ��� ������ ����� ��������
public enum ResourceType
{
    Wood,
    // ������ ���� ��������, ���� ��� ����
}

public class BuildingManager : MonoBehaviour
{
    public int initialCost = 2; // ��������� ��������� ������
    public int costIncrement = 8; // ���������� ��������� ����� ������ �������

    private int currentCost; // ������� ��������� ������

    private void Start()
    {
        currentCost = initialCost; // ������������� ��������� ���������
    }

    public bool CanAfford()
    {
        // ���������, ������� �� �������� ��� ������� ������
        return ResourceManager.instance.GetResourceAmount(ResourceType.Wood) >= currentCost;
    }

    public void BuyBuilding()
    {
        if (CanAfford())
        {
            // �������� ��������� �� ��������
            ResourceManager.instance.SpendResource(ResourceType.Wood, currentCost);

            // ����������� ��������� ��� ��������� �������
            currentCost += costIncrement;
        }
        else
        {
            Debug.Log("������������ �������� ��� ������� ������!");
        }
    }
}
