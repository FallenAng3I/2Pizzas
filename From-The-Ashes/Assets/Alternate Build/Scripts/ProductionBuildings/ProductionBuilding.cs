using System;
using System.Collections;
using UnityEngine;
public abstract class ProductionBuilding : MonoBehaviour
{
    [Header("Production On Click")]
    [SerializeField] protected int clickProductionQuantity;
    [SerializeField] protected int clickProductionQuantityIncrease;

    [Header("Click Upgrade Cost")]
    [SerializeField] protected int clickUpgradeCostInWood;
    [SerializeField] protected int clickUpgradeCostInWoodIncrease;
    [SerializeField] protected int clickUpgradeCostInSteel;
    [SerializeField] protected int clickUpgradeCostInSteelIncrease;
    [SerializeField] protected int clickUpgradeCostInFuel;
    [SerializeField] protected int clickUpgradeCostInFuelIncrease;
    [SerializeField] protected int clickUpgradeCostInLead;
    [SerializeField] protected int clickUpgradeCostInLeadIncrease;
    // ћожно добавить цены в других ресурсах

    [Header("Passive Production")]
    [SerializeField] protected bool passiveProductionUpgraded;
    [SerializeField] public bool passiveProductionEnabled = true;
    [SerializeField] protected int passiveProductionTime;
    [SerializeField] protected int passiveProductionQuantity;
    [SerializeField] protected int passiveProductionQuantityIncrease;

    [Header("Passive Upgrade Cost")]
    [SerializeField] protected int passiveUpgradeCostInWood;
    [SerializeField] protected int passiveUpgradeCostInWoodIncrease;
    [SerializeField] protected int passiveUpgradeCostInSteel;
    [SerializeField] protected int passiveUpgradeCostInSteelIncrease;
    [SerializeField] protected int passiveUpgradeCostInFuel;
    [SerializeField] protected int passiveUpgradeCostInFuelIncrease;
    [SerializeField] protected int passiveUpgradeCostInLead;
    [SerializeField] protected int passiveUpgradeCostInLeadIncrease;
    // ћожно добавить цены в других ресурсах

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

    protected void Start()
    {
        StartCoroutine(ProductionCycle());
    }

    protected void ProduceOnClick()
    {
        Produce(clickProductionQuantity);
    }

    public abstract void InvokeAction();

    protected abstract void Produce(int quantity);

    protected abstract void OnEnable();

    protected abstract void OnDisable();

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
