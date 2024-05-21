using System;
using UnityEngine;

public class AmmunitionProduction : Production
{
    [SerializeField] private int leadForAmmunition;
    [SerializeField] private int steelForAmmunition;

    public static Action ProduceAmmunition;

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

    protected override void OnEnable()
    {
        ProduceAmmunition += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceAmmunition -= ProduceOnClick;
    }
}
