using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidModule
{
    [SerializeField] private RaidData raidData;
    [SerializeField] private GameObject raidField;

    public RaidModule(RaidData raidData, GameObject raidField)
    {
        this.raidData = raidData;
        this.raidField = raidField;
    }

    public RaidData RaidData { get => raidData; }
    public GameObject RaidField { get => raidField; }
}

public class RaidsPanel : MonoBehaviour
{
    [SerializeField] private List<RaidData> raidDatas;
    private RaidData selectedRaidData;

    [Header("Raids Window")]
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject menuWindowObject;

    [Header("Raid Information Window")]
    [SerializeField] private GameObject raidInformationWindowObject;
    [SerializeField] private Image raidImage;
    [SerializeField] private TextMeshProUGUI raidDescriptionText;
    [SerializeField] private TextMeshProUGUI raidCostText;
    [SerializeField] private Button startRaidButton;

    [Header("Raid Module")]
    [SerializeField] private GameObject raidFieldPrefab;
    [SerializeField] private Transform raidsScrollViewContentTransform;
    private List<RaidModule> raidModules = new List<RaidModule>();

    public static event Action<SpecialItem> OnSpecialItemObtained;

    private void Awake()
    {
        RemoveExcess();

        foreach (RaidData raidData in raidDatas)
        {
            GameObject raidField = Instantiate(raidFieldPrefab, raidsScrollViewContentTransform);
            raidField.GetComponentInChildren<TextMeshProUGUI>().text = raidData.name;
            raidField.GetComponentInChildren<Button>().onClick.AddListener(() => LoadRaidInformation(raidData));
            RaidModule raidModule = new RaidModule(raidData, raidField);
            raidModules.Add(raidModule);
        }

        MainMission.OnMissionCompleted += CloseMenu;
        MilitaryBaseMenu.OnRaidsButtonClicked += OpenMenu;
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
        menuWindowObject.SetActive(true);
    }

    public void CloseMenu()
    {
        menuWindowObject.SetActive(false);
        raidInformationWindowObject.SetActive(false);
    }

    private void LoadRaidInformation(RaidData raidData)
    {
        selectedRaidData = raidData;
        raidImage.sprite = selectedRaidData.RaidSprite;
        raidDescriptionText.text = selectedRaidData.RaidDescription;
        ShowCost(selectedRaidData);
        raidInformationWindowObject.SetActive(true);
    }

    private void ClearRaidInformation()
    {
        raidInformationWindowObject.SetActive(false);
        selectedRaidData = null;
        raidImage.sprite = default;
        raidDescriptionText.text = "";
        raidCostText.text = "";
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

            RaidModule raidModule = raidModules.Find(RaidModule => RaidModule.RaidData == selectedRaidData);
            Destroy(raidModule.RaidField);
            raidModules.Remove(raidModule);

            ClearRaidInformation();
        }
    }
}
