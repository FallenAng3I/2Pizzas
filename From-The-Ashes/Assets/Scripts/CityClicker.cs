using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Класс для описания улучшения
[System.Serializable]
public class Upgrade
{
    public Button button; // Кнопка улучшения
    public float clickMultiplier; // Множитель для улучшения клика
}

// Класс для описания здания
[System.Serializable]
public class Building
{
    public Button button; // Кнопка здания для клика
    public Upgrade[] upgrades; // Массив улучшений для данного типа здания
}

// Главный класс, управляющий градостроительным кликером
public class CityClicker : MonoBehaviour
{
    private int wood; // Общий счетчик ресурса A
    private int iron; // Общий счетчик ресурса B
    private int oil; // Общий счетчик ресурса C

    public TextMeshProUGUI txtWOOD; // Текстовое поле для отображения количества ресурсов A
    public TextMeshProUGUI txtIRON; // Текстовое поле для отображения количества ресурсов B
    public TextMeshProUGUI txtOIL; // Текстовое поле для отображения количества ресурсов C

    public Building[] sawmill; // Здания для добычи ресурса A
    public Building[] mine; // Здания для добычи ресурса B
    public Building[] oilrig; // Здания для добычи ресурса C

    void Start()
    {
        // Добавляем слушателей событий клика для каждого типа зданий
        AddListeners(sawmill, txtWOOD, ref wood);
        AddListeners(mine, txtIRON, ref iron);
        AddListeners(oilrig, txtOIL, ref oil);
    }

    // Метод для добавления слушателей событий клика для зданий
    void AddListeners(Building[] buildings, TextMeshProUGUI resourceText, ref int resourceCounter)
    {
        foreach (Building building in buildings)
        {
            int tempResourceCounter = resourceCounter;

            // Для каждой кнопки здания добавляем слушатель события клика
            building.button.onClick.AddListener(() => ClickBuilding(resourceText, ref tempResourceCounter));
           
            // Добавляем слушатели событий клика для каждого улучшения здания
            foreach (Upgrade upgrade in building.upgrades)
            {
                upgrade.button.onClick.AddListener(() => UpgradeClick(resourceText, ref tempResourceCounter, upgrade));
            }

            resourceCounter = tempResourceCounter; // Присваиваем измененное значение обратно resourceCounter
        }
    }

    // Метод вызывается при клике по зданию
    void ClickBuilding(TextMeshProUGUI resourceText, ref int resourceCounter)
    {
        // Увеличиваем общий счетчик ресурсов
        resourceCounter++;

        // Обновляем текстовое поле с количеством ресурсов
        UpdateResourceText(resourceText, resourceCounter);
    }

    // Метод для обновления текстового поля с количеством ресурсов
    void UpdateResourceText(TextMeshProUGUI resourceText, int resourceCounter)
    {
        // Обновляем текстовое поле с учетом текущего количества ресурсов
        resourceText.text = " " + resourceCounter;
    }
    
    // Метод вызывается при клике по улучшению здания
    void UpgradeClick(TextMeshProUGUI resourceText, ref int resourceCounter, Upgrade upgrade)
    {
        // Увеличиваем множитель клика на основе улучшения
        resourceCounter += Mathf.FloorToInt(upgrade.clickMultiplier);

        // Обновляем текстовое поле с количеством ресурсов
        UpdateResourceText(resourceText, resourceCounter);
    }
}

