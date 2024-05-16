using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private Building building;

    [SerializeField] private TextMeshProUGUI buildingNameText;

    // Кнопки улучшений и поля стоимости улучшений
    [SerializeField] private Button passiveUpgradeButton;
    [SerializeField] private TextMeshProUGUI passiveUpgradeCostText;
    [SerializeField] private Button doubleUpgradeButton;
    [SerializeField] private TextMeshProUGUI doubleUpgradeCostText;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseMenu);

        CloseMenu();
    }

    // Закрываем меню, заполняем поля информации о здании, назначаем здание для кнопок апргредов и устанавливаем цену апгрейдов
    public void OpenMenu(Building newBuilding)
    {
        CloseMenu();

        building = newBuilding;

        buildingNameText.text = building.buildingInformation.buildingName;

        passiveUpgradeButton.onClick.AddListener(() => { building.productionBuilding.UpgradePassive(); UpdateCostText(); });
        doubleUpgradeButton.onClick.AddListener(() => { building.productionBuilding.UpgradeClick(); UpdateCostText(); });
        
        UpdateCostText();

        gameObject.SetActive(true);
    }

    // Очищаем информацию о здании и закрываем меню
    public void CloseMenu()
    {
        building = null;

        buildingNameText.text = "";

        passiveUpgradeButton.onClick.RemoveAllListeners();
        passiveUpgradeCostText.text = "";
        doubleUpgradeButton.onClick.RemoveAllListeners();
        doubleUpgradeCostText.text = "";

        gameObject.SetActive(false);
    }

    private void UpdateCostText()
    {
        passiveUpgradeCostText.text = building.productionBuilding.PassiveUpgradeCostInWood.ToString() + " wood";
        doubleUpgradeCostText.text = building.productionBuilding.ClickUpgradeCostInWood.ToString() + " wood";
    }
}
