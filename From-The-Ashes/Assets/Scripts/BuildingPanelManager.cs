using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPanelManager : MonoBehaviour
{
    public GameObject buildingPanelPrefab; // Префаб вашей динамической панели
    private GameObject currentPanel; // Текущая открытая панель

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверка на нажатие левой кнопки мыши
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("BuildingArea")) // Проверка тега области для постройки
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
        HideBuildingPanel(); // Закрыть текущую панель, если она открыта

        // Создать новую панель по указанным координатам
        currentPanel = Instantiate(buildingPanelPrefab, position, Quaternion.identity);
    }

    void HideBuildingPanel()
    {
        if (currentPanel != null)
        {
            Destroy(currentPanel); // Уничтожить текущую панель, если она открыта
        }
    }
}