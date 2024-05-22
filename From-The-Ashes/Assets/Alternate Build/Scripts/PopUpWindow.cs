using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject windowObject;
    [SerializeField] private TextMeshProUGUI buildingCostText;

    [HideInInspector] public BuildingInformation buildingInformation;

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

        if (buildingInformation.CurrentConstructionCostInWood != 0)
        {
            costText += $"Wood: {buildingInformation.CurrentConstructionCostInWood}\r\n";
        }
        if (buildingInformation.CurrentConstructionCostInSteel != 0)
        {
            costText += $"Steel: {buildingInformation.CurrentConstructionCostInSteel}\r\n";
        }
        if (buildingInformation.CurrentConstructionCostInFuel != 0)
        {
            costText += $"Fuel: {buildingInformation.CurrentConstructionCostInFuel}\r\n";
        }
        if (buildingInformation.CurrentConstructionCostInLead != 0)
        {
            costText += $"Lead: {buildingInformation.CurrentConstructionCostInLead}\r\n";
        }

        buildingCostText.text = costText;
    }
}