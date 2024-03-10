using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ����� ��� �������� ������
[System.Serializable]
public class Building
{
    public Button button; // ������ ������ ��� �����
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
            int tempResourceCounter = resourceCounter; // ������� ��������� ����������

            // ��� ������ ������ ������ ��������� ��������� ������� �����
            building.button.onClick.AddListener(() => ClickBuilding(resourceText, ref tempResourceCounter));

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
}
