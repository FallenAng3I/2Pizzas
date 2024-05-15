using System;

public class Sawmill : ProductionBuilding
{
    public static Action ProduceWood;

    protected override void Awake()
    {
        ProduceWood += ProduceMediator;
    }

    public override void InvokeAction()
    {
        ProduceWood.Invoke();
    }

    private void ProduceMediator()
    {
        Produce(clickProductionQuantity);
    }

    protected override void Produce(int quantity)
    {
        NewResources.WoodProduced.Invoke(quantity);
    }

    private void OnDestroy()
    {
        ProduceWood -= ProduceMediator;
    }
}
