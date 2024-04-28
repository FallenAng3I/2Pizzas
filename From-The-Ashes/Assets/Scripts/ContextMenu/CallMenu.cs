using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CallMenu : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public ContextMenu menu;

    public GameObject Sawmill;
    public GameObject Mine;
    public GameObject OilWell;
    public GameObject OilFactory;
    public GameObject SteelFactory;
    public GameObject LeadMine;
    public GameObject LeadFactory;
    public GameObject MilitaryFactory;

    public void OnDeselect(BaseEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            menu.CloseMenu();
        }
    }
     
    public void OnSelect(BaseEventData eventData)
    {
        if (eventData.selectedObject.CompareTag("Area"))
        {
            menu.OpenMenu();
        }
    }

    public void ToBuildSawmill()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        Destroy(selectedObject);
    }
}