using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MilitaryBaseConstructionArea : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image selectionIndicator;
    [SerializeField] private GameObject militaryBaseObject;
    [SerializeField] private List<Cost> constructionCost;
    private bool militaryBaseConstructed;

    [Header("Menu UI")]
    [SerializeField] private GameObject constructionMenuObject;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button constructionButton;
    [SerializeField] private float menuClosingDelay = 0.1f;
    [Space]
    [SerializeField] private VoidEvent SomethingSelectedEvent;

    private void Awake()
    {
        militaryBaseObject.SetActive(false);
        selectionIndicator.enabled = false;
        constructionButton.onClick.AddListener(ConstructMilitaryBase);
        UpdateCostText();
        CloseMenu();
    }

    public void OnSelect(BaseEventData eventData)
    {
        selectionIndicator.enabled = true;
        SomethingSelectedEvent.RaiseEvent();
        OpenMenu();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selectionIndicator.enabled = false;
        StartCoroutine(CloseMenuWithDelay());
    }

    private void OpenMenu()
    {
        constructionMenuObject.SetActive(true);
    }

    private void CloseMenu()
    {
        constructionMenuObject.SetActive(false);
    }

    private IEnumerator CloseMenuWithDelay()
    {
        yield return new WaitForSeconds(menuClosingDelay);
        CloseMenu();
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

                selectionIndicator.enabled = false;
                CloseMenu();
            }
        }
    }
}
