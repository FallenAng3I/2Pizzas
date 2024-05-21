using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    private Building building = null;

    [SerializeField] private UpgradeMenu upgradeMenu;
    [SerializeField] private ConstructionMenu constructionMenu;

    [SerializeField] private Button closeButton;

    [Header("Building Information")]
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingDescriptionText;

    [Header("Building Control Buttons")]
    [SerializeField] private Button produceButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button replaceButton;

    private void Start()
    {
        Building.BuildingButtonClicked += OpenMenu;

        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // «акрываем меню, заполн€ем пол€ информации о здании, назначаем это здание дл€ кнопок управлени€ и открываем меню
    private void OpenMenu(Building newBuilding)
    {
        CloseMenu();

        building = newBuilding;

        buildingNameText.text = building.buildingInformation.BuildingName;
        buildingDescriptionText.text = building.buildingInformation.BuildingDescription;
        UpdateStopButton();

        produceButton.onClick.AddListener(building.production.InvokeAction);
        upgradeButton.onClick.AddListener(() => upgradeMenu.OpenMenu(building));
        stopButton.onClick.AddListener(TogglePassiveProduction);
        replaceButton.onClick.AddListener(() => constructionMenu.OpenMenu(building.GetComponentInParent<ConstructionSlot>()));

        gameObject.SetActive(true);
    }

    // ќчищаем всю информацию о здании, очищаем действи€ с кнопок, закрываем другие меню, св€занные со зданием
    public void CloseMenu()
    {
        building = null;

        buildingImage.sprite = default;
        buildingNameText.text = "";
        buildingDescriptionText.text = "";

        produceButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();
        stopButton.onClick.RemoveAllListeners();
        replaceButton.onClick.RemoveAllListeners();

        upgradeMenu.CloseMenu();
        constructionMenu.CloseMenu();
 
        gameObject.SetActive(false);
    }

    // ѕри нажатии на кнопку остановки переключаем пассиноые производство и обновл€ем кнопку остановки
    private void TogglePassiveProduction()
    {
        building.production.TogglePassiveProduction();
        UpdateStopButton();
    }

    // “екст кнопки остановки измен€етс€ в зависимости от того, остановлено здание или нет
    private void UpdateStopButton()
    {
        if (building.production.passiveProductionEnabled)
        {
            stopButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
        }
        else
        {
            stopButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }
    }
}
