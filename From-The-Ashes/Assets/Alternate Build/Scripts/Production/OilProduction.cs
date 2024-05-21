using System;

public class OilProduction : Production
{
    public static Action ProduceOil;

    public override void InvokeAction()
    {
        ProduceOil.Invoke();
    }

    protected override void Produce(int quantity)
    {
        NewResources.OilProduced.Invoke(quantity);
    }

    protected override void OnEnable()
    {
        ProduceOil += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceOil -= ProduceOnClick;
    }
}
