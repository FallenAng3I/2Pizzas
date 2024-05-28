using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MilitaryBaseConstructionSlot : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private List<Cost> constructionCost;
    [SerializeField] private GameObject militaryBaseObject;
    private bool militaryBaseConstructed;

    [Header("Menu UI")]
    [SerializeField] private GameObject constructionMenuObject;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button constructionButton;
    [SerializeField] private float menuClosingDelay = 0.1f;

    private void Awake()
    {
        militaryBaseObject.SetActive(false);
        constructionButton.onClick.AddListener(ConstructMilitaryBase);
        UpdateCostText();
        CloseMenu();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OpenMenu();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(CloseWithMenuWithDelay());
    }

    private void OpenMenu()
    {
        constructionMenuObject.SetActive(true);
    }

    private IEnumerator CloseWithMenuWithDelay()
    {
        yield return new WaitForSeconds(menuClosingDelay);
        CloseMenu();
    }

    private void CloseMenu()
    {
        constructionMenuObject.SetActive(false);
    }

    private void UpdateCostText()
    {
        string costString = "";

        foreach (Cost cost in constructionCost)
        {
            cost.ResetCost();
            costString += $"{cost.Resource.name}: {cost.Quantity}\r\n";
        }

        costText.text = costString;
    }

    private void ConstructMilitaryBase()
    {
        if (!militaryBaseConstructed)
        {
            bool enoughResources = true;

            foreach (Cost cost in constructionCost)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
            }

            if (enoughResources)
            {
                foreach (Cost cost in constructionCost)
                {
                    Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                }

                militaryBaseObject.SetActive(true);
                militaryBaseConstructed = true;

                CloseMenu();
            }
        }
    }
}
