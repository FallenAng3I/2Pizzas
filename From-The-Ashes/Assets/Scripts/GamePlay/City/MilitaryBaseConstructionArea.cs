using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MilitaryBaseConstructionArea : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image selectionIndicator;
    [SerializeField] private Image slotImage;
    [SerializeField] private GameObject militaryBaseObject;
    [SerializeField] private List<ResourceContainer> constructionCost = new List<ResourceContainer>();
    private bool militaryBaseConstructed;

    [Header("Menu UI")]
    [SerializeField] private GameObject constructionMenuObject;
    [SerializeField] private ResourcesCountTab resourcesCountTab;
    [SerializeField] private Button constructionButton;
    [SerializeField] private float menuClosingDelay = 0.1f;
    [Space]
    [SerializeField] private GameEvent SomethingSelectedEvent;

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
        SomethingSelectedEvent.Raise();
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
        resourcesCountTab.FillInData(constructionCost);
    }

    private void ConstructMilitaryBase()
    {
        if (!militaryBaseConstructed)
        {
            bool enoughResources = true;

            foreach (ResourceContainer cost in constructionCost)
            {
                enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
            }

            if (enoughResources)
            {
                foreach (ResourceContainer cost in constructionCost)
                {
                    Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                }

                militaryBaseObject.SetActive(true);
                militaryBaseConstructed = true;

                selectionIndicator.enabled = false;
                slotImage.enabled = false;
                CloseMenu();
            }
        }
    }
}
