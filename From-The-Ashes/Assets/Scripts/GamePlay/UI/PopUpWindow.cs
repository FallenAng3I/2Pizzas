using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject windowObject;
    [SerializeField] private ResourcesCountTab resourcesCountTab;

    [HideInInspector] public BuildingData buildingData;

    private void Start()
    {
        windowObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        windowObject.SetActive(true);
        UpdateCostText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        windowObject.SetActive(false);
    }

    private void OnDisable()
    {
        windowObject.SetActive(false);
    }

    private void UpdateCostText()
    {
        resourcesCountTab.FillInData(buildingData.ConstructionCost);
    }
}