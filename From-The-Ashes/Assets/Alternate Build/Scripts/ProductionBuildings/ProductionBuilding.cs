using System.Collections;
using UnityEngine;

public abstract class ProductionBuilding : MonoBehaviour
{
    [Header("Production For Click")]
    [SerializeField] protected int clickProductionQuantity;
    [SerializeField] protected int clickProductionQuantityIncrease;
    [SerializeField] protected int clickUpgradeCostInWood; // ћожно добавить цены в других ресурсах
    [SerializeField] protected int clickUpgradeCostInWoodIncrease;

    [Header("Passive Production")]
    [SerializeField] protected bool passiveProductionUpgraded;
    [SerializeField] public bool passiveProductionEnabled = true;
    [SerializeField] protected int passiveProductionTime;
    [SerializeField] protected int passiveProductionQuantity;
    [SerializeField] protected int passiveProductionQuantityIncrease;
    [SerializeField] protected int passiveUpgradeCostInWood; // ћожно добавить цены в других ресурсах
    [SerializeField] protected int passiveUpgradeCostInWoodIncrease;

    public int ClickUpgradeCostInWood { get { return clickUpgradeCostInWood; } }
    public int PassiveUpgradeCostInWood { get { return passiveUpgradeCostInWood; } }

    protected IEnumerator ProductionCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(passiveProductionTime);
            if (passiveProductionUpgraded && passiveProductionEnabled) Produce(passiveProductionQuantity);
        }
    }

    protected abstract void Awake();

    protected void Start()
    {
        StartCoroutine(ProductionCycle());
    }

    public abstract void InvokeAction();

    protected abstract void Produce(int quantity);

    public void UpgradeClick()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(clickUpgradeCostInWood);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(clickUpgradeCostInWood);

            // ћожно использовать другую форму апгрейда, например, удваивать количество продукта
            clickProductionQuantity += clickProductionQuantityIncrease;

            clickUpgradeCostInWood += clickUpgradeCostInWoodIncrease;
        }
    }

    public void UpgradePassive()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(passiveUpgradeCostInWood);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(passiveUpgradeCostInWood);

            if (!passiveProductionUpgraded)
            {
                passiveProductionUpgraded = true;
            }
            else
            {
                // ћожно использовать другую форму апгрейда, например, удваивать количество продукта
                passiveProductionQuantity += passiveProductionQuantityIncrease;
            }

            passiveUpgradeCostInWood += passiveUpgradeCostInWoodIncrease;
        }
    }

    public void TogglePassiveProduction()
    {
        passiveProductionEnabled = !passiveProductionEnabled;
    }
}
