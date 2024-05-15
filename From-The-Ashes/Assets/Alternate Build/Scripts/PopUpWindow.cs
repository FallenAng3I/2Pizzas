using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject windowObject;
    [SerializeField] private TextMeshProUGUI buildingCostText;

    private void Start()
    {
        windowObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        windowObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        windowObject.SetActive(false);
    }

    private void OnDisable()
    {
        windowObject.SetActive(false);
    }

    public void UpdateCostText(int costInWood, int costInSteel, int costInFuel, int costInLead)
    {
        string costText = "";

        if (costInWood != 0)
        {
            costText += $"Wood: {costInWood}\r\n";
        }
        if (costInSteel != 0)
        {
            costText += $"Steel: {costInSteel}\r\n";
        }
        if (costInFuel != 0)
        {
            costText += $"Fuel: {costInFuel}\r\n";
        }
        if (costInLead != 0)
        {
            costText += $"Lead: {costInLead}\r\n";
        }

        buildingCostText.text = costText;
    }
}
