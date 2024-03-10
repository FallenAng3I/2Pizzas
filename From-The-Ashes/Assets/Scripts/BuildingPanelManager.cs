using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPanelManager : MonoBehaviour
{
    public GameObject buildingPanelPrefab; // ������ ����� ������������ ������
    private GameObject currentPanel; // ������� �������� ������

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �������� �� ������� ����� ������ ����
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("BuildingArea")) // �������� ���� ������� ��� ���������
                {
                    ShowBuildingPanel(hit.collider.gameObject.transform.position);
                }
            }
            else
            {
                HideBuildingPanel();
            }
        }
    }

    void ShowBuildingPanel(Vector2 position)
    {
        HideBuildingPanel(); // ������� ������� ������, ���� ��� �������

        // ������� ����� ������ �� ��������� �����������
        currentPanel = Instantiate(buildingPanelPrefab, position, Quaternion.identity);
    }

    void HideBuildingPanel()
    {
        if (currentPanel != null)
        {
            Destroy(currentPanel); // ���������� ������� ������, ���� ��� �������
        }
    }
}