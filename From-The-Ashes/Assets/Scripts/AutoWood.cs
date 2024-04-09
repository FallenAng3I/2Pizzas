using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodProduction : MonoBehaviour
{
    public Text textWood;
    public int woodPerClick = 1; // ���������� ������ �� ������ ����
    public int woodPerSecond = 1; // ���������� ������ � �������
    public int upgradeCost = 10; // ��������� ���������

    private float nextUpdateTime;

    private void Start()
    {
        nextUpdateTime = Time.time + 1f; // ������������� ����� ���������� ���������� ������
    }

    private void Update()
    {
        if (Time.time > nextUpdateTime)
        {
            // ����������� ���������� ������ �� woodPerSecond ������ �������
            IncreaseWood(woodPerSecond);
            nextUpdateTime = Time.time + 1f; // ��������� ����� ���������� ���������� ������
        }
    }

    public void UpgradeWoodProduction()
    {
        // ���������, ������� �� ������ ��� ���������
        if (ResourceManager.instance.GetResourceAmount(ResourceType.Wood) >= upgradeCost)
        {
            // ��������� ���������� ������ �� ��������� ���������
            ResourceManager.instance.SpendResource(ResourceType.Wood, upgradeCost);

            // ����������� ���������� ������ �� ������ ����
            woodPerClick++;

            // ����������� ��������� ���������
            upgradeCost += 5;

            Debug.Log("��������� ���������!");

            // ��������� ��������� ���� � ����������� ������
            UpdateWoodText();
        }
        else
        {
            Debug.Log("������������ ������ ��� ���������!");
        }
    }

    public void IncreaseWood(int amount)
    {
        // ����������� ���������� ������
        ResourceManager.instance.AddResource(ResourceType.Wood, amount);

        // ��������� ��������� ���� � ����������� ������
        UpdateWoodText();
    }

    private void UpdateWoodText()
    {
        // ��������� ��������� ���� � ����������� ������
        textWood.text = "������: " + ResourceManager.instance.GetResourceAmount(ResourceType.Wood);
    }
}