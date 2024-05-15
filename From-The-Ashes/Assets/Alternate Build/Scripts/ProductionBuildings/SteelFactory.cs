using System;
using System.Collections;
using UnityEngine;

public class SteelFactory : ProductionBuilding
{
    [SerializeField] private int ironForSteel;

    public static Action ProduceSteel;

    protected override void Awake()
    {
        ProduceSteel += () => Produce(clickProductionQuantity);
    }

    public override void InvokeAction()
    {
        ProduceSteel.Invoke();
    }

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.IronOreNeeded.Invoke(ironForSteel * quantity);

        if (enoughResources)
        {
            NewResources.IronOreConsumed.Invoke(ironForSteel * quantity);
            NewResources.SteelProduced.Invoke(quantity);
        }
    }
}
