using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject windowObject;
    [SerializeField] private TextMeshProUGUI buildingCostText;

    [HideInInspector] public BuildingData buildingInformation;

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
        string costText = "";


        foreach (Cost cost in buildingInformation.ConstructionCost)
        {
            costText += $"{cost.Resource.name}: {cost.Quantity}\r\n";
        }

        buildingCostText.text = costText;
    }
}