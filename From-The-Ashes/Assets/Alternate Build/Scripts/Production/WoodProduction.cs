public class WoodProduction : Production
{
    protected override void Produce(int quantity)
    {
        NewResources.WoodProduced.Invoke(quantity);
    }
}
