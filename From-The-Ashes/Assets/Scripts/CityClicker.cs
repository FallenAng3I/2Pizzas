using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ����� ��� �������� ���������
[System.Serializable]
public class Upgrade
{
    public Button button; // ������ ���������
    public float clickMultiplier; // ��������� ��� ��������� �����
}

// ����� ��� �������� ������
[System.Serializable]
public class Building
{
    public Button button; // ������ ������ ��� �����
    public Upgrade[] upgrades; // ������ ��������� ��� ������� ���� ������
}

// ������� �����, ����������� ����������������� ��������
public class CityClicker : MonoBehaviour
{
    private int wood; // ����� ������� ������� A
    private int iron; // ����� ������� ������� B
    private int oil; // ����� ������� ������� C

    public TextMeshProUGUI txtWOOD; // ��������� ���� ��� ����������� ���������� �������� A
    public TextMeshProUGUI txtIRON; // ��������� ���� ��� ����������� ���������� �������� B
    public TextMeshProUGUI txtOIL; // ��������� ���� ��� ����������� ���������� �������� C

    public Building[] sawmill; // ������ ��� ������ ������� A
    public Building[] mine; // ������ ��� ������ ������� B
    public Building[] oilrig; // ������ ��� ������ ������� C

    void Start()
    {
        // ��������� ���������� ������� ����� ��� ������� ���� ������
        AddListeners(sawmill, txtWOOD, ref wood);
        AddListeners(mine, txtIRON, ref iron);
        AddListeners(oilrig, txtOIL, ref oil);
    }

    // ����� ��� ���������� ���������� ������� ����� ��� ������
    void AddListeners(Building[] buildings, TextMeshProUGUI resourceText, ref int resourceCounter)
    {
        foreach (Building building in buildings)
        {
            int tempResourceCounter = resourceCounter;

            // ��� ������ ������ ������ ��������� ��������� ������� �����
            building.button.onClick.AddListener(() => ClickBuilding(resourceText, ref tempResourceCounter));
           
            // ��������� ��������� ������� ����� ��� ������� ��������� ������
            foreach (Upgrade upgrade in building.upgrades)
            {
                upgrade.button.onClick.AddListener(() => UpgradeClick(resourceText, ref tempResourceCounter, upgrade));
            }

            resourceCounter = tempResourceCounter; // ����������� ���������� �������� ������� resourceCounter
        }
    }

    // ����� ���������� ��� ����� �� ������
    void ClickBuilding(TextMeshProUGUI resourceText, ref int resourceCounter)
    {
        // ����������� ����� ������� ��������
        resourceCounter++;

        // ��������� ��������� ���� � ����������� ��������
        UpdateResourceText(resourceText, resourceCounter);
    }

    // ����� ��� ���������� ���������� ���� � ����������� ��������
    void UpdateResourceText(TextMeshProUGUI resourceText, int resourceCounter)
    {
        // ��������� ��������� ���� � ������ �������� ���������� ��������
        resourceText.text = " " + resourceCounter;
    }
    
    // ����� ���������� ��� ����� �� ��������� ������
    void UpgradeClick(TextMeshProUGUI resourceText, ref int resourceCounter, Upgrade upgrade)
    {
        // ����������� ��������� ����� �� ������ ���������
        resourceCounter += Mathf.FloorToInt(upgrade.clickMultiplier);

        // ��������� ��������� ���� � ����������� ��������
        UpdateResourceText(resourceText, resourceCounter);
    }
}

