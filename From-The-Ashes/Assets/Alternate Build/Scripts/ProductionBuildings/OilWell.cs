using System;
using System.Collections;
using UnityEngine;

public class OilWell : ProductionBuilding
{
    public static Action ProduceOil;

    protected override void Awake()
    {
        ProduceOil += () => Produce(clickProductionQuantity);
    }

    public override void InvokeAction()
    {
        ProduceOil.Invoke();
    }

    protected override void Produce(int quantity)
    {
        NewResources.OilProduced.Invoke(quantity);
    }
}
