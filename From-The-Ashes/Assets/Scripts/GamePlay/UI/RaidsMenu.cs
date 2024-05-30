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

public class RaidsMenu : MonoBehaviour
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
    [SerializeField] private ResourcesCountTab raidCost;
    [SerializeField] private ResourcesCountTab raidReward;
    [SerializeField] private Button startRaidButton;

    [Header("Raid Module")]
    [SerializeField] private GameObject raidFieldPrefab;
    [SerializeField] private Transform raidsScrollViewContentTransform;
    private List<RaidModule> raidModules = new List<RaidModule>();

    public static event Action OnRaidsMenuOpened;
    public static event Action OnRaidsMenuClosed;
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

    private void OpenMenu()
    {
        menuWindowObject.SetActive(true);
        OnRaidsMenuOpened?.Invoke();
    }

    private void CloseMenu()
    {
        menuWindowObject.SetActive(false);
        raidInformationWindowObject.SetActive(false);
        OnRaidsMenuClosed?.Invoke();
    }

    private void LoadRaidInformation(RaidData raidData)
    {
        selectedRaidData = raidData;
        raidImage.sprite = selectedRaidData.RaidSprite;
        raidDescriptionText.text = selectedRaidData.RaidDescription;
        raidCost.FillInData(raidData.Cost);
        raidReward.FillInData(raidData.Reward);
        raidInformationWindowObject.SetActive(true);
    }

    private void ClearRaidInformation()
    {
        raidInformationWindowObject.SetActive(false);
        selectedRaidData = null;
        raidImage.sprite = default;
        raidDescriptionText.text = "";
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

    private void OnEnable()
    {
        MainMission.OnMissionCompleted += CloseMenu;
        MilitaryBaseMenu.OnRaidsButtonClicked += OpenMenu;
        MilitaryBaseMenu.OnMenuClosed += CloseMenu;
    }

    private void OnDisable()
    {
        MainMission.OnMissionCompleted -= CloseMenu;
        MilitaryBaseMenu.OnRaidsButtonClicked -= OpenMenu;
        MilitaryBaseMenu.OnMenuClosed -= CloseMenu;
    }
}
