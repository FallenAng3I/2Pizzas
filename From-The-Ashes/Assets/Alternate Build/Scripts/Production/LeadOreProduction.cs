public class LeadOreProduction : Production
{
    protected override void Produce(int quantity)
    {
        NewResources.LeadOreProduced.Invoke(quantity);
    }
}
