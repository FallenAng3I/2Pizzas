using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedAreaBuild : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public static SelectedAreaBuild SelectedArea { get; private set; }
    public static UnityEvent<SelectedAreaBuild> OnBuildingSelected { get; } = new UnityEvent<SelectedAreaBuild>();
    public static UnityEvent<SelectedAreaBuild> OnBuildingDeselected { get; } = new UnityEvent<SelectedAreaBuild>();

    
    public void OnSelect(BaseEventData eventData)
    {
        SelectedArea = this;
        OnBuildingSelected.Invoke(this);
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        SelectedArea = null;
        OnBuildingSelected.Invoke(this);
    }
}