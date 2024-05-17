using System;
using UnityEngine;

public class LeadFactory : ProductionBuilding
{
    public static Action ProduceLead;

    [SerializeField] private int leadOreForLead;

    public override void InvokeAction()
    {
        ProduceLead.Invoke();
    }

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.LeadOreNeeded.Invoke(leadOreForLead * quantity);

        if (enoughResources)
        {
            NewResources.LeadOreConsumed.Invoke(leadOreForLead * quantity);
            NewResources.LeadProduced.Invoke(quantity);
        }
    }

    protected override void OnEnable()
    {
        ProduceLead += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceLead -= ProduceOnClick;
    }
}
