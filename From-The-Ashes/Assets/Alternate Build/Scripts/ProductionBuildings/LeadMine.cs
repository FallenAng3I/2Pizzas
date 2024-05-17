using System;

public class LeadMine : ProductionBuilding
{
    public static Action ProduceLeadOre;

    public override void InvokeAction()
    {
        ProduceLeadOre.Invoke();
    }

    protected override void Produce(int quantity)
    {
        NewResources.LeadOreProduced.Invoke(quantity);
    }

    protected override void OnEnable()
    {
        ProduceLeadOre += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceLeadOre -= ProduceOnClick;
    }
}
