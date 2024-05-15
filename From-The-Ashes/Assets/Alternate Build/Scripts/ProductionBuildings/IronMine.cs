using System;
using System.Collections;
using UnityEngine;

public class IronMine : ProductionBuilding
{
    public static Action ProduceIronOre;

    protected override void Awake()
    {
        ProduceIronOre += () => Produce(clickProductionQuantity);
    }

    public override void InvokeAction()
    {
        ProduceIronOre.Invoke();
    }

    protected override void Produce(int quantity)
    {
        NewResources.IronOreProduced.Invoke(quantity);
    }
}
