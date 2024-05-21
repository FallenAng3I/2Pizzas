public class IronOreProduction : Production
{
    protected override void Produce(int quantity)
    {
        NewResources.IronOreProduced.Invoke(quantity);
    }
}
