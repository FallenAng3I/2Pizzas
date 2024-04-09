using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodProduction : MonoBehaviour
{
    public Text textWood;
    public int woodPerClick = 1; // Количество дерева за каждый клик
    public int woodPerSecond = 1; // Количество дерева в секунду
    public int upgradeCost = 10; // Стоимость улучшения

    private float nextUpdateTime;

    private void Start()
    {
        nextUpdateTime = Time.time + 1f; // Устанавливаем время следующего обновления дерева
    }

    private void Update()
    {
        if (Time.time > nextUpdateTime)
        {
            // Увеличиваем количество дерева на woodPerSecond каждую секунду
            IncreaseWood(woodPerSecond);
            nextUpdateTime = Time.time + 1f; // Обновляем время следующего обновления дерева
        }
    }

    public void UpgradeWoodProduction()
    {
        // Проверяем, хватает ли дерева для улучшения
        if (ResourceManager.instance.GetResourceAmount(ResourceType.Wood) >= upgradeCost)
        {
            // Уменьшаем количество дерева на стоимость улучшения
            ResourceManager.instance.SpendResource(ResourceType.Wood, upgradeCost);

            // Увеличиваем количество дерева за каждый клик
            woodPerClick++;

            // Увеличиваем стоимость улучшения
            upgradeCost += 5;

            Debug.Log("Улучшение выполнено!");

            // Обновляем текстовое поле с количеством дерева
            UpdateWoodText();
        }
        else
        {
            Debug.Log("Недостаточно дерева для улучшения!");
        }
    }

    public void IncreaseWood(int amount)
    {
        // Увеличиваем количество дерева
        ResourceManager.instance.AddResource(ResourceType.Wood, amount);

        // Обновляем текстовое поле с количеством дерева
        UpdateWoodText();
    }

    private void UpdateWoodText()
    {
        // Обновляем текстовое поле с количеством дерева
        textWood.text = "Дерево: " + ResourceManager.instance.GetResourceAmount(ResourceType.Wood);
    }
}