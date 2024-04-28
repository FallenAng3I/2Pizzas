using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CallMenu : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public ContextMenu menu;

    public void OnDeselect(BaseEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            menu.CloseMenu();
            AreasManager.Instance.currentSelection = null;
        }
    }
     
    public void OnSelect(BaseEventData eventData)
    {
        if (!eventData.selectedObject.CompareTag("Build"))
        {
            
            menu.OpenMenu();
            AreasManager.Instance.currentSelection = EventSystem.current.currentSelectedGameObject;
        }
    }
}