using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class RaidData
{
    [SerializeField] private Button raidButton;
    [SerializeField] private TextMeshProUGUI raidNameText;
    [SerializeField] private string raidName;
    [SerializeField] private string raidDescription;
    [SerializeField] private Sprite raidSprite;

    [SerializeField] private List<ResourceContainer> cost;

    public Button RaidButton { get => raidButton; }
    public TextMeshProUGUI RaidNameText { get => raidNameText; }
    public string RaidName { get => raidName; }
    public string RaidDescription { get => raidDescription; }
    public Sprite RaidSprite { get => raidSprite; }
    public List<ResourceContainer> Cost { get => cost; }
}

public class RaidsWindow : MonoBehaviour
{
    [SerializeField] private List<RaidData> raidDatas;
    private RaidData selectedRaid;

    [Header("Menu UI")]
    [SerializeField] private Button militaryBaseButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject raidMenuObject;
    [SerializeField] private GameObject raidDataObject;

    [SerializeField] private Image raidImage;

    [SerializeField] private TextMeshProUGUI raidDescriptionText;
    [SerializeField] private TextMeshProUGUI raidCostText;

    [SerializeField] private Button startRaidButton;

    private void Awake()
    {
        militaryBaseButton.onClick.AddListener(OpenMenu);
        closeButton.onClick.AddListener(CloseMenu);

        foreach (RaidData raidData in raidDatas)
        {
            raidData.RaidNameText.text = raidData.RaidName;
            raidData.RaidButton.onClick.AddListener(() => SelectRaid(raidData));
        }

        startRaidButton.onClick.AddListener(Raid);

        CloseMenu();
    }

    private void OpenMenu()
    {
        raidMenuObject.SetActive(true);
    }

    private void CloseMenu()
    {
        raidMenuObject.SetActive(false);
        raidDataObject.SetActive(false);
    }

    private void SelectRaid(RaidData raidData)
    {
        selectedRaid = raidData;
        raidImage.sprite = selectedRaid.RaidSprite;
        raidDescriptionText.text = selectedRaid.RaidDescription;
        ShowCost(selectedRaid);
        raidDataObject.SetActive(true);
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

        foreach (ResourceContainer resourceContainer in selectedRaid.Cost)
        {
            enoughResources = enoughResources && Storage.Instance.GetResourceAmount(resourceContainer.Resource) >= resourceContainer.Quantity;
        }

        if (enoughResources)
        {
            foreach (ResourceContainer resourceContainer in selectedRaid.Cost)
            {
                Storage.Instance.SubtractResource(resourceContainer.Resource, resourceContainer.Quantity);
            }

            Debug.Log("Raid Succesfull");
        }

    }
}
