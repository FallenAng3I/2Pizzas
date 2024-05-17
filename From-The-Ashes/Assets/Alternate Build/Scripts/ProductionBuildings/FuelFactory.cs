using System;
using UnityEngine;

public class FuelFactory : ProductionBuilding
{
    public static Action ProduceFuel;

    [SerializeField] private int oilForFuel;

    public override void InvokeAction()
    {
        ProduceFuel.Invoke();
    }

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.IronOreNeeded.Invoke(oilForFuel * quantity);

        if (enoughResources)
        {
            NewResources.OilConsumed.Invoke(oilForFuel * quantity);
            NewResources.FuelProduced.Invoke(quantity);
        }
    }

    protected override void OnEnable()
    {
        ProduceFuel += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceFuel -= ProduceOnClick;
    }
}
