using UnityEngine;

public class FuelProduction : Production
{
    [SerializeField] private int oilForFuel;

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.IronOreNeeded.Invoke(oilForFuel * quantity);

        if (enoughResources)
        {
            NewResources.OilConsumed.Invoke(oilForFuel * quantity);
            NewResources.FuelProduced.Invoke(quantity);
        }
    }
}
