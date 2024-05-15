using System;
using System.Collections;
using UnityEngine;

public class MilitaryFactory : ProductionBuilding
{
    [SerializeField] private int leadForAmmunition;
    [SerializeField] private int steelForAmmunition;

    public static Action ProduceAmmunition;

    protected override void Awake()
    {
        ProduceAmmunition += () => Produce(clickProductionQuantity);
    }

    public override void InvokeAction()
    {
        ProduceAmmunition();
    }

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.LeadNeeded.Invoke(leadForAmmunition * quantity) && NewResources.SteelNeeded.Invoke(steelForAmmunition * quantity);

        if (enoughResources)
        {
            NewResources.LeadConsumed.Invoke(leadForAmmunition * quantity);
            NewResources.SteelConsumed.Invoke(steelForAmmunition * quantity);
            NewResources.AmmunitionProduced.Invoke(quantity);
        }
    }
}
