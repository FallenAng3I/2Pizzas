using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] private UpgradeMenu upgradeMenu;
    [SerializeField] private ConstructionMenu constructionMenu;

    [SerializeField] private Button closeButton;

    private Building building;

    // Поля информации о здании
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingDescriptionText;

    // Кнопки управления зданием
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

    // Закрываем меню, заполняем поля информации о здании, назначаем это здание для кнопок управления и открываем меню
    private void OpenMenu(Building newBuilding)
    {
        CloseMenu();

        building = newBuilding;

        buildingNameText.text = building.buildingName;
        buildingDescriptionText.text = building.buildingDescription;
        UpdateStopButton();

        produceButton.onClick.AddListener(building.productionBuilding.InvokeAction);
        upgradeButton.onClick.AddListener(() => upgradeMenu.OpenMenu(building));
        stopButton.onClick.AddListener(() => { building.productionBuilding.TogglePassiveProduction(); UpdateStopButton(); });
        replaceButton.onClick.AddListener(() => constructionMenu.OpenMenu(building.GetComponentInParent<ConstructionSlot>()));

        gameObject.SetActive(true);
    }

    // Очищаем информацию о здании и закрываем меню
    public void CloseMenu()
    {
        building = null;

        buildingImage.sprite = default;
        buildingNameText.text = "";
        buildingDescriptionText.text = "";

        produceButton.onClick.RemoveAllListeners();

        gameObject.SetActive(false);
        upgradeMenu.CloseMenu();
    }

    private void UpdateStopButton()
    {
        if (building.productionBuilding.passiveProductionEnabled)
        {
            stopButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
        }
        else
        {
            stopButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }
    }
}
