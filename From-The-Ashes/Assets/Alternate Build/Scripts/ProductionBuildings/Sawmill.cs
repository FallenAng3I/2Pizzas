using System;
using System.Diagnostics;

public class Sawmill : ProductionBuilding
{
    public static Action ProduceWood;

    public override void InvokeAction()
    {
        ProduceWood.Invoke();
    }

    protected override void Produce(int quantity)
    {
        NewResources.WoodProduced.Invoke(quantity);
    }

    protected override void OnEnable()
    {
        ProduceWood += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceWood -= ProduceOnClick;
    }
}
