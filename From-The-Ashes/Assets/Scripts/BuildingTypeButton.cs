using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingTypeButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject buildingPrefab; // Префаб здания для постройки

    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, что это левый клик мыши
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Заменяем кнопку свободной области на кнопку здания
            GameObject building = Instantiate(buildingPrefab, transform.position, Quaternion.identity);
            building.transform.SetParent(transform.parent, false);
            Destroy(transform.gameObject);
        }
    }
}