using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class CallMenu : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public ConstructionMenu menu;

    public void OnDeselect(BaseEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // menu.CloseMenu();
            AreasManager.Instance.currentSelection = null;
        }
    }
     
    public void OnSelect(BaseEventData eventData)
    {
        if (!eventData.selectedObject.CompareTag("Build"))
        {
            // menu.OpenMenu();
            AreasManager.Instance.currentSelection = EventSystem.current.currentSelectedGameObject;
        }
    }
}