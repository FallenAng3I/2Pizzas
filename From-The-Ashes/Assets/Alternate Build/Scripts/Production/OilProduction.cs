public class OilProduction : Production
{
    protected override void Produce(int quantity)
    {
        NewResources.OilProduced.Invoke(quantity);
    }
}
