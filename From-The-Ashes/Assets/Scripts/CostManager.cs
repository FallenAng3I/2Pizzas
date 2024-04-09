using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Это статическое поле, которое позволит другим классам обращаться к ResourceManager через его экземпляр
    public static ResourceManager instance;

    // Ваши ресурсы
    public int wood = 0;

    private void Awake()
    {
        // Убеждаемся, что instance не пустой и устанавливаем его, если это так
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Уже существует экземпляр ResourceManager");
        }
    }

    // Методы для получения и траты ресурсов
    public int GetResourceAmount(ResourceType resourceType)
    {
        // Реализуйте логику получения количества ресурсов
        // В этом примере мы просто вернем количество дерева
        return wood;
    }

    public void SpendResource(ResourceType resourceType, int amount)
    {
        // Реализуйте логику траты ресурсов
        // В этом примере мы просто вычтем количество дерева
        wood -= amount;
    }
}

// Предполагая, что у вас есть enum для разных типов ресурсов
public enum ResourceType
{
    Wood,
    // Другие типы ресурсов, если они есть
}

public class BuildingManager : MonoBehaviour
{
    public int initialCost = 2; // Начальная стоимость здания
    public int costIncrement = 8; // Увеличение стоимости после каждой покупки

    private int currentCost; // Текущая стоимость здания

    private void Start()
    {
        currentCost = initialCost; // Устанавливаем начальную стоимость
    }

    public bool CanAfford()
    {
        // Проверяем, хватает ли ресурсов для покупки здания
        return ResourceManager.instance.GetResourceAmount(ResourceType.Wood) >= currentCost;
    }

    public void BuyBuilding()
    {
        if (CanAfford())
        {
            // Вычитаем стоимость из ресурсов
            ResourceManager.instance.SpendResource(ResourceType.Wood, currentCost);

            // Увеличиваем стоимость для следующей покупки
            currentCost += costIncrement;
        }
        else
        {
            Debug.Log("Недостаточно ресурсов для покупки здания!");
        }
    }
}
