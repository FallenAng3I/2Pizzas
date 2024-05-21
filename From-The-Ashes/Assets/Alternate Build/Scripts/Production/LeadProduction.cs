using UnityEngine;

public class LeadProduction : Production
{
    [SerializeField] private int leadOreForLead;

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.LeadOreNeeded.Invoke(leadOreForLead * quantity);

        if (enoughResources)
        {
            NewResources.LeadOreConsumed.Invoke(leadOreForLead * quantity);
            NewResources.LeadProduced.Invoke(quantity);
        }
    }
}
