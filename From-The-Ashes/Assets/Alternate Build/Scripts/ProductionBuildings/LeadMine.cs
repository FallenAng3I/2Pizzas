using System;
using System.Collections;
using UnityEngine;

public class LeadMine : ProductionBuilding
{
    public static Action ProduceLeadOre;

    protected override void Awake()
    {
        ProduceLeadOre += () => Produce(clickProductionQuantity);
    }

    public override void InvokeAction()
    {
        ProduceLeadOre.Invoke();
    }

    protected override void Produce(int quantity)
    {
        NewResources.LeadOreProduced.Invoke(quantity);
    }
}
