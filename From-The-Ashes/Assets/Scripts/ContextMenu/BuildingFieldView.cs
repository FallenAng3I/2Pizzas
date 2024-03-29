using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingFieldView : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public static BuildingFieldView SelectedBuildingField { get; private set; }
    public static UnityEvent<BuildingFieldView> OnBuildingSelected { get; } = new UnityEvent<BuildingFieldView>();
    public static UnityEvent<BuildingFieldView> OnBuildingDeselected { get; } = new UnityEvent<BuildingFieldView>();

    
    public void OnSelect(BaseEventData eventData)
    {
        SelectedBuildingField = this;
        OnBuildingSelected.Invoke(this); 
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        SelectedBuildingField = null;
        OnBuildingSelected.Invoke(this);
    }
}