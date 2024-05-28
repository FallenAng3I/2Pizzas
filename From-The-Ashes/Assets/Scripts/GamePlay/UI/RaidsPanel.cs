using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidsPanel : MonoBehaviour
{
    [SerializeField] private List<RaidData> raidDatas;
    private RaidData selectedRaidData;

    [Header("Raids Window")]
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject raidMenuObject;

    [Header("Raid Information Window")]
    [SerializeField] private GameObject raidInformationWindowObject;
    [SerializeField] private Image raidImage;
    [SerializeField] private TextMeshProUGUI raidDescriptionText;
    [SerializeField] private TextMeshProUGUI raidCostText;
    [SerializeField] private Button startRaidButton;

    [Header("Raid Module")]
    [SerializeField] private GameObject raidModulePrefab;
    [SerializeField] private Transform raidsScrollViewContentTransform;

    public static event Action<SpecialItem> OnSpecialItemObtained;

    private void Awake()
    {
        RemoveExcess();

        foreach (RaidData raidData in raidDatas)
        {
            GameObject raidModule = Instantiate(raidModulePrefab, raidsScrollViewContentTransform);
            raidModule.GetComponentInChildren<TextMeshProUGUI>().text = raidData.name;
            raidModule.GetComponentInChildren<Button>().onClick.AddListener(() => SelectRaid(raidData));
        }

        startRaidButton.onClick.AddListener(Raid);
        closeButton.onClick.AddListener(CloseMenu);
        CloseMenu();
    }

    private void RemoveExcess()
    {
        foreach(RaidData raidData in raidDatas.ToList())
        {
            if (raidData == null)
            {
                raidDatas.Remove(raidData);
            }
        }
    }

    public void OpenMenu()
    {
        raidMenuObject.SetActive(true);
    }

    public void CloseMenu()
    {
        raidMenuObject.SetActive(false);
        raidInformationWindowObject.SetActive(false);
    }

    private void SelectRaid(RaidData raidData)
    {
        selectedRaidData = raidData;
        raidImage.sprite = selectedRaidData.RaidSprite;
        raidDescriptionText.text = selectedRaidData.RaidDescription;
        ShowCost(selectedRaidData);
        raidInformationWindowObject.SetActive(true);
    }

    private void ShowCost(RaidData raidData)
    {
        string costString = "";

        foreach (ResourceContainer resourceContainer in raidData.Cost)
        {
            costString += $"{resourceContainer.Resource.name}: {resourceContainer.Quantity}\r\n";
        }

        raidCostText.text = costString;
    }

    private void Raid()
    {
        bool enoughResources = true;

        foreach (ResourceContainer resourceContainer in selectedRaidData.Cost)
        {
            enoughResources = enoughResources && Storage.Instance.GetResourceAmount(resourceContainer.Resource) >= resourceContainer.Quantity;
        }

        if (enoughResources)
        {
            foreach (ResourceContainer cost in selectedRaidData.Cost)
            {
                Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
            }

            foreach (ResourceContainer reward in selectedRaidData.Reward)
            {
                Storage.Instance.AddResource(reward.Resource, reward.Quantity);
            }
            OnSpecialItemObtained?.Invoke(selectedRaidData.SpecialReward);
        }

    }
}
