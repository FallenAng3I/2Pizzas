using System;

public class IronMine : ProductionBuilding
{
    public static Action ProduceIronOre;

    public override void InvokeAction()
    {
        ProduceIronOre.Invoke();
    }

    protected override void Produce(int quantity)
    {
        NewResources.IronOreProduced.Invoke(quantity);
    }

    protected override void OnEnable()
    {
        ProduceIronOre += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceIronOre -= ProduceOnClick;
    }
}
