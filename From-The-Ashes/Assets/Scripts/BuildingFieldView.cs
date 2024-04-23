using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingFieldView : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public ContextMenu menu;
    //public static BuildingFieldView SelectedBuildingField { get; private set; }
    //public static UnityEvent<BuildingFieldView> OnBuildingSelected { get; } = new UnityEvent<BuildingFieldView>();
    //public static UnityEvent<BuildingFieldView> OnBuildingDeselected { get; } = new UnityEvent<BuildingFieldView>();

    public void OnDeselect(BaseEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            menu.CloseMenu();
        }
    }
     
    public void OnSelect(BaseEventData eventData)
    {
        menu.OpenMenu();
    }
}