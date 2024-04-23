using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingFieldView : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public ContextMenu menu;

    public void OnDeselect(BaseEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            menu.CloseMenu();
        }
    }
     
    public void OnSelect(BaseEventData eventData)
    {
        if (!eventData.selectedObject.CompareTag("Build"))
        {
            menu.OpenMenu();
        }
    }
}