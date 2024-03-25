using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildAreaButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject ContextMenu; // Префаб контекстного меню

    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, что это левый клик мыши
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Вызываем контекстное меню
            ShowContextMenu();
        }
    }

    void ShowContextMenu()
    {
        // Создаем экземпляр контекстного меню из префаба
        GameObject contextMenu = Instantiate(ContextMenu, transform.position, Quaternion.identity);
        // Помещаем меню внутрь родительской кнопки
        contextMenu.transform.SetParent(transform, false);
    }
}