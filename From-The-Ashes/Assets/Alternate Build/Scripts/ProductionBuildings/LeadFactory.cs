using System;
using System.Collections;
using UnityEngine;

public class LeadFactory : ProductionBuilding
{
    public static Action ProduceLead;

    [SerializeField] private int leadOreForLead;

    protected override void Awake()
    {
        ProduceLead += () => Produce(clickProductionQuantity);
    }

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
}
